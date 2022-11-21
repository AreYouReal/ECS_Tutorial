using System;
using UnityEngine;

namespace ECSTut.Scripts.MonoBehaviours{
    public class CameraSingleton : MonoBehaviour{
        public static Camera I;

        private void Awake(){
            I = GetComponent<Camera>();
        }
    }
}