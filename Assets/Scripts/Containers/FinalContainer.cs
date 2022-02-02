using System.Collections.Generic;
using UnityEngine;

public class FinalContainer : MonoBehaviour
{
    [SerializeField] private TakingContainer _container;
    [SerializeField] private Food _prefab;
    [SerializeField] private Vector2Int _plane;
    [SerializeField] private int _capacity;

    private List<Food> _foodList;
    private Food _animatingFood;

    private void Awake()
    {
        _foodList = new List<Food>();
    }

    private void Update()
    {
        TryTake(_container);
    }

    private void OnDisable()
    {
        _animatingFood.AnimationFinished -= Eat;
        _animatingFood.Eaten -= Remove;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Client>(out Client client))
        {
            _container.TryUpdateOrder();
        }
    }

    private void TryTake(TakingContainer other)
    {
        if (_foodList.Count < _capacity)
        {
            if (other.TryGetFood(out Food food))
            {
                Vector3Int coordinate = Coordinate.GetCoordinates(_foodList.Count, _plane);
                Vector3 targetPosition = Coordinate.GetLocalCoordinates(coordinate, _prefab.Size);
                _animatingFood = food;
                _animatingFood.AnimationFinished += Eat;
                _animatingFood.Drag(transform, targetPosition);
                _foodList.Add(food);
            }
        }
    }

    private void Eat()
    {
        _animatingFood.AnimationFinished -= Eat;
        _animatingFood.Eat();
        _animatingFood.Eaten += Remove;
    }

    private void Remove()
    {
        _animatingFood.Eaten -= Remove;
        _foodList.Remove(_animatingFood);
        Destroy(_animatingFood.gameObject);
    }
}
