using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class CutoutTextMaskUI : TextMeshProUGUI
{
    private static readonly int StencilComp = Shader.PropertyToID("_StencilComp");
    public override Material materialForRendering
    {
        get {
            Material material = new Material(base.materialForRendering);
            material.SetInt(StencilComp, (int)CompareFunction.NotEqual);
            return material;
        }
    }
}