using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcherControllerold : HeroBaseController
{
    //
    // FIELDS
    //

    private Vector3 dashTarget;
    [SerializeField] private AcherDashSkill acherDashSkill;
    [SerializeField] private AcherSpecialSkill acherSpecialSkill;
    [SerializeField] private AcherUltimateSkill acherUltimateSkill;

    // Ultimate effect
    private bool hyperInstict;
    public bool HyperInstict
    {
        get { return hyperInstict; }
        set { hyperInstict = value; }
    }
    //
    // FUNCTIONS
    //

    // Initialize acher data
    //
    protected override void InitilizeValue()
    {
        // Skill flags
        canDash = true;
        canSpecial = true;
        canUltimate = true;

        // Ultimate special effect flag
        hyperInstict = false;

        // Acher health state
        heroHealthState = HeroHealthState.Alive;

        // Acher rigid body
        heroRigidBody = GetComponent<Rigidbody>();
        heroCollider = GetComponent<CapsuleCollider>();
    }
    //
    protected override void InitializeStats()
    {
        heroStats = new HeroStatsOld(heroData);
    }
    //
    protected override void InitializeEffectSystem()
    {
        heroSpecialEffectSystem = new HeroSpecialEffectSystem();
        heroSpecialEffectSystem.hero = this;
    }
    //
    protected override void InitializeBlessingSystem()
    {
        heroBlessingSystem = new HeroBlessingSystem();
    }
    // 
    protected override void InitializeWeaponSystem()
    {
        heroWeaponSystem = new HeroWeaponSystem();
    }
    //
    public override void InitializeDash(float instantiateDashDistance, float instantiateDashSpeed, float instantiateSpecialEffectDuration)
    {
        acherDashSkill.InitializeSkillData(heroData.dashSkill);
        acherDashSkill.DashDistance = instantiateDashDistance;
        acherDashSkill.DashSpeed = instantiateDashSpeed;
    }
    //
    public override void InitializeSpecial()
    {
        acherSpecialSkill.InitializeSkillData(heroData.specialSkill);
    }
    //
    public override void InitializeUltimate()
    {
        acherUltimateSkill.InitializeSkillData(heroData.ultimateSkill);
    }

    // HANDLING ACHER BEHAVIOR 
    // Acher movement function
    protected override void HandleMovement()
    {
        if (heroMovementState == HeroMovementState.Moving)
        {
            //Handle Input
            Vector2 inputVector = GameInput.GetMovementVectorNormalized();
            Vector3 moveDirVector = new Vector3(inputVector.x, 0, inputVector.y);
            //Move
            transform.position += moveDirVector * heroStats.Speed * Time.deltaTime;

            //Rotation
            float rotateSpeed = 10f;
            transform.forward = Vector3.Slerp(transform.forward, moveDirVector, Time.deltaTime * rotateSpeed);
        }
        else if (heroMovementState == HeroMovementState.Dashing)
        {
            transform.position = Vector3.MoveTowards(transform.position, dashTarget, acherDashSkill.DashSpeed * Time.deltaTime);
        }
    }
    // Acher hurt function
    public override void Hurt(float damageTaken)
    {
        if (heroHealthState == HeroHealthState.Alive)
        {
            float damageAfterResistance = 0;
            float damageLeft = damageTaken;

            // Calculate damange taken
            // Take damage 
            if (heroStats.Amor >= damageTaken)
            {
                heroStats.Amor -= damageTaken;
            }
            else
            {
                if (heroStats.Amor > 0)
                {
                    damageLeft -= heroStats.Amor;
                    heroStats.Amor = 0;
                }

                damageAfterResistance = damageLeft - (damageLeft * heroStats.Resistance / 100f);
                heroStats.Health -= damageAfterResistance;
                if (heroStats.Health == 0) Dead();
            }
            //
            canAmorRegen = false;
            // Check if there is already a coroutine
            if (regenCooldownCoroutine != null) StopCoroutine(regenCooldownCoroutine);
            regenCooldownCoroutine = StartCoroutine(AmorRegenCountDown());
        }
    }
    // Acher dead function
    protected override void Dead()
    {
        //
        heroHealthState = HeroHealthState.Dead;
        InvokeOnHeroDead();

        //
        heroRigidBody.useGravity = false;
        heroCollider.enabled = false;
    }

    // HANDLING ACHER SKILLS
    // Handle dash skill
    protected override void HandleDashSkill()
    {
        if (canDash && HeroMovementState == HeroMovementState.Moving)
        {
            // Change the behavior state
            heroMovementState = HeroMovementState.Dashing;

            // Invoke the dash event
            InvokeOnHeroDash();

            // Set up the position for the dash
            dashTarget = transform.position + transform.forward * acherDashSkill.DashDistance;

            //Set the dashing flag
            canDash = false;

            //Reset the skill 
            if (hyperInstict)
            {
                StartCoroutine(ResetDashSkill(acherDashSkill.SkillCooldown / 2));
            }
            else
            {
                StartCoroutine(ResetDashSkill(acherDashSkill.SkillCooldown));
            }
        }
    }
    // Handle special skill
    protected override void HandleSpecialSkill()
    {
        if (canSpecial && HeroMovementState == HeroMovementState.Moving)
        {
            // Change the behavior state
            heroMovementState = HeroMovementState.Casting;

            // Invoke the special event
            InvokeOnHeroSpecial();

            // Set special flag
            canSpecial = false;

            // Reset the skill
            StartCoroutine(ResetSpecialSkill(acherSpecialSkill.SkillCooldown));
        }
    }
    // Handle ultimate skill
    protected override void HandleUltimateSkill()
    {
        if (canUltimate && HeroMovementState == HeroMovementState.Moving)
        {
            // Invoke the ultimate event
            InvokeOnHeroUltimate();

            // Hyper instict
            hyperInstict = true;

            // Set ultimate flag
            canUltimate = false;

            // Reset the skill
            StartCoroutine(ResetUltimateSkill(acherUltimateSkill.SkillCooldown));
            StartCoroutine(HyperInstictCountDown());
        }
    }
    private IEnumerator HyperInstictCountDown()
    {
        hyperInstict = true;
        yield return new WaitForSeconds(10f);
        hyperInstict = false;
    }

    // SUPPORT FUNCTIONS
    // Collision check
    private void OnCollisionEnter(Collision collision)
    {
        if (heroMovementState == HeroMovementState.Dashing)
        {
            if (collision.gameObject.CompareTag("Wall")) heroMovementState = HeroMovementState.Moving;
        }
    }
    private void TestCharacterStats()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            InvokeOnlevelUp();
        }
    }

    private void Awake()
    {
        // Initialize hero data
        InitilizeValue();
        InitializeStats();
        InitializeEffectSystem();
        InitializeBlessingSystem();
        InitializeWeaponSystem();
        InitializeDash(5, 8, 3);
        InitializeSpecial();
        InitializeUltimate();
    }
    private void Start()
    {

        //Subscribe to game input
        GameInput.OnDashAction += HandleDashSkill;
        GameInput.OnSpecialAction += HandleSpecialSkill;
        GameInput.OnUltimateAction += HandleUltimateSkill;

        // Subscribe to upgrade manager
        UpgradeController.Instance.OnSelectWeapon += ReceiveWeapon;
        UpgradeController.Instance.OnSelectBlessing += ReceiveBlessing;

        InitializePrimaryWeapon(heroData.primaryWeapon);
    }

    private void Update()
    {
        if (heroHealthState == HeroHealthState.Alive)
        {
            HandleMovement();
            UpdateSpecialEffect();
            AmorRegen();
            TestCharacterStats();
        }
    }
}

