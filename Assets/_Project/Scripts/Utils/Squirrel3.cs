using System;
using Unity.Mathematics;
using UnityEngine;

public class Squirrel3
{
    private const uint NOISE1 = 0xb5297a4d;
    private const uint NOISE2 = 0x68e31da4;
    private const uint NOISE3 = 0x1b56c4e9;
    private const uint CAP = uint.MaxValue;

    private int _n = 0;
    private readonly int _seed;

    public Squirrel3(int seed = 0)
    {
        if (seed == 0) seed = Guid.NewGuid().GetHashCode();
        _seed = seed;
    }

    public float Value()
    {
        ++_n;
        return Rnd(_n, _seed) / (float)CAP;
    }

    public double Next()
    {
        return Lerp(0, double.MaxValue, Value());
    }

    public double Range(double min, double max)
    {
        return Lerp(min, max, Value());
    }
    
    public float Range(float min, float max)
    {
        return (float)Lerp(min, max, Value());
    }
    
    public int Range(int min, int max)
    {
        return (int)Lerp(min, max, Value());
    }
    
    public bool Bool()
    {
        return Value() > 0.5f;
    }

    public Quaternion Angle(Vector3 axis)
    {
        return Quaternion.AngleAxis(Range(0f, 360f), axis);
    }
    
    private static double Lerp(double a, double b, double t)
    {
        return a + (b - a) * math.clamp(t, 0d, 1d);
    }

    private static long Rnd(long n, int seed = 0)
    {
        n *= NOISE1;
        n += seed;
        n ^= n >> 8;
        n += NOISE2;
        n ^= n << 8;
        n *= NOISE3;
        n ^= n >> 8;
        return n % CAP;
    }
}