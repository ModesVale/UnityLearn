using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiCube : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _growSpeed;

    private void Update()
    {
        transform.Translate(Vector3.forward * _moveSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up * _rotateSpeed * Time.deltaTime);
        transform.localScale += Vector3.one * _growSpeed * Time.deltaTime;
    }
}
