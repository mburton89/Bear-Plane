using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchInputManager : MonoBehaviour
{
    private PlayerCharacter _playerCharacter;
    private PlayerPlane _playerPlane;
    private BearPlaneStateManager _bearPlaneStateManager;
    private BearMechanics _bearMechanics;
    [SerializeField] private HoldButton _jumpButton;
    [SerializeField] private HoldButton _attackButton;
    //[SerializeField] private GameObject _touchCircle;
    public VariableJoystick variableJoystick;

    private Vector2 _initialTouchPosition;
    private Vector2 _currentTouchPosition;

    void Start()
    {
        _playerCharacter = FindObjectOfType<PlayerCharacter>();
        _playerPlane = FindObjectOfType<PlayerPlane>();
        _bearPlaneStateManager = FindObjectOfType<BearPlaneStateManager>();
        _bearMechanics = FindObjectOfType<BearMechanics>();
    }

    private void Update()
    {
        //if (Input.touchCount > 0)
        //{
        //    Touch touch = Input.GetTouch(0);

        //    if (touch.position.x > 700) return;

        //    if (touch.phase == TouchPhase.Began)
        //    {
        //        _initialTouchPosition = touch.position;
        //        _touchCircle.transform.position = _initialTouchPosition;
        //        _touchCircle.SetActive(true);
        //    }
        //    else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
        //    {
        //        _currentTouchPosition = touch.position;
        //    }

        //    if (_playerCharacter.enabled == true)
        //    {
        //        _playerCharacter.HandlePhoneInput(_initialTouchPosition, _currentTouchPosition);
        //    }
        //    else if (_playerPlane.enabled == true)
        //    {
        //        _playerPlane.HandlePhoneInput(_initialTouchPosition, _currentTouchPosition);
        //    }
        //}
        //else
        //{
        //    _touchCircle.SetActive(false);
        //}

        //if (_attackButton.isPressed)
        //{
        //    if (_playerCharacter != null)
        //    {
        //        //maul
        //    }
        //    else if (_playerPlane != null)
        //    {
        //        FireProjectile();
        //    }
        //}
    }

    public void FixedUpdate()
    {
        Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;

       

        //rb.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);

        if (!_bearPlaneStateManager.isPlane)
        {
            print("direction: " + direction);
            _playerCharacter.HandleJoystickInput(direction);
        }
        else
        {
            _playerPlane.HandleJoystickInput(new Vector2(direction.x, direction.z));
        }
    }

    private void OnEnable()
    {
        _attackButton.onPointerDown.AddListener(Attack);
        _jumpButton.onPointerDown.AddListener(HandleJumpButtonPressed);
        _jumpButton.onPointerUp.AddListener(HandleJumpButtonLetGo);
    }

    private void OnDisable()
    {
        _attackButton.onPointerDown.RemoveListener(Attack);
        _jumpButton.onPointerDown.RemoveListener(HandleJumpButtonPressed);
        _jumpButton.onPointerUp.RemoveListener(HandleJumpButtonLetGo);
    }

    void Attack()
    {
        if (_bearPlaneStateManager.isPlane)
        {
            _playerPlane.FireProjectile(Vector2.right);
        }
        else
        {
            _bearMechanics.Pound();
        }
    }

    void HandleJumpButtonPressed()
    {
        _bearPlaneStateManager.HandleJumpButtonPressed(true);
    }

    void HandleJumpButtonLetGo()
    {
        _bearPlaneStateManager.HandleJumpButtonPressed(false);
    }
}
