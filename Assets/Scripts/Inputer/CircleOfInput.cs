using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class CircleOfInput : MonoBehaviour
{
    [SerializeField] private Inputer _inputer;

    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void OnDisable()
    {
        Reset();
    }

    private void Update()
    {
        _rectTransform.anchoredPosition = _inputer.Direction * _inputer.Share * _inputer.Radius;
    }

    private void Reset()
    {
        _rectTransform.anchoredPosition = Vector2.zero;
    }
}
