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
    [SerializeField] private HoldButton _attackButton;
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

    public void FixedUpdate()
    {
        Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;

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
    }

    private void OnDisable()
    {
        _attackButton.onPointerDown.RemoveListener(Attack);
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
}
