using UnityEngine;
using UnityEngine.Events;

public class Inputer : MonoBehaviour
{
    [SerializeField] private float _step;
    [SerializeField] private float _radius;

    private Vector2 _screenPoint;
    private Vector2 _centerPoint;
    private Vector2 _direction;
    private float _share;

    public event UnityAction TouchStarted;
    public event UnityAction TouchFinished;

    private float _radiusMagnitude => _radius * _radius;

    public Vector2 CenterPoint => _centerPoint;
    public float Share => _share;
    public Vector2 Direction => _direction;
    public float Radius => _radius;


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _centerPoint = Input.mousePosition;
            TouchStarted?.Invoke();
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 mousePosition = Input.mousePosition;
            _screenPoint = mousePosition;

            if (_screenPoint != _centerPoint)
            {
                Calculate();
            }
            else
            {
                _share = 0;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            _share = 0;
            TouchFinished?.Invoke();
        }
    }

    private void Calculate()
    {
        Vector2 translation = _screenPoint - _centerPoint;
        _direction = translation.normalized;
        float magnitude = translation.sqrMagnitude;
        _share = Mathf.Clamp(magnitude, 0, _radiusMagnitude) / _radiusMagnitude;

        if (magnitude > _radiusMagnitude)
        {
            Vector2 targetCenter = _screenPoint - _direction * _radius;
            _centerPoint = Vector2.MoveTowards(_centerPoint, targetCenter, _step * Time.deltaTime);
        }
    }
}
