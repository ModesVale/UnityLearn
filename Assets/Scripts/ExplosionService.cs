using System.Collections.Generic;
using UnityEngine;

public class ExplosionService : MonoBehaviour
{
    [SerializeField] private float _explosionForce = 5f;
    [SerializeField] private float _explosionRadius = 1f;
    [SerializeField] private float _upwardModifier = 0.5f;

    public void ApplyExplosion(List<GameObject> cubesToAffect, Vector3 explosionCenter)
    {
        foreach (GameObject cube in cubesToAffect)
        {
            if (cube.TryGetComponent<Rigidbody>(out Rigidbody cubeRigidBody))
            {
                cubeRigidBody.AddExplosionForce(_explosionForce, explosionCenter, _explosionRadius, _upwardModifier);
            }
        }
    }
}
