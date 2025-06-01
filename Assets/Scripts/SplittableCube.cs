using System;
using UnityEngine;

public class SplittableCube : MonoBehaviour
{
    [SerializeField][Range(0f, 1f)] private float _splitChance = 1f;
    
    public float SplitChance => _splitChance;

    public static event Action<SplittableCube> CubeCliked;

    public void SetSplitChance(float newChance)
    {
        _splitChance = Mathf.Clamp01(newChance);
    }

    public static void RaiseCubeCliked(SplittableCube cube)
    {
        CubeCliked?.Invoke(cube);
    }
}