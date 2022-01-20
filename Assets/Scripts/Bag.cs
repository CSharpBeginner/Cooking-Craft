using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{
    [SerializeField] private float _animationTime;
    [SerializeField] private Vector3Int _size;

    private List<Food> _foodList;
    private bool isAvailable;

    private int _capacity => _size.x * _size.y * _size.z;

    private void Awake()
    {
        isAvailable = true;
        _foodList = new List<Food>();
    }
    public bool TryAdd(Food food)
    {
        if (_foodList.Count < _capacity && isAvailable)
        {
            isAvailable = false;
            _foodList.Add(food);
            PlayAnimation(food);
            return true;
        }

        return false;
    }

    public bool TryGetFood(out Food food)
    {
        if (_foodList.Count == 0 && isAvailable)
        {
            isAvailable = false;
            food = _foodList[_foodList.Count - 1];
            Remove(food);
            PlayAnimation(food);
            return true;
        }
        food = null;
        return false;
    }

    private void Remove(Food food)
    {
        _foodList.Remove(food);
    }

    private void PlayAnimation(Food food)
    {
        food.transform.SetParent(transform);
        food.transform.rotation = transform.rotation;
        StartCoroutine(Animate(food));
    }

    private IEnumerator Animate(Food food)
    {
        float updateInterval = 0.05f;
        var waiting = new WaitForSeconds(updateInterval);
        Vector3 startPosition = food.transform.localPosition;
        float progress = 0;
        float time = 0f;
        Vector3 targetPosition = new Vector3(0, (_foodList.Count - 1) * food.Size.y, 0);

        while (progress <= 1)
        {
            time += updateInterval;
            progress = time / _animationTime;
            food.transform.localPosition = Vector3.Lerp(startPosition, targetPosition, progress);
            yield return waiting;
        }

        isAvailable = true;
    }
}
