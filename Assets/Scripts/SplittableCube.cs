using System.Collections;
using UnityEngine;

public class SplittableCube : MonoBehaviour
{
    [SerializeField][Range(0f, 1f)] private float _splitChance = 1f;
    [SerializeField] private int _minChildren = 2;
    [SerializeField] private int _maxChildren = 6;
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private float _explosionForce = 5f;
    [SerializeField] private float _explosionRadius = 1f;
    [SerializeField] private float _explosionUpwardModifier = 0.5f;

    private void OnMouseDown()
    {
        TrySplit();
    }

    private void TrySplit()
    {
        if (Random.value <= _splitChance)
        {
            CreateChildrenCubes();
        }

        Destroy(gameObject);
    }

    private void CreateChildrenCubes()
    {
        int count = Random.Range(_minChildren, _maxChildren + 1);

        for (int i = 0; i < count; i++)
        {
            GameObject newCube = Instantiate(_cubePrefab, transform.position, Quaternion.identity);

            newCube.transform.localScale = transform.localScale * 0.5f;

            Renderer newCubeRenderer = newCube.GetComponent<Renderer>();
            newCubeRenderer.material = new Material(newCubeRenderer.sharedMaterial);
            newCubeRenderer.material.color = Random.ColorHSV();

            Rigidbody newCubeRigidBody = newCube.GetComponent<Rigidbody>();
            newCubeRigidBody.useGravity = true;

            SplittableCube newCubeSplit = newCube.GetComponent<SplittableCube>();
            newCubeSplit._splitChance = _splitChance * 0.5f;
        }

        ExplodeChildren();
    }

    private void ExplodeChildren()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _explosionRadius);

        foreach (Collider hit in hitColliders)
        {
            Rigidbody hitRigidBody = hit.attachedRigidbody;

            if (hitRigidBody != null && hit.gameObject.CompareTag("Splittable"))
            {
                hitRigidBody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius, _explosionUpwardModifier);
            }
        }
    }
}