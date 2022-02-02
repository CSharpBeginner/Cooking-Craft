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
        transform.rotation = newParent.rotation;
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
        float updateInterval = 0.05f;
        var waiting = new WaitForSeconds(updateInterval);
        Vector3 startPosition = transform.localPosition;
        float progress = 0;
        float time = 0f;

        while (progress <= 1)
        {
            time += updateInterval;
            progress = time / _animationTime;
            transform.localPosition = Vector3.Lerp(startPosition, targetPosition, progress);
            yield return waiting;
        }

        AnimationFinished?.Invoke();
    }
}
