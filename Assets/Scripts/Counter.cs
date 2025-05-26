using System.Collections;
using TMPro;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _counterText;
    [SerializeField] private float _delayValye = 0.5f;
    [SerializeField] private float _stepValye = 1f;
    [SerializeField] private float _startValye = 0.0f;

    private float _currentValye = 0.0f;
    private bool _isCounterRunning = false;
    private Coroutine _countingCoroutine;

    private void Start()
    {
        DisplayCount(_currentValye); 
        _currentValye = _startValye;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_isCounterRunning)
            {
                StopCounting();
            }
            else
            {
                StartCounting();
            }
        }
    }

    private IEnumerator Count(float delay, float step)
    {
        var wait = new WaitForSeconds(delay);

        while (true)
        {
            _currentValye += step;
            DisplayCount(_currentValye);
            yield return wait;
        }

    }

    private void DisplayCount(float count)
    {
        _counterText.text = count.ToString("");
    }

    private void StartCounting()
    {
        _countingCoroutine = StartCoroutine(Count(_delayValye, _stepValye));
        _isCounterRunning = true;
    }

    private void StopCounting()
    {
        if (_countingCoroutine != null)
        {
            StopCoroutine( _countingCoroutine);
            _countingCoroutine = null;
        }

        _isCounterRunning = false;
    }
}