using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerPlane : Plane
{
    public float flyingEnergyConsumptionRate;
    public float shootingEnergyConsumptionRate;

    private bool _canFire;
    private float _shouldFire;

    [SerializeField] private BearClaws _bearClaws;
    [SerializeField] private KeyCode _poundKey;

    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private Sprite _windUp;
    [SerializeField] private Sprite _pound;
    [SerializeField] private Sprite _idle;

    private bool _canAttack;
    private float _shouldAttack;

    [SerializeField] private List<AudioClip> _growls;
    [SerializeField] private AudioSource _growlAudio;

    private void Awake()
    {
        base.Awake();
        _canFire = true;
        _bearClaws.Init(this);
        _canAttack = true;
    }

    private void Update()
    {
        BearPlaneStateManager.Instance.UseEnergy(flyingEnergyConsumptionRate);

        Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Move(direction);

        //if (Input.GetMouseButtonDown(0))
        //{
        //    Maul();
        //    //BearPlaneStateManager.Instance.UseEnergy(shootingEnergyConsumptionRate);
        //}
        //else if (Input.GetMouseButtonDown(1))
        //{
        //    FireProjectile(Vector2.right);
        //    BearPlaneStateManager.Instance.UseEnergy(shootingEnergyConsumptionRate);
        //}
        DetermineAttackController();

        CreateThrustParticles();
    }

    void DetermineAttackController()
    {
        _shouldFire = Input.GetAxis("AttackTrigger");

        if (_shouldFire == 1 && _canFire)
        {
            FireProjectile(Vector2.right);
            BearPlaneStateManager.Instance.UseEnergy(shootingEnergyConsumptionRate);
            StartCoroutine(FireBuffer());
        }
    }

    IEnumerator FireBuffer()
    {
        _canFire = false;
        yield return new WaitForSeconds(fireRate);
        _canFire = true;
    }

    //public void HandlePhoneInput(Vector2 initialTouchPosition, Vector2 currentTouchPosition)
    //{
    //    print("initialTouchPosition: " + initialTouchPosition);
    //    print("currentTouchPosition: " + currentTouchPosition);

    //    Vector2 directionRelativeToPhoneMiddle = currentTouchPosition - initialTouchPosition;
    //    Move(directionRelativeToPhoneMiddle.normalized);
    //}

    public void HandleJoystickInput(Vector2 joystickDirection)
    {
        Move(joystickDirection);
    }

    public void Maul()
    {
        if (_canAttack)
        {
            StartCoroutine(nameof(MaulCo));
            int rand = Random.Range(0, 4);
            _growlAudio.clip = _growls[rand];
            _growlAudio.Play();
        }
    }

    public IEnumerator MaulCo()
    {
        _canAttack = false;
        _sprite.sprite = _windUp;
        yield return new WaitForSeconds(0.1f);
        _bearClaws.Attack();
        _sprite.sprite = _pound;
        yield return new WaitForSeconds(0.1f);
        _bearClaws.FinishAttack();
        _sprite.sprite = _idle;
        BearPlaneStateManager.Instance.UseEnergy(_bearClaws.energyConsumptionAmount);
        _canAttack = true;
    }
}
