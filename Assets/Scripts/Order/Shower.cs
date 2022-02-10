using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class Shower : MonoBehaviour
{
    [SerializeField] private OrderActivator _orderActivator;
    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        Show(_orderActivator.Container.Nessecity);
        _orderActivator.Container.NessecityChanged += Show;
    }

    private void OnDisable()
    {
        _orderActivator.Container.NessecityChanged -= Show;
    }

    private void Show(int value)
    {
        _text.text = value.ToString();
    }
}
