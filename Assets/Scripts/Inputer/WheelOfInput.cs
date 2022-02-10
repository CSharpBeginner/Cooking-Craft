using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class WheelOfInput : MonoBehaviour
{
    [SerializeField] private Inputer _inputer;

    private RectTransform _rectTransform;
    private Vector2 _screenCenter;

    private void Awake()
    {
        float diametr = _inputer.Radius * 2;
        _rectTransform = GetComponent<RectTransform>();
        _rectTransform.sizeDelta = new Vector2(diametr, diametr);
        _screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
    }

    private void OnEnable()
    {
        _inputer.TouchFinished += Deactivate;
    }

    private void OnDisable()
    {
        _inputer.TouchFinished -= Deactivate;
    }

    private void Update()
    {
        _rectTransform.transform.localPosition = _inputer.CenterPoint - _screenCenter;
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
