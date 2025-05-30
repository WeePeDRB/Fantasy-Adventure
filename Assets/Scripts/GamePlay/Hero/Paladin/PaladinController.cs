using UnityEngine;

public class PaladinController : HeroBaseController
{
    //
    // FIELDS
    //

    // Paladin skills
    // Dash skill 
    private Vector3 dashTarget; // The position that player will dash to
    [SerializeField] private PaladinDashSkill paladinDashSkill;
    // Special skill
    [SerializeField] private PaladinSpecialSkill paladinSpecialSkill;
    // Ultimate skill
    [SerializeField] private PaladinUltimateSkill paladinUltimateSkill;


    //
    // FUNCTIONS
    //

    // Initialize paldin data

    //
    protected override void InitilizeValue()
    {
        //
        canSpecial = true;
        canUltimate = true;

        //
        heroHealthState = HeroHealthState.Alive;

        //
        heroRigidBody = GetComponent<Rigidbody>();
        heroCollider = GetComponent<CapsuleCollider>();
    }

    // 
    protected override void InitializeStats()
    {
        heroStats = new HeroStats(  heroData.maxHealth, heroData.speed, heroData.level, heroData.maxAmor, 
                                    heroData.resistance, heroData.damageAmplifier, heroData.abilityHaste    );
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
    public override void InitializeDash(float instantiateDashDistance, float instantiateDashSpeed
                                        , float instantiateSpecialEffectDuration)
    {
        paladinDashSkill.InitializeSkillData(heroData.dashSkill);

        paladinDashSkill.DashDistance = instantiateDashDistance;
        paladinDashSkill.DashSpeed = instantiateDashSpeed;
        canDash = true;
    }

    //
    public override void InitializeSpecial()
    {
        paladinSpecialSkill.InitializeSkillData(heroData.specialSkill);
    }

    //
    public override void InitializeUltimate()
    {
        paladinUltimateSkill.InitializeSkillData(heroData.ultimateSkill);
    }

    // HANDLING PALADIN BEHAVIOR
    // Paladin movement function
    protected override void HandleMovement()
    {
        if (heroMovementState == HeroMovementState.Normal)
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
            transform.position = Vector3.MoveTowards(transform.position, dashTarget, paladinDashSkill.DashSpeed * Time.deltaTime);
        }

    }

    // Paladin hurt function
    public override void Hurt(float damageTaken)
    {
        float damageAfterResistance = 0;
        float damageLeft = damageTaken;

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

    // Paladin dead function
    protected override void Dead()
    {
        //
        heroHealthState = HeroHealthState.Dead;
        InvokeOnHeroDead();

        //
        heroRigidBody.useGravity = false;
        heroCollider.enabled = false;
    }

    // HANDLING PALADIN SKILLS
    // Handle the dash skill 
    protected override void HandleDashSkill()
    {
        if ( canDash && HeroMovementState == HeroMovementState.Normal)
        {
            // Change the behavior state
            heroMovementState = HeroMovementState.Dashing;

            // Invoke the dash event
            InvokeOnHeroDash();

            // Set up the position for the dash
            dashTarget = transform.position + transform.forward * paladinDashSkill.DashDistance;
            
            //Set the dashing flag
            canDash = false;

            //Reset the skill and special effect
            StartCoroutine(ResetDashSkill(paladinDashSkill.SkillCooldown));
        }
    }

    // Handle the special skill
    // This function will invoke the event and start the cooldown coroutine
    protected override void HandleSpecialSkill()
    {
        if ( canSpecial && HeroMovementState == HeroMovementState.Normal)
        {
            // Change the behavior state
            heroMovementState = HeroMovementState.Casting;

            // Invoke the special event
            InvokeOnHeroSpecial();

            // Set special flag
            canSpecial = false;

            // Reset the skill
            StartCoroutine(ResetSpecialSkill(paladinSpecialSkill.SkillCooldown));
        }
    }

    // Handle the ultimate skill
    // This function will invoke the event and start the cooldown coroutine
    protected override void HandleUltimateSkill()
    {

        if (canUltimate && HeroMovementState == HeroMovementState.Normal)
            {
                // Change the behavior state
                heroMovementState = HeroMovementState.Casting;
                // Invoke the ultimate event
                InvokeOnHeroUltimate();
                // Set ultimate flag
                canUltimate = false;
                // Reset the skill
                StartCoroutine(ResetUltimateSkill(paladinUltimateSkill.SkillCooldown));
            }
    }

    // SUPPORT FUNCTIONS
    // Collision check
    private void OnCollisionEnter(Collision collision)
    {
        if (heroMovementState == HeroMovementState.Dashing)
        {
            if (collision.gameObject.CompareTag("Wall")) heroMovementState = HeroMovementState.Normal;
            else if (collision.gameObject.CompareTag("Monster"))
            {
                MonsterBaseController monsterBaseController = collision.gameObject.GetComponent<MonsterBaseController>();
                monsterBaseController.Hurt(20f);
            }
        }
        
        if (collision.gameObject.CompareTag("Coin"))
        {
            coin ++ ;
        }

        if (collision.gameObject.CompareTag("ExpGem"))
        {
            heroStats.Exp += 10;
            LevelUp();
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
        InitializeDash(5, 18, 3);
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
        UpgradeManager.Instance.OnSelectWeapon += ReceiveWeapon;
        UpgradeManager.Instance.OnSelectBlessing += ReceiveBlessing;
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