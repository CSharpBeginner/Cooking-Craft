using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{
    [SerializeField] private Vector2Int _plane;
    [SerializeField] private int _capacity;
    private List<Food> _foodList;

    public Vector2Int Plane => _plane;
    public IReadOnlyList<Food> FoodList => _foodList;

    private void Awake()
    {
        _foodList = new List<Food>();
    }

    public bool TryAdd(Food food)
    {
        if (_foodList.Count < _capacity)
        {
            _foodList.Add(food);
            return true;
        }

        return false;
    }

    public bool TryGetFood(out Food food)
    {
        if (_foodList.Count != 0)
        {
            food = _foodList[_foodList.Count - 1];
            _foodList.Remove(food);
            return true;
        }

        food = null;
        return false;
    }
}
