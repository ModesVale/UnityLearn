using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _cubePrefab;

    [SerializeField, Range(0f, 1f)] private float _scaleFactor = 0.5f;

    public List<GameObject> SpawnChildren(SplittableCube parent)
    {
        var spawned = new List<GameObject>();

        if (Random.value > parent.SplitChance)
        {
            return spawned;
        }

        int count = Random.Range(parent.MinChildren, parent.MaxChildren + 1);

        for (int i = 0; i < count; i++)
        {
            GameObject child = Instantiate(_cubePrefab, parent.Position, Quaternion.identity);
            child.transform.localScale = parent.Scale * _scaleFactor;

            if (child.TryGetComponent<Renderer>(out Renderer childRenderer))
            {
                Material material = new Material(childRenderer.sharedMaterial);
                material.color = Random.ColorHSV();
                childRenderer.material = material;
            }

            spawned.Add(child);

            if(child.TryGetComponent<SplittableCube>(out SplittableCube childCube))
            {
                childCube.TrySetSplitChance(parent.SplitChance * _scaleFactor);
            }
        }

        return spawned;
    }
}