using UnityEngine;
using UnityEngine.Events;

public class FinalContainer : Container
{
    [SerializeField] private ReceivingContainer _container;

    public event UnityAction Free;

    private void Update()
    {
        TryTake(_container);
    }

    public void TryUpdateOrder()
    {
        _container.TryUpdateOrder();
    }
}
