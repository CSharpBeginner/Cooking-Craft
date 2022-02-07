using UnityEngine;

public class OrderActivator : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private TakingContainer _container;
    [SerializeField] private OrderPanel _orderPanel;

    public TakingContainer Container => _container;

    private void Start()
    {
        transform.LookAt(transform.position + _camera.transform.forward);
    }

    private void OnEnable()
    {
        _container.NessecitySetted += ActivatePanel;
    }

    private void OnDisable()
    {
        _container.NessecitySetted -= ActivatePanel;
    }

    private void ActivatePanel()
    {
        _orderPanel.gameObject.SetActive(true);
    }
}
