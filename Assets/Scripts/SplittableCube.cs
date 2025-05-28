using System;
using UnityEngine;

public class SplittableCube : MonoBehaviour
{
    [SerializeField][Range(0f, 1f)] private float _splitChance = 1f;
    [SerializeField] private int _minChildren = 2;
    [SerializeField] private int _maxChildren = 6;

    public event Action<SplittableCube> OnClicked;

    public float SplitChance => _splitChance;
    public int MinChildren => _minChildren;
    public int MaxChildren => _maxChildren;
    public Vector3 Position => transform.position;
    public Vector3 Scale => transform.localScale;

    public void HandleClick() => OnClicked?.Invoke(this);

    public void TrySetSplitChance(float newChance)
    {
        _splitChance = Mathf.Clamp01(newChance);
    }
}