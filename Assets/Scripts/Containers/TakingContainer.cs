using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TakingContainer : MonoBehaviour
{
    [SerializeField] private int _minCapacity;
    [SerializeField] private int _maxCapacity;
    [SerializeField] private Food _prefab;
    [SerializeField] private Vector2Int _plane;
    [SerializeField] private int _capacity;

    private List<Food> _foodList;
    private bool _isAvailable;
    private Food _animatingFood;

    public event UnityAction<int> CapacityChanged;
    public event UnityAction Added;
    public event UnityAction Entered;
    public event UnityAction Exited;

    private void Awake()
    {
        _foodList = new List<Food>();
        _isAvailable = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Bag>(out Bag bag))
        {
            Entered?.Invoke();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        TryTake(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Bag>(out Bag bag))
        {
            Exited?.Invoke();
        }
    }

    private void TryTake(Collider other)
    {
        if (other.TryGetComponent<Bag>(out Bag bag) && _foodList.Count < _capacity && _isAvailable)
        {
            if (bag.TryGetFood(out Food food))
            {
                _isAvailable = false;
                Vector3Int coordinate = Coordinate.GetCoordinates(_foodList.Count, _plane);
                Vector3 targetPosition = Coordinate.GetLocalCoordinates(coordinate, _prefab.Size);
                _animatingFood = food;
                _animatingFood.AnimationFinished += Unlock;
                _animatingFood.PlayAnimation(transform, targetPosition);
                _foodList.Add(food);
                Added?.Invoke();
            }
        }
    }

    public void TryUpdateOrder()
    {
        if (_capacity == 0 && _foodList.Count == 0)
        {
            SetCapacity(Random.Range(_minCapacity, _maxCapacity));
        }
    }

    public bool TryGetFood(out Food food)
    {
        if (_foodList.Count != 0)
        {
            food = _foodList[_foodList.Count - 1];
            _foodList.Remove(food);
            _capacity--;
            return true;
        }

        food = null;
        return false;
    }

    private void Unlock()
    {
        _isAvailable = true;
        _animatingFood.AnimationFinished -= Unlock;
    }

    private void SetCapacity(int value)
    {
        _capacity = value;
        CapacityChanged?.Invoke(value);
    }
}
