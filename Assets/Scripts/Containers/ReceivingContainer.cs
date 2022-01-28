using UnityEngine;

public class ReceivingContainer : Container
{
    [SerializeField] private int _minCapacity;
    [SerializeField] private int _maxCapacity;

    private void OnTriggerStay(Collider other)
    {
        TryTake(other);
    }

    public void TryUpdateOrder()
    {
        if (Capacity == 0 && FoodList.Count == 0)
        {
            SetCapacity(Random.Range(_minCapacity, _maxCapacity));
        }
    }
}
