using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
public static class ClassExtensions
{
    public static Vector3 WithX(this Vector3 vector, float x)
    {
        return new Vector3(x, vector.y, vector.z);
    }
    
    public static Vector3 WithY(this Vector3 vector, float y)
    {
        return new Vector3(vector.x, y, vector.z);
    }
    
    public static Vector3 WithZ(this Vector3 vector, float z)
    {
        return new Vector3(vector.x, vector.y, z);
    }

    public static float MoveTowards(this float current, float target, float maxDelta)
    {
        return Mathf.MoveTowards(current, target, maxDelta);
    }
    
    /// <summary>
    /// This isn't a recursive function, only searches the top children of a transform.
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="tag"></param>
    /// <returns></returns>
    public static GameObject FindObjectWithTag(this Transform parent, string tag)
    {
        return (from Transform child in parent where child.CompareTag(tag) select child.gameObject).FirstOrDefault();
    }
    
    public static GameObject[] FindObjectsWithTag(this Transform parent, string tag)
    {
        var taggedGameObjects = new List<GameObject>();
 
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            if (child.CompareTag(tag))
            {
                taggedGameObjects.Add(child.gameObject);
            }
            if (child.childCount > 0)
            {
                taggedGameObjects.AddRange(FindObjectsWithTag(child, tag));
            }
        }
        return taggedGameObjects.ToArray();
    }

    public static Vector3 ToXYZ(this Vector2 vector)
    {
        return new Vector3(vector.x, 0, vector.y);
    }
    
    public static Vector3 ToXYZ(this Vector2 vector, float y)
    {
        return new Vector3(vector.x, y, vector.y);
    }
    
    public static Vector2 ToXY(this Vector3 vector)
    {
        return new Vector2(vector.x, vector.z);
    }

    public static T GetRandomElement<T>(this IEnumerable<T> input)
    { 
        var enumerable = input as T[] ?? input.ToArray();
        return enumerable[new Squirrel3().Range(0, enumerable.Length)];
    }

    public static LTDescr LeanValue(this ScaleToMiddleSlider slider, float from, float to, float time)
    {
        return LeanTween.value(slider.gameObject, val => slider.Value = val, from, to, time);
    }

    public static Vector3 ToVector3(this float f)
    {
        return new Vector3(f, f, f);
    }
    
    public static Vector2 ToVector2(this float f)
    {
        return new Vector2(f, f);
    }

    public static LTDescr LeanColor(this Image image, Color to, float time)
    {
        return LeanTween.value(image.gameObject, val => image.color = val, image.color, to, time);
    }

    public static LTDescr LeanRotateLocal(this Transform transform, float anglesX, float anglesY, float anglesZ, float time)
    {
        var startRot = transform.localRotation;
        var endRot = transform.localRotation;
        endRot *= Quaternion.AngleAxis(anglesX, Vector3.right);
        endRot *= Quaternion.AngleAxis(anglesY, Vector3.up);
        endRot *= Quaternion.AngleAxis(anglesZ, Vector3.forward);
        
        return LeanTween.value(transform.gameObject, val => transform.localRotation = Quaternion.Lerp(startRot, endRot, val), 0, 1, time);
    }

    public static LTDescr LeanBackgroundColor(this Camera cam, Color to, float time)
    {
        var startColor = cam.backgroundColor;
        return LeanTween.value(cam.gameObject, val => cam.backgroundColor = Color.Lerp(startColor, to, val), 0, 1, time);
    }

    public static Queue<T> ToQueue<T>(this IEnumerable<T> enumerable)
    {
        return new Queue<T>(enumerable);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="format">
    /// 00.0
    /// |
    /// #0.0
    /// |
    /// 00.00
    /// |
    /// 00.000
    /// |
    /// #00.000
    /// |
    /// #0:00
    /// |
    /// #00:00
    /// |
    /// 0:00.0
    /// |
    /// #0:00.0
    /// |
    /// 0:00.00
    /// |
    /// #0:00.00
    /// |
    /// 0:00.000
    /// |
    /// #0:00.000
    /// </param>
    /// <returns></returns>
    public static string ToTime(this float toConvert, string format)
    {
        return format switch
        {
            "00.0" => $"{Mathf.Floor(toConvert) % 60:00}:{Mathf.Floor((toConvert * 10) % 10):0}",
            
            "#0.0" => $"{Mathf.Floor(toConvert) % 60:#0}:{Mathf.Floor((toConvert * 10) % 10):0}",
            
            "00.00" => $"{Mathf.Floor(toConvert) % 60:00}:{Mathf.Floor((toConvert * 100) % 100):00}",
            
            "00.000" => $"{Mathf.Floor(toConvert) % 60:00}:{Mathf.Floor((toConvert * 1000) % 1000):000}",
            
            "#00.000" => $"{Mathf.Floor(toConvert) % 60:#00}:{Mathf.Floor((toConvert * 1000) % 1000):000}",
            
            "#0:00" => $"{Mathf.Floor(toConvert / 60):#0}:{Mathf.Floor(toConvert) % 60:00}",
            
            "#00:00" => $"{Mathf.Floor(toConvert / 60):#00}:{Mathf.Floor(toConvert) % 60:00}",
            
            "0:00.0" => $"{Mathf.Floor(toConvert / 60):0}:{Mathf.Floor(toConvert) % 60:00}.{Mathf.Floor((toConvert * 10) % 10):0}",
            
            "#0:00.0" => $"{Mathf.Floor(toConvert / 60):#0}:{Mathf.Floor(toConvert) % 60:00}.{Mathf.Floor((toConvert * 10) % 10):0}",
            
            "0:00.00" => $"{Mathf.Floor(toConvert / 60):0}:{Mathf.Floor(toConvert) % 60:00}.{Mathf.Floor((toConvert * 100) % 100):00}",
            
            "#0:00.00" => $"{Mathf.Floor(toConvert / 60):#0}:{Mathf.Floor(toConvert) % 60:00}.{Mathf.Floor((toConvert * 100) % 100):00}",
            
            "0:00.000" => $"{Mathf.Floor(toConvert / 60):0}:{Mathf.Floor(toConvert) % 60:00}.{Mathf.Floor((toConvert * 1000) % 1000):000}",
            
            "#0:00.000" => $"{Mathf.Floor(toConvert / 60):#0}:{Mathf.Floor(toConvert) % 60:00}.{Mathf.Floor((toConvert * 1000) % 1000):000}",
            
            "00:00.000" => $"{Mathf.Floor(toConvert / 60):00}:{Mathf.Floor(toConvert) % 60:00}.{Mathf.Floor((toConvert * 1000) % 1000):000}",
            _ => "error"
        };
    }
    
    public static double? Median<T>(
        this IEnumerable<T> source)
    {
        if(Nullable.GetUnderlyingType(typeof(T)) != null)
            source = source.Where(x => x != null);

        int count = source.Count();
        if(count == 0)
            return null;

        source = source.OrderBy(n => n);

        int midpoint = count / 2;
        if(count % 2 == 0)
            return (Convert.ToDouble(source.ElementAt(midpoint - 1)) + Convert.ToDouble(source.ElementAt(midpoint))) / 2.0;
        return Convert.ToDouble(source.ElementAt(midpoint));
    }

    public static LTDescr LeanWeight(this Volume volume, float to, float time)
    {
        return LeanTween.value(volume.gameObject, val => volume.weight = val, volume.weight, to, time);
    }
    
    public static LTDescr LeanWeight(this Volume volume, float from, float to, float time)
    {
        return LeanTween.value(volume.gameObject, val => volume.weight = val, from, to, time);
    }

    public static Color WithAlpha(this Color color, float alhpa)
    {
        return new Color(color.r, color.g, color.b, alhpa);
    }

    public static LTDescr LeanVolume(this AudioSource source, float to, float time)
    {
        var startVol = source.volume;
        return LeanTween.value(source.gameObject, val => source.volume = val, startVol, to, time);
    }

    public static void Print(this IEnumerable ienumerable)
    {
        var stringBuilder = new StringBuilder("[");
        bool start = true;
        foreach (var element in ienumerable)
        {
            if (start)
            {
                start = false;
                stringBuilder.Append($"{element}");
            }
            else
            {
                stringBuilder.Append($", {element}");
            }
        }
        stringBuilder.Append("]");
        Debug.Log(stringBuilder.ToString());
    }

    public static LTDescr LeanPitch(this AudioSource src, float to, float time)
    {
        var start = src.pitch;
        return LeanTween.value(src.gameObject, val => src.pitch = val, start, to, time);
    }

    public static LTDescr LeanColor(this TextMeshPro text, Color to, float time)
    {
        var start = text.color;
        return LeanTween.value(text.gameObject, val => text.color = Color.Lerp(start, to, val), 0, 1f, time);
    }

    public static Gradient Lerp(this Gradient a, Gradient b, float t)
    {
        return Lerp(a, b, t, false, false);
    }

    public static Gradient LerpNoAlpha(this Gradient a, Gradient b, float t)
    {
        return Lerp(a, b, t, true, false);
    }

    public static Gradient LerpNoColor(this Gradient a, Gradient b, float t)
    {
        return Lerp(a, b, t, false, true);
    }

    private static Gradient Lerp(Gradient a, Gradient b, float t, bool noAlpha, bool noColor)
    {
        //list of all the unique key times
        var keysTimes = new List<float>();

        if (!noColor)
        {
            for (int i = 0; i < a.colorKeys.Length; i++)
            {
                float k = a.colorKeys[i].time;
                if (!keysTimes.Contains(k))
                    keysTimes.Add(k);
            }

            for (int i = 0; i < b.colorKeys.Length; i++)
            {
                float k = b.colorKeys[i].time;
                if (!keysTimes.Contains(k))
                    keysTimes.Add(k);
            }
        }

        if (!noAlpha)
        {
            for (int i = 0; i < a.alphaKeys.Length; i++)
            {
                float k = a.alphaKeys[i].time;
                if (!keysTimes.Contains(k))
                    keysTimes.Add(k);
            }

            for (int i = 0; i < b.alphaKeys.Length; i++)
            {
                float k = b.alphaKeys[i].time;
                if (!keysTimes.Contains(k))
                    keysTimes.Add(k);
            }
        }

        GradientColorKey[] clrs = new GradientColorKey[keysTimes.Count];
        GradientAlphaKey[] alphas = new GradientAlphaKey[keysTimes.Count];

        //Pick colors of both gradients at key times and lerp them
        for (int i = 0; i < keysTimes.Count; i++)
        {
            float key = keysTimes[i];
            var clr = Color.Lerp(a.Evaluate(key), b.Evaluate(key), t);
            clrs[i] = new GradientColorKey(clr, key);
            alphas[i] = new GradientAlphaKey(clr.a, key);
        }

        var g = new Gradient();
        g.SetKeys(clrs, alphas);

        return g;
    }
    
    public static LTDescr LeanColor(this SpriteRenderer rend, Color to, float time)
    {
        var start = rend.color;
        return LeanTween.value(rend.gameObject, val => rend.color = Color.Lerp(start, to, val), 0, 1f, time);
    }

    public static bool TryAddComponent<T>(this GameObject gameObject, out T component) where T : Component
    {
        if (gameObject.TryGetComponent(out component))
        {
            return true;
        }
        
        component = gameObject.AddComponent<T>();
        return false;
    }
    
    public static void SetLayerIncludingChildren(this GameObject parent, int layer)
    {
        parent.layer = layer;

        foreach (Transform child in parent.transform)
        {
            SetLayerIncludingChildren(child.gameObject, layer);
        }
    }

    public static Vector3 CameraCanvasPosToWorldPos(this Vector3 camPos, Camera cam)
    {
        var screenPos = RectTransformUtility.WorldToScreenPoint(cam, camPos);
        return cam.ScreenToWorldPoint(screenPos);
    }
    
    public static Vector3 OverlayCanvasPosToWorldPos(this Vector3 camPos, Camera cam)
    {
        return cam.ScreenToWorldPoint(camPos);
    }
    
    public static Vector3 WorldToCanvasPosition(this Canvas canvas, Vector3 worldPosition, Camera camera = null)
    {
        if (camera == null)
        {
            camera = Camera.main;
        }
        var viewportPosition = camera.WorldToViewportPoint(worldPosition);
        return canvas.ViewportToCanvasPosition(viewportPosition);
    }

    public static Vector3 ScreenToCanvasPosition(this Canvas canvas, Vector3 screenPosition)
    {
        var viewportPosition = new Vector3(screenPosition.x / Screen.width,
            screenPosition.y / Screen.height,
            0);
        return canvas.ViewportToCanvasPosition(viewportPosition);
    }

    public static Vector3 ViewportToCanvasPosition(this Canvas canvas, Vector3 viewportPosition)
    {
        var centerBasedViewPortPosition = viewportPosition - new Vector3(0.5f, 0.5f, 0);
        var canvasRect = canvas.GetComponent<RectTransform>();
        var scale = canvasRect.sizeDelta;
        return Vector3.Scale(centerBasedViewPortPosition, scale);
    }
    
    public static LTDescr LeanMoveAnchored(this RectTransform rt, Vector2 to, float time)
    {
        var start = rt.anchoredPosition;
        return LeanTween.value(rt.gameObject, val => rt.anchoredPosition = Vector2.Lerp(start, to, val), 0, 1f, time);
    }
    
    public static List<T> Shuffle<T>(this IEnumerable<T> list, int size)
    {
        var r = new Squirrel3();
        var shuffledList = 
            list.
                Select(x => new { Number = r.Next(), Item = x }).
                OrderBy(x => x.Number).
                Select(x => x.Item).
                Take(size); // Assume first @size items is fine

        return shuffledList.ToList();
    }
    
    public static void AddAfterEach<T>(this List<T> list, Func<T, Boolean> condition, T objectToAdd) 
    {
        foreach (var item in list.Select((o, i) => new { Value = o, Index = i }).Where(p => condition(p.Value)).OrderByDescending(p => p.Index))
        {
            if (item.Index + 1 == list.Count) list.Add(objectToAdd);
            else list.Insert(item.Index + 1, objectToAdd);
        } 
    }
    
    public static float ParseAsFloat(this string input)
    {
        input = input.Replace(',', '.');
        var parsed = float.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out float value);

        if (!parsed) return 0;
        return value;
    }
}