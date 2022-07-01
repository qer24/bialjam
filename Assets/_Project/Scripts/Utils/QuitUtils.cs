using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
 
public static class QuitUtils
{
    #region Static
 
    public static bool isQuitting;
 
#if UNITY_EDITOR
        [InitializeOnEnterPlayMode]
        private static void EnterPlayMode(EnterPlayModeOptions options)
        {
            isQuitting = false;
        }
#endif
 
    [RuntimeInitializeOnLoadMethod]
    private static void RunOnStart()
    {
        isQuitting = false;
        Application.quitting += Quit;
    }

    private static void Quit()
    {
        isQuitting = true;
    }
 
    #endregion
}