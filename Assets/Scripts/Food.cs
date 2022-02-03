using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class Food : MonoBehaviour
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private Vector3 _size;
    [SerializeField] private float _animationTime;
    [SerializeField] private float _eatingAnimationTime;
    [SerializeField] private AnimationCurve _flyCurve;
    [SerializeField] private float _flyHeightMultiplier;

    private AudioSource _audioSource;

    public event UnityAction AnimationFinished;
    public event UnityAction Eaten;

    public Vector3 Size => _size;
    public Sprite Sprite => _sprite;

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, _size);
    }

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Fly(Transform newParent, Vector3 targetPosition)
    {
        _audioSource.Play();
        Drag(newParent, targetPosition);
    }

    public void Drag(Transform newParent, Vector3 targetPosition)
    {
        transform.SetParent(newParent);
        StartCoroutine(Flying(targetPosition));
    }

    public void Eat()
    {
        StartCoroutine(Eating());
    }

    private IEnumerator Eating()
    {
        float time = 0;

        while (time < _eatingAnimationTime)
        {
            time += Time.deltaTime;
            yield return null;
        }

        Eaten?.Invoke();
    }

    private IEnumerator Flying(Vector3 targetPosition)
    {
        Vector3 startPosition = transform.localPosition;
        Quaternion startRotation = transform.localRotation;
        float progress = 0;
        float time = 0f;
        float flyCurveAnimationTime = _flyCurve[_flyCurve.length - 1].time - _flyCurve[0].time;

        while (progress <= 1)
        {
            time += Time.deltaTime;
            progress = time / _animationTime;
            transform.localRotation = Quaternion.Lerp(startRotation, Quaternion.identity, progress);
            transform.localPosition = Vector3.Lerp(startPosition, targetPosition, progress) + new Vector3(0, _flyCurve.Evaluate(_flyCurve[0].time + progress * flyCurveAnimationTime) * _flyHeightMultiplier, 0);
            yield return null;
        }

        AnimationFinished?.Invoke();
    }
}
