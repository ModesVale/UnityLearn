using TMPro;
using UnityEngine;

public class CounterView : MonoBehaviour
{
    [SerializeField] private Counter _counter;
    [SerializeField] private TextMeshProUGUI _counterText;

    private void Start()
    {
        _counter.ValueChanged += UpdateText;
    }

    private void OnDestroy()
    {
        _counter.ValueChanged -= UpdateText;
    }

    private void UpdateText(float value)
    {
        _counterText.text = value.ToString();
    }
}
