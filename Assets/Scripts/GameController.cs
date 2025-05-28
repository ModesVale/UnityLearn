using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private CubeSpawner _spawner;
    [SerializeField] private ExplosionService _explosionService;

    private void OnEnable()
    {
        foreach (SplittableCube cube in FindObjectsOfType<SplittableCube>())
        {
            cube.OnClicked += HandleCubeClicked;
        }
    }

    private void OnDisable()
    {
        foreach (SplittableCube cube in FindObjectsOfType<SplittableCube>())
        {
            cube.OnClicked -= HandleCubeClicked;
        }
    }

    private void HandleCubeClicked(SplittableCube parentCube)
    {
        List<GameObject> children = _spawner.SpawnChildren(parentCube);
        _explosionService.ApplyExplosion(children, parentCube.Position);
        Destroy(parentCube.gameObject);

        foreach (GameObject child in children)
        {
            if (child.TryGetComponent<SplittableCube>(out SplittableCube childCube))
            {
                childCube.OnClicked += HandleCubeClicked;
            }
        }
    }
}
