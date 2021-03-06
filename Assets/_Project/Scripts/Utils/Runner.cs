using System.Collections;
using UnityEngine;

namespace Utils
{
    public class Runner : MonoBehaviour
    {
        public static Runner _instance; 
    
        /// <summary>
        /// When the game loads, create an instance of this class, and make sure it isn't destroyed between scenes
        /// </summary>
        private static void CreateInstance()
        {
            _instance = new GameObject ("Runner").AddComponent<Runner>();
            
            DontDestroyOnLoad (_instance);
        }

        /// <summary>
        /// Run a coroutine
        /// </summary>
        public static void Run(IEnumerator coroutine)
        {
            if (_instance == null)
                CreateInstance();
            _instance.StartCoroutine(coroutine);
        }
    }
}