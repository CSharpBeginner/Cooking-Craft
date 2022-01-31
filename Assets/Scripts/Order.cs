using UnityEngine;
using TMPro;
using System;
using System.Collections;

public class Order : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private TakingContainer _container;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private GameObject _dialog;
    [SerializeField] private float _sizeMultiplier;
    [SerializeField] private float _step;
    [SerializeField] private float _animationTimeMultiplier;
    [SerializeField] private AnimationCurve _animationCurve;

    private Vector3 _originalScale;
    private Coroutine _currentCoroutine;

    private int _count;

    private void Awake()
    {
        _originalScale = _dialog.transform.localScale;
    }

    private void Start()
    {
        transform.LookAt(2 * transform.position - _camera.transform.position);
    }

    private void OnEnable()
    {
        _container.Added += Decrease;
        _container.CapacityChanged += Reset;
        _container.Entered += IncreaseDialog;
        _container.Exited += DecreaseDialog;
    }

    private void OnDisable()
    {
        _container.Added -= Decrease;
        _container.CapacityChanged -= Reset;
        _container.Entered -= IncreaseDialog;
        _container.Exited -= DecreaseDialog;
    }

    private void DecreaseDialog()
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
        }

        _currentCoroutine = StartCoroutine(ChangeSize(1));
    }

    private void IncreaseDialog()
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

        while (_dialog.transform.localScale != target)
        {
            _dialog.transform.localScale = Vector3.MoveTowards(_dialog.transform.localScale, target, _step * Time.deltaTime);
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
            _dialog.transform.localScale = _originalScale * _animationCurve.Evaluate(time);
            yield return null;
        }
    }

    private void Decrease()
    {
        _count--;
        _text.text = _count.ToString();
    }

    private void Reset(int value)
    {
        _count = value;
        _text.text = _count.ToString();
        _dialog.SetActive(true);

        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
        }

        _currentCoroutine = StartCoroutine(Appear());
    }
}
