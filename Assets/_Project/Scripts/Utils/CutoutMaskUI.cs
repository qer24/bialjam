using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class CutoutMaskUI : Image {
    private static readonly int StencilComp = Shader.PropertyToID("_StencilComp");
    private Sprite activeSprite => overrideSprite != null ? overrideSprite : sprite;
    public override Material materialForRendering {
        get {
            Material material = new Material(base.materialForRendering);
            material.SetInt(StencilComp, (int)CompareFunction.NotEqual);
            return material;
        }
    }

    public override bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
    {
        // Skip if deactived.
        // if (!isActiveAndEnabled || !targetMask || !targetMask.isActiveAndEnabled)
        // {
        //     return true;
        // }

        return true;
    }
    
    // public static Rect RectTransformToScreenSpace(RectTransform transform)
    // {
    //     Vector2 size = Vector2.Scale(transform.rect.size, transform.lossyScale);
    //     return new Rect((Vector2)transform.position - (size * transform.pivot), size);
    // }
    
    public static Rect RectTransformToScreenSpace(RectTransform transform)
    {
        Vector2 size= Vector2.Scale(transform.rect.size, transform.lossyScale);
        float x= transform.position.x + transform.anchoredPosition.x;
        float y= Screen.height - transform.position.y - transform.anchoredPosition.y;
 
        return new Rect(x, y, size.x, size.y);
    }

    // public override bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
    // {
    //     if (alphaHitTestMinimumThreshold <= 0)
    //         return true;
    //
    //     if (alphaHitTestMinimumThreshold > 1)
    //         return false;
    //
    //     if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, screenPoint, eventCamera, out var local))
    //         return false;
    //
    //     var rect = GetPixelAdjustedRect();
    //
    //     // Convert to have lower left corner as reference point.
    //     local.x += rectTransform.pivot.x * rect.width;
    //     local.y += rectTransform.pivot.y * rect.height;
    //
    //     local = MapCoordinate(local, rect);
    //
    //     // Convert local coordinates to texture space.
    //     Rect spriteRect = activeSprite.textureRect;
    //     float x = (spriteRect.x + local.x) / activeSprite.texture.width;
    //     float y = (spriteRect.y + local.y) / activeSprite.texture.height;
    //
    //     try
    //     {
    //         return activeSprite.texture.GetPixelBilinear(x, y).a < alphaHitTestMinimumThreshold;
    //     }
    //     catch (UnityException e)
    //     {
    //         Debug.LogError("Using alphaHitTestMinimumThreshold greater than 0 on Image whose sprite texture cannot be read. " + e.Message + " Also make sure to disable sprite packing for this sprite.", this);
    //         return true;
    //     }
    // }
    //
    // private Vector2 MapCoordinate(Vector2 local, Rect rect)
    // {
    //     Rect spriteRect = activeSprite.rect;
    //     if (type == Type.Simple || type == Type.Filled)
    //         return new Vector2(local.x * spriteRect.width / rect.width, local.y * spriteRect.height / rect.height);
    //
    //     Vector4 border = activeSprite.border;
    //     Vector4 adjustedBorder = GetAdjustedBorders(border / pixelsPerUnit, rect);
    //
    //     for (int i = 0; i < 2; i++)
    //     {
    //         if (local[i] <= adjustedBorder[i])
    //             continue;
    //
    //         if (rect.size[i] - local[i] <= adjustedBorder[i + 2])
    //         {
    //             local[i] -= (rect.size[i] - spriteRect.size[i]);
    //             continue;
    //         }
    //
    //         if (type == Type.Sliced)
    //         {
    //             float lerp = Mathf.InverseLerp(adjustedBorder[i], rect.size[i] - adjustedBorder[i + 2], local[i]);
    //             local[i] = Mathf.Lerp(border[i], spriteRect.size[i] - border[i + 2], lerp);
    //         }
    //         else
    //         {
    //             local[i] -= adjustedBorder[i];
    //             local[i] = Mathf.Repeat(local[i], spriteRect.size[i] - border[i] - border[i + 2]);
    //             local[i] += border[i];
    //         }
    //     }
    //
    //     return local;
    // }
    //
    // private Vector4 GetAdjustedBorders(Vector4 border, Rect adjustedRect)
    // {
    //     Rect originalRect = rectTransform.rect;
    //
    //     for (int axis = 0; axis <= 1; axis++)
    //     {
    //         float borderScaleRatio;
    //
    //         // The adjusted rect (adjusted for pixel correctness)
    //         // may be slightly larger than the original rect.
    //         // Adjust the border to match the adjustedRect to avoid
    //         // small gaps between borders (case 833201).
    //         if (originalRect.size[axis] != 0)
    //         {
    //             borderScaleRatio = adjustedRect.size[axis] / originalRect.size[axis];
    //             border[axis] *= borderScaleRatio;
    //             border[axis + 2] *= borderScaleRatio;
    //         }
    //
    //         // If the rect is smaller than the combined borders, then there's not room for the borders at their normal size.
    //         // In order to avoid artefacts with overlapping borders, we scale the borders down to fit.
    //         float combinedBorders = border[axis] + border[axis + 2];
    //         if (adjustedRect.size[axis] < combinedBorders && combinedBorders != 0)
    //         {
    //             borderScaleRatio = adjustedRect.size[axis] / combinedBorders;
    //             border[axis] *= borderScaleRatio;
    //             border[axis + 2] *= borderScaleRatio;
    //         }
    //     }
    //     return border;
    // }
}