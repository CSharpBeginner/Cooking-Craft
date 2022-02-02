using UnityEngine;
using System.Collections;

public class OrderPanel : MonoBehaviour
{
    [SerializeField] private OrderActivator _orderActivator;
    [SerializeField] private float _sizeMultiplier;
    [SerializeField] private float _step;
    [SerializeField] private float _animationTimeMultiplier;
    [SerializeField] private AnimationCurve _animationCurve;

    private Vector3 _originalScale;
    private Coroutine _currentCoroutine;

    private void Awake()
    {
        _originalScale = transform.localScale;
    }

    private void OnEnable()
    {
        _orderActivator.Container.Entered += Increase;
        _orderActivator.Container.Exited += Decrease;

        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
        }

        _currentCoroutine = StartCoroutine(Appear());
    }

    private void OnDisable()
    {
        _orderActivator.Container.Entered -= Increase;
        _orderActivator.Container.Exited -= Decrease;
    }

    private void Decrease()
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
        }

        _currentCoroutine = StartCoroutine(ChangeSize(1));
    }

    private void Increase()
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
        }

        _currentCoroutine = StartCoroutine(ChangeSize(_sizeMultiplier));
    }

    private IEnumerator ChangeSize(float multiplier)
    {
        Vector3 target = _originalScale * multiplier;

        while (transform.localScale != target)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, target, _step * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator Appear()
    {
        float time = _animationCurve[0].time;
        float endTime = _animationCurve[_animationCurve.length - 1].time;

        while (time < endTime)
        {
            time += Time.deltaTime / _animationTimeMultiplier;
            transform.localScale = _originalScale * _animationCurve.Evaluate(time);
            yield return null;
        }
    }
}
