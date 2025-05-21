using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleGrow : MonoBehaviour
{
    [SerializeField] private float _growSpeed;

    private void Update()
    {
        transform.localScale += Vector3.one * _growSpeed * Time.deltaTime;
    }
}
