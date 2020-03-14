using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearMechanics : MonoBehaviour
{
    public int throwSpeed;
    [SerializeField] private BearClaws _bearClaws;
    [SerializeField] private KeyCode _poundKey;
    [SerializeField] private KeyCode _controlPlaneKey;
    [HideInInspector] public Plane _currentPlane;

    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private Sprite _windUp;
    [SerializeField] private Sprite _pound;
    [SerializeField] private Sprite _idle;

    private bool _canAttack;

    private void Awake()
    {
        _bearClaws.Init(this);
        _canAttack = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            _currentPlane = collision.GetComponent<Plane>();
            _currentPlane.isControlling = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_currentPlane == null)
        {
            if (collision.tag == "Enemy")
            {
                _currentPlane = collision.GetComponent<Plane>();
                _currentPlane.isControlling = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            if (_currentPlane != null)
            {
                _currentPlane.isControlling = false;
                _currentPlane = null;
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Pound();
        }

        //if (Input.GetMouseButtonDown(1))
        //{
        //    LatchOnToPlane();
        //}
        //else if (Input.GetMouseButtonUp(1))
        //{
        //    LatchOffOfPlane();
        //}
    }

    public void LatchOnToPlane()
    {
        PlayerSwap.Instance.SetIsPlane(true);
    }

    public void LatchOffOfPlane()
    {
        PlayerSwap.Instance.SetIsPlane(false);
    }

    public void ThrowPilot()
    {
        if (_currentPlane != null)
        {
            _currentPlane.LaunchPilot(throwSpeed);
        }
    }

    public void Pound()
    {
        StartCoroutine(nameof(PoundCo));
    }

    public IEnumerator PoundCo()
    {
        _canAttack = false;
        _sprite.sprite = _windUp;
        yield return new WaitForSeconds(0.1f);
        _bearClaws.Attack();
        _sprite.sprite = _pound;
        ScreenShaker.Instance.ShakeScreen(0.1f, 0.2f);
        yield return new WaitForSeconds(0.1f);
        _bearClaws.FinishAttack();
        _sprite.sprite = _idle;
        BearPlaneStateManager.Instance.UseEnergy(_bearClaws.energyConsumptionAmount);
        _canAttack = true;
    }
}
