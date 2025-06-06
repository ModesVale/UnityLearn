using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private SplittableCube _cubePrefab;
    [SerializeField] private int _minChildren = 2;
    [SerializeField] private int _maxChildren = 6;
    [SerializeField] private float _childScaleFactor = 0.5f;
    [SerializeField] private ExplosionService _explosionService;

    private readonly List<SplittableCube> _activeCubes = new List<SplittableCube>();

    private void Start()
    {
        foreach (SplittableCube existingCube in FindObjectsOfType<SplittableCube>())
        {
            SubscribeToCube(existingCube);
        }
    }

    private void SubscribeToCube(SplittableCube cube)
    {
        cube.Clicked += OnCubeClicked;
        _activeCubes.Add(cube);
    }

    private void OnCubeClicked(SplittableCube parentCube)
    {
        if (Random.value <= parentCube.SplitChance)
        {
            List<SplittableCube> children = SpawnChildren(parentCube);

            foreach (SplittableCube childCube in children)
            {
                float newChance = parentCube.SplitChance * _childScaleFactor;
                childCube.SetSplitChance(newChance);
                SubscribeToCube(childCube);
            }

            _explosionService.ExplodeCubes(children, parentCube.transform.position);
        }

        UnsubscribeAndDestroy(parentCube);
    }

    public List<SplittableCube> SpawnChildren(SplittableCube parent)
    {
        int count = Random.Range(_minChildren, _maxChildren+1);
        List<SplittableCube> childrenList = new List<SplittableCube>(count);

        for (int i = 0; i < count; i++)
        {
            SplittableCube newCube = Instantiate(_cubePrefab, parent.transform.position, Quaternion.identity);

            Vector3 childScale = parent.transform.localScale * _childScaleFactor;
            float childInitialChance = parent.SplitChance * _childScaleFactor;

            newCube.Initialize(childInitialChance, OnCubeClicked, childScale);

            if (newCube.TryGetComponent<Renderer>(out Renderer renderer))
            {
                Material materialInstance = new Material(renderer.sharedMaterial);
                materialInstance.color = Random.ColorHSV();
                renderer.material = materialInstance;  
            }

            childrenList.Add(newCube);
        }

        return childrenList;
    }

    private void UnsubscribeAndDestroy(SplittableCube cube)
    {
        cube.Clicked -= OnCubeClicked;
        _activeCubes.Remove(cube); 
        Destroy(cube.gameObject);
    }
}