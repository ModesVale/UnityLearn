using System.Collections.Generic;
using UnityEngine;

public class CubeSplitResponder : MonoBehaviour
{
    [SerializeField] private CubeSpawner _spawner;
    [SerializeField] private ExplosionService _explosionService;

    private void OnEnable()
    {
        SplittableCube.CubeCliked += OnAnyCubeClicked;
    }

    private void OnDisable()
    {
        SplittableCube.CubeCliked += OnAnyCubeClicked;
    }

    private void OnAnyCubeClicked(SplittableCube parentCube)
    {
        if (Random.value <= parentCube.SplitChance)
        {
            List<SplittableCube> children = _spawner.SpawnChildren(parentCube);

            foreach (SplittableCube child in children)
            {
                float reducedChance = parentCube.SplitChance * _spawner.ChildScaleFactor;
                child.SetSplitChance(reducedChance);
            }

            _explosionService.ExplodeCubes(children, parentCube.transform.position);

        }

        Destroy(parentCube.gameObject);
    }
}
