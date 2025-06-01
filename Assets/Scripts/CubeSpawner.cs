using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private SplittableCube _cubePrefab;

    [SerializeField] private int _minChildren = 2;

    [SerializeField] private int _maxChildren = 6;

    [SerializeField] private float _childScaleFactor = 0.5f;

    public float ChildScaleFactor => _childScaleFactor;

    public List<SplittableCube> SpawnChildren(SplittableCube parent)
    {
        int count = Random.Range(_minChildren, _maxChildren + 1);
        var list = new List<SplittableCube>(count);

        for (int i = 0; i < count; i++)
        {
            SplittableCube child = Instantiate(_cubePrefab, parent.transform.position, Quaternion.identity);

            child.transform.localScale = parent.transform.localScale * _childScaleFactor;

            if (child.TryGetComponent<Renderer>(out var renderer))
            {
                Material material = new Material(renderer.sharedMaterial);
                material.color = Random.ColorHSV();
                renderer.material = material;
            }

            list.Add(child);
        }

        return list;
    }  
}