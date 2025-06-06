using System;
using UnityEngine;

public class SplittableCube : MonoBehaviour
{
    [SerializeField][Range(0f, 1f)] private float _splitChance = 1f;
    
    public event Action<SplittableCube> Clicked;

    public float SplitChance => _splitChance;

    public void Initialize(float initialChance, Action<SplittableCube> onClickedHandler, Vector3 initialScale)
    {
        _splitChance = Mathf.Clamp01(initialChance);
        transform.localScale = initialScale;

        Clicked += onClickedHandler;
    }

    private void OnMouseDown()
    {
        HandleClick();
    }

    public void HandleClick()
    {
        Clicked?.Invoke(this);
    }

    public void SetSplitChance(float newChance)
    {
        _splitChance = Mathf.Clamp01(newChance);
    }
}