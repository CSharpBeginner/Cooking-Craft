using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class OrderImage : MonoBehaviour
{
    [SerializeField] private OrderActivator _orderActivator;

    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void Start()
    {
        _image.sprite = _orderActivator.Container.Sprite;
    }
}
