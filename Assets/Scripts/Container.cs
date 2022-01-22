using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Container : MonoBehaviour
{
    [SerializeField] private Food _prefab;
    [SerializeField] private Vector3Int _size;
    [SerializeField] private float _additionalSize;

    private List<Food> _foodList;
    private BoxCollider _boxCollider;
    private bool _isAvailable;
    private Food _animatingFood;

    private int _capacity => _size.x * _size.y * _size.z;

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

    protected void TryGive(Collider other)
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

    protected void TryTake(Collider other)
    {
        if (other.TryGetComponent<Bag>(out Bag bag) && _isAvailable)
        {
            if (bag.TryGetFood(out Food food))
            {
                _isAvailable = false;
                Vector3 targetPosition = new Vector3(0, _foodList.Count * food.Size.y, 0);
                _animatingFood = food;
                _animatingFood.AnimationFinished += Unlock;
                _animatingFood.PlayAnimation(transform, targetPosition);
                _foodList.Add(food);
            }
        }
    }

    public void Fill()
    {
        for (int i = 0; i < _size.x; i++)
        {
            for (int j = 0; j < _size.y; j++)
            {
                for (int k = 0; k < _size.z; k++)
                {
                    Food newFood = Instantiate(_prefab, transform);
                    newFood.transform.localPosition = new Vector3(i * _prefab.Size.x, j * _prefab.Size.y, k * _prefab.Size.z);
                    _foodList.Add(newFood);
                }
            }
        }

        _boxCollider.size = new Vector3((_size.x + _additionalSize) * _prefab.Size.x, (_size.y + _additionalSize) * _prefab.Size.y, (_size.z + _additionalSize) * _prefab.Size.z);
        _boxCollider.center = new Vector3((_size.x - 1) / 2f * _prefab.Size.x, (_size.y - 1) / 2f * _prefab.Size.y, (_size.z - 1) / 2f * _prefab.Size.z);
    }

    private void Unlock()
    {
        _isAvailable = true;
        _animatingFood.AnimationFinished -= Unlock;
    }
}
