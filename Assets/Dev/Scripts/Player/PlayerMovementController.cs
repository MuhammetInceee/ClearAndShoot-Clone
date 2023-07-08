using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public float sensitivity = 0.01f;
    public float clampValue = 2.5f;
    public float verticalSpeed;

    private Vector2 _firstMousePosition;
    private Vector2 _lastMousePosition;
    private Vector2 _deltaMousePosition;
    private Vector2 _movementVector;
    private Vector3 _movement;
    private GameManager _gameManager;
    
    private void Awake() => _gameManager = GameManager.Instance;
    
    private void Update()
    {
        if(_gameManager.gameStates == GameStates.Wait) return;
        
        if (Input.GetMouseButtonDown(0)) FingerDown();
        if (Input.GetMouseButton(0)) FingerDrag();
        VerticalMovement();
    }

    private void VerticalMovement()
    {
        _movement = new Vector3(0f, 0f, verticalSpeed * Time.deltaTime);
        transform.Translate(-_movement);
    }
    
    private void FingerDown() => _firstMousePosition = Input.mousePosition;

    private void FingerDrag()
    {
        _lastMousePosition = Input.mousePosition;
        _deltaMousePosition = _lastMousePosition - _firstMousePosition;
        _movementVector = _deltaMousePosition * sensitivity;
        var currentPos = transform.position;
        var posX = Mathf.Clamp(currentPos.x + _movementVector.x, -clampValue, clampValue);
        
        transform.position = new Vector3(posX, currentPos.y, currentPos.z);
        _firstMousePosition = _lastMousePosition;
    }
}
