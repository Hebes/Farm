using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MFarm.Transition
{
    public class Telepor : MonoBehaviour
    {
        public string sceneToGo;
        public Vector3 positionTpGo;
        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("进入了触发器");
            if (other.CompareTag("Player"))
                EventHandler.CallTransitionEvent(sceneToGo, positionTpGo);
        }
    }
}