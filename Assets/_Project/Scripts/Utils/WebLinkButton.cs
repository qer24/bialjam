using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class WebLinkButton : MonoBehaviour
{
    public void OpenURL(string url) => Application.OpenURL(url);
}