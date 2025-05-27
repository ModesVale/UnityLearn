using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Counter : MonoBehaviour
{
    [SerializeField] private float _delayValye = 0.5f;
    [SerializeField] private float _stepValye = 1f;
    [SerializeField] private float _startValye = 0.0f;

    private float _currentValye = 0.0f;
    private Coroutine _countingCoroutine;

    public event UnityAction<float> ValueChanged;

    private void Start()
    { 
        _currentValye = _startValye;
        ValueChanged?.Invoke(_currentValye);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_countingCoroutine == null)
            {
                _countingCoroutine = StartCoroutine(Count(_delayValye, _stepValye));
            }
            else
            {
                StopCounting();
            }
        }
    }

    private IEnumerator Count(float delay, float step)
    {
        var wait = new WaitForSeconds(delay);

        while (true)
        {
            _currentValye += step;
            ValueChanged?.Invoke(_currentValye);
            yield return wait;
        }

    }

    private void StopCounting()
    {
        if (_countingCoroutine != null)
        {
            StopCoroutine( _countingCoroutine);
            _countingCoroutine = null;
        }
    }
}