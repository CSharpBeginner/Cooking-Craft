using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Food : MonoBehaviour
{
    [SerializeField] private Vector3 _size;
    [SerializeField] private float _animationTime;
    [SerializeField] private float _eatingAnimationTime;

    public event UnityAction AnimationFinished;
    public event UnityAction Eaten;

    public Vector3 Size => _size;

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, _size);
    }

    public void PlayAnimation(Transform newParent, Vector3 targetPosition)
    {
        transform.SetParent(newParent);
        transform.rotation = newParent.rotation;
        StartCoroutine(Animate(targetPosition));
    }

    public void PlayEatAnimation()
    {
        StartCoroutine(Eat());
    }

    private IEnumerator Eat()
    {
        float time = 0;

        while (time < _eatingAnimationTime)
        {
            time += Time.deltaTime;
            yield return null;
        }

        Eaten?.Invoke();
    }

    private IEnumerator Animate(Vector3 targetPosition)
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
