using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    [SerializeField] private Food _prefab;
    [SerializeField] private Vector3Int _size;

    private List<Food> _foodList;

    private int _capacity => _size.x * _size.y * _size.z;

    private void Start()
    {
        Instatiate();
    }

    private void OnTriggerStay(Collider other)
    {

        if (_foodList.Count != 0 && other.TryGetComponent<Bag>(out Bag bag))
        {
            Food food = _foodList[_foodList.Count - 1];
            if (bag.TryAdd(food))
            {
                _foodList.Remove(food);
            }
        }
    }

    private void Instatiate()
    {
        _foodList = new List<Food>();

        for (int i = 0; i < _size.x; i++)
        {
            for (int j = 0; j < _size.y; j++)
            {
                for (int k = 0; k < _size.z; k++)
                {
                    Food newFood = Instantiate(_prefab, transform);
                    newFood.transform.localPosition = new Vector3(i * _prefab.Size.x, j * _prefab.Size.y, k * _prefab.Size.z);
                    newFood.transform.localRotation = transform.rotation;
                    _foodList.Add(newFood);
                }
            }
        }
    }

    private void TryAdd(Food food)
    {
        if (_foodList.Count < _capacity)
        {
            _foodList.Add(food);
        }
    }
}
