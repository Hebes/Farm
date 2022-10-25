using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MFarm.Transition
{
    /// <summary>
    /// 场景转换管理类
    /// </summary>
    public class TransitionManager : MonoBehaviour
    {
        [SceneName]
        public string startSceneName = string.Empty;
        /// <summary>是否切换场景</summary>
        private bool isFade;

        private CanvasGroup fadeCanvasGroup => GameObject.Find("Fade Panel").GetComponent<CanvasGroup>();

        private void OnEnable() => EventHandler.TransitionEvent += OnTransitionEvent;
        private void OnDisable() => EventHandler.TransitionEvent -= OnTransitionEvent;
        private IEnumerator Start()
        {
            yield return LoadSceneSetActive(startSceneName);
            EventHandler.CallAfterSceneLoadedEvent();
        }


        /// <summary>
        /// 切换场景
        /// </summary>
        /// <param name="scenToGO"></param>
        /// <param name="positionToGo"></param>
        private void OnTransitionEvent(string scenToGO, Vector3 positionToGo)
        {
            if (!isFade)//如果不是切换场景的情况下
                StartCoroutine(Transition(scenToGO, positionToGo));
        }

        /// <summary>
        /// 场景切换
        /// </summary>
        /// <param name="sceneName">目标位置</param>
        /// <param name="targetPosition">目标场景</param>
        /// <returns></returns>
        private IEnumerator Transition(string sceneName, Vector3 targetPosition)
        {
            EventHandler.CallBeforeSceneUnLoadEvent();
            yield return Fade(1);
            yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);

            yield return LoadSceneSetActive(sceneName);
            //移动人物坐标
            EventHandler.CallMoveToPosition(targetPosition);
            EventHandler.CallAfterSceneLoadedEvent();
            yield return Fade(0);
        }

        /// <summary>
        /// 加载场景并设置为激活
        /// </summary>
        /// <param name="sceneName"></param>
        /// <returns></returns>
        private IEnumerator LoadSceneSetActive(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
            SceneManager.SetActiveScene(newScene);
        }

        /// <summary>loading画面的显示与隐藏 淡入淡出场景</summary>
        /// <param name="targetAlpha">1是黑 0是透明</param>
        /// <returns></returns>
        private IEnumerator Fade(float targetAlpha)
        {
            isFade = true;
            fadeCanvasGroup.blocksRaycasts = true;
            float speed = Mathf.Abs(fadeCanvasGroup.alpha - targetAlpha) / Settings.fadeDuretion;
            while (!Mathf.Approximately(fadeCanvasGroup.alpha, targetAlpha))//Approximately 判断是否大概相似
            {
                fadeCanvasGroup.alpha = Mathf.MoveTowards(fadeCanvasGroup.alpha, targetAlpha, speed * Time.deltaTime);
                yield return null;
            }
            fadeCanvasGroup.blocksRaycasts = false;
            isFade = false;
        }
    }
}