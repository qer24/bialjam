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
        else
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
    
    public static float ParseAsFloat(this string input)
    {
        input = input.Replace(',', '.');
        var parsed = float.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out float value);

        if (!parsed) return 0;
        return value;
    }
}