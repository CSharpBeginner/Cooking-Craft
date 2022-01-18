using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Inputer _inputer;
    [SerializeField] private WheelOfInput _wheelOfInput;
    [SerializeField] private bool _showInputWheel;

    private void OnEnable()
    {
        if (_showInputWheel)
        {
            _inputer.TouchStarted += ActivateMovementCircle;
        }
    }

    private void OnDisable()
    {
        _inputer.TouchStarted -= ActivateMovementCircle;
    }

    private void ActivateMovementCircle()
    {
        _wheelOfInput.gameObject.SetActive(true);
    }
}
