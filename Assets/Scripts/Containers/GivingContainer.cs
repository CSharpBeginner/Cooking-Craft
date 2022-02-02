using System.Collections.Generic;
using UnityEngine;

public class GivingContainer : MonoBehaviour
{
    [SerializeField] private Food _prefab;
    [SerializeField] private Vector2Int _plane;
    [SerializeField] private float _additionalSize;
    [SerializeField] private int _capacity;

    private List<Food> _foodList;
    private BoxCollider _boxCollider;
    private bool _isAvailable;
    private Food _animatingFood;

    private void Awake()
    {
        _foodList = new List<Food>();
        _isAvailable = true;
        _boxCollider = GetComponent<BoxCollider>();
    }

    private void OnDisable()
    {
        _animatingFood.AnimationFinished -= Unlock;
    }

    private void OnTriggerStay(Collider other)
    {
        TryGive(other);
    }

    public void Fill()
    {
        for (int i = 0; i < _capacity; i++)
        {
            Vector3Int coordinate = Coordinate.GetCoordinates(i, _plane);
            Food newFood = Instantiate(_prefab, transform);
            newFood.transform.localPosition = Coordinate.GetLocalCoordinates(coordinate, _prefab.Size);
            _foodList.Add(newFood);
        }

        _boxCollider.size = new Vector3((_plane.y + _additionalSize) * _prefab.Size.x, (_capacity / (_plane.x * _plane.y) + _additionalSize) * _prefab.Size.y, (_plane.x + _additionalSize) * _prefab.Size.z);
        _boxCollider.center = new Vector3((_plane.y - 1) / 2f * _prefab.Size.x, ((_capacity - 1) / (_plane.x * _plane.y)) / 2f * _prefab.Size.y, (_plane.x - 1) / 2f * _prefab.Size.z);
    }

    private void TryGive(Collider other)
    {
        if (_foodList.Count != 0 && other.TryGetComponent<Bag>(out Bag bag) && _isAvailable)
        {
            Food food = _foodList[_foodList.Count - 1];

            if (bag.TryAdd(food))
            {
                _isAvailable = false;
                Vector3Int coordinate = Coordinate.GetCoordinates(bag.FoodList.Count - 1, bag.Plane);
                Vector3 targetPosition = Coordinate.GetLocalCoordinates(coordinate, _prefab.Size);
                _animatingFood = food;
                _animatingFood.AnimationFinished += Unlock;
                _animatingFood.Fly(bag.transform, targetPosition);
                _foodList.Remove(food);
            }
        }
    }

    private void Unlock()
    {
        _isAvailable = true;
        _animatingFood.AnimationFinished -= Unlock;
    }
}
