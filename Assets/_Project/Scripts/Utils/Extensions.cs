using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utils
{
    public static class Extensions
    {
        /// <summary>
        /// Get all interfaces in the given scene
        /// </summary>
        public static List<T> FindInterfaces<T>(this Scene scene)
        {
            var interfaces = new List<T>();
            var rootGameObjects = scene.GetRootGameObjects();
            foreach (var rootGameObject in rootGameObjects)
            {
                var childrenInterfaces = rootGameObject.GetComponentsInChildren<T>();
                foreach (var childInterface in childrenInterfaces)
                    interfaces.Add(childInterface);
            }

            return interfaces;
        }

        public static void ResetVelocity(this Rigidbody rb)
        {
            rb.velocity = Vector3.zero;
        }
        
        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            T component = gameObject.GetComponent<T>();

            if (component == null)     
                component = gameObject.AddComponent<T>() as T;

            return component;

        }

        public static bool LessThan(this string a, string b)
        {
            return string.CompareOrdinal(a, b) < 0;
        }
        
        public static bool MoreThan(this string a, string b)
        {
            return string.CompareOrdinal(a, b) > 0;
        }
        
        public static float ClampAngle(this float angle) {
            if(angle < 0f)
                return angle + (360f * (int) ((angle / 360f) + 1));
            else if(angle > 360f)
                return angle - (360f * (int) (angle / 360f));
            else
                return angle;
        }
        
        public static int ToLayer (this LayerMask bitmask ) {
            int result = bitmask>0 ? 0 : 31;
            while( bitmask>1 ) {
                bitmask >>= 1;
                result++;
            }
            return result;
        }

        public static int Floor(this float f)
        {
            return Mathf.FloorToInt(f);
        }
        
        public static int Round(this float f)
        {
            return Mathf.RoundToInt(f);
        }
        
        public static int Ceil(this float f)
        {
            return Mathf.CeilToInt(f);
        }

        public static LTDescr LeanWidth(this LineRenderer line, float to, float time)
        {
            return LeanTween.value(line.gameObject, val => line.widthCurve = AnimationCurve.Constant(0, 1, val), line.startWidth, to, time);
        }

        public static void SetConstantWidth(this LineRenderer line, float val)
        {
            line.widthCurve = AnimationCurve.Constant(0, 1, val);
        }

        // public static LTDescr LeanRotate(this Transform transform, Quaternion to, float time)
        // {
        //     var startRot = transform.rotation;
        //     return LeanTween.value(transform.gameObject, val => Quaternion.Lerp(startRot, to, val), 0, 1, time);
        // }
        
        public static LTDescr LeanSmoothRotate(this Transform transform, Quaternion to, float time)
        {
            var startRot = transform.rotation;
            return LeanTween.value(transform.gameObject, val => transform.rotation = Quaternion.Lerp(startRot, to, val), 0, 1, time);
        }
        
        public static LTDescr LeanSmoothRotateLocal(this Transform transform, Quaternion to, float time)
        {
            var startRot = transform.localRotation;
            return LeanTween.value(transform.gameObject, val => transform.localRotation = Quaternion.Lerp(startRot, to, val), 0, 1, time);
        }
    }
}