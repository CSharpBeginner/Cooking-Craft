using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{
    [SerializeField] private Vector3Int _size;

    private List<Food> _foodList;
    private bool _isAvailable;
    private Food _animatingFood;

    private int _capacity => _size.x * _size.y * _size.z;

    private void Awake()
    {
        _isAvailable = true;
        _foodList = new List<Food>();
    }

    private void OnDisable()
    {
        _animatingFood.AnimationFinished -= Unlock;
    }

    public bool TryAdd(Food food)
    {
        if (_foodList.Count < _capacity && _isAvailable)
        {
            _isAvailable = false;
            Vector3 targetPosition = new Vector3(0, _foodList.Count * food.Size.y, 0);
            _animatingFood = food;
            _animatingFood.AnimationFinished += Unlock;
            _animatingFood.PlayAnimation(transform, targetPosition);
            _foodList.Add(food);
            return true;
        }

        return false;
    }

    private void Unlock()
    {
        _isAvailable = true;
        _animatingFood.AnimationFinished -= Unlock;
    }

    public bool TryGetFood(out Food food)
    {
        if (_foodList.Count != 0 && _isAvailable)
        {
            _isAvailable = false;
            food = _foodList[_foodList.Count - 1];
            Remove(food);
            return true;
        }

        food = null;
        return false;
    }

    private void Remove(Food food)
    {
        _foodList.Remove(food);
    }
}
