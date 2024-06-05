using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{

    public delegate void ModifyHealthbar(float health);
    public static event ModifyHealthbar SetHealth;
    public static event ModifyHealthbar SetMaxHealth;

    public delegate void ManageDealDamage(float damage = 0);
    public static event ManageDealDamage ManageWeapon;
    public static event ManageDealDamage ManageSpecial;

    public delegate void HealthRelatedEvent();
    public static event HealthRelatedEvent PlayerDead;
    public static event HealthRelatedEvent PlayerDefiedDeath;
    public static event HealthRelatedEvent PlayerLowerThanPercentage;
    public static event HealthRelatedEvent PlayerHigherThanPercentage;
    public static event HealthRelatedEvent CheckReduceReceivedDamage;
    public static event HealthRelatedEvent ReceivedDamage;


    [SerializeField] private float maxHealth;
    private PlayerStatsObtainer stats;
    private float currentHealth;

    private float attackDamage, specialAttackDamage;
    private bool canAttack, canSpecial;
    private float attackCooldown, specialAttackCooldown;
    private bool attackPressed, specialPressed;

    Animator animator;
    int isAttackingHash;

    private int timesCanDefyDeath = 0;
    private bool reduceReceivedDamage = false;
    private bool invulnerable = false;


    private void Awake()
    {
        canAttack = false;
        canSpecial = false;
    }


    void Start()
    {
        DeathDefianceAbility.DefyDeath += SetDeathDefiance;
        ThickSkinAbility.IncreaseMaxHealth += IncreaseMaxHealth;

        PermanentTradeoff.RestoreMaxHealth += RestoreMaxHealth;
        newDeadState.RespawnPlayer += FillHealth;
        HealthRestorer.MedpackHeal += RestoreHealth;
        RegenerateAbility.AddVitality += RestoreHealth;
        HealInteractable.InteractedHeal += RestoreHealth;
        WeaponPrompt.ChangeWeaponStats += ChangeStatsForWeapon;
        EnemyDealDamage.PlayerReceiveDamage += TakeDamage;
        Bullet.Hit += TakeDamage;
        DashDealDamage.PlayerReceiveDamage += TakeDamage;

        DoubleMaxHealth.DoubleHealth += ModifyMaxHealth;
        DoubleMaxHealth.ResetHealth += ResetMaxHealth;

        KnifeSpecial.BoostStats += ModifyMaxHealth;
        KnifeSpecial.ResetStats += ResetMaxHealth;

        LowHPDamage.ReduceDamage += ReduceReceivedDamage;
        Tradeoff.ReduceCurrentHealth += ReduceToPercentage;
        PermanentTradeoff.ReduceMaxHealth += ReduceMaxHealth;

        InvulnerabilityNPCAbility.MakeInvulnerable += Invulnerable;
        InvulnerabilityNPCAbility.MakeVulnerable += Vulnerable;

        HealNPCAbility.AddHealth += RestoreHealth;

        BossMeleeDamage.BossDamage += TakeDamage;
        BossAreaDamage.BossDamage += TakeDamage;

        animator = GetComponent<Animator>();
        isAttackingHash = Animator.StringToHash("isAttacking");

        stats = GetComponent<PlayerStatsObtainer>();
        maxHealth = stats.ObtainCurrentMaxHealth();

        currentHealth = maxHealth;
        if (SetHealth != null)
        {
            SetHealth(currentHealth);
        }
    }


    void Update()
    {
        if (attackPressed && canAttack)
        {
            handleAttack();
        }

        if (specialPressed && canSpecial) {
            handleSpecial();
        }

        //DEBUG***************************
        if (Input.GetKeyDown("p")) {
            TakeDamage(90);
        }

        if (Input.GetKeyDown("i")) {
            DebugFillHealth();
            Debug.Log("I have removed godmode from this debug key.");
        }
        //DEBUG***************************
    }

    private void ManageWeaponDamageAbility() {
        if (ManageWeapon != null)
        {
            ManageWeapon(attackDamage);
        }
    }

    private void ManageWeaponSpecialDamage() {
        if (ManageSpecial != null)
        {
            ManageSpecial(specialAttackDamage);
        }
    }

    void handleAttack()
    {
        canAttack = false;
        animator.SetTrigger("isAttacking");
    }

    void handleSpecial()
    {
        canSpecial = false;
        animator.SetTrigger("Special");
    }


    private void ChangeStatsForWeapon(float damage, float cooldown, float specialDamage, float specialCooldown) {
        attackDamage = damage;
        attackCooldown = cooldown;
        specialAttackDamage = specialDamage;
        specialAttackCooldown = specialCooldown;
        canAttack = true;
        canSpecial = true;
    }


    IEnumerator resetAttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    IEnumerator resetSpecialCooldown()
    {
        yield return new WaitForSeconds(specialAttackCooldown);
        canSpecial = true;
    }

    private void ReduceReceivedDamage() {
        reduceReceivedDamage = true;
    }

    private void ReduceToPercentage(int percentage) {
        currentHealth = (percentage / 100.0f) * currentHealth;


        SetHealth?.Invoke(currentHealth);
    }

    private void ReduceMaxHealth(int amount) {
        //For PermanentTradeoff
        stats.SetTemporaryMaxHealth(stats.ObtainTemporaryMaxHealth() / amount);
        maxHealth = stats.ObtainTemporaryMaxHealth();
        currentHealth = currentHealth / amount;

        SetMaxHealth?.Invoke(maxHealth);
        SetHealth?.Invoke(currentHealth);
    }

    private void RestoreMaxHealth(int amount) {
        //For PermanentTradeoff
        maxHealth = stats.ObtainCurrentMaxHealth();
        stats.SetTemporaryMaxHealth(stats.ObtainCurrentMaxHealth());
        currentHealth = maxHealth;


        SetMaxHealth?.Invoke(maxHealth);
        SetHealth?.Invoke(currentHealth);
    }

    public void TakeDamage(float damage)
    {
        if (!invulnerable)
        {
            if (currentHealth <= 0.25 * maxHealth)
            {
                CheckReduceReceivedDamage?.Invoke();
            }

            if (reduceReceivedDamage)
            {
                damage /= 2;
                damage = (int)damage;
                reduceReceivedDamage = false;
            }

            if (currentHealth - damage < 0)
            {
                currentHealth = 0;
            }
            else
            {
                currentHealth -= damage;
            }

            ReceivedDamage?.Invoke();

            if (SetHealth != null)
            {
                SetHealth(currentHealth);
            }

            CheckPercentage();

            if (currentHealth <= 0)
            {
                if (timesCanDefyDeath > 0)
                {
                    timesCanDefyDeath--;
                    RestoreHealth(50);
                    if (PlayerDefiedDeath != null)
                    {
                        PlayerDefiedDeath();
                    }
                }
                else
                {
                    Die();
                }
            }
        }
    }

    void Die()
    {
        if (PlayerDead != null) {
            PlayerDead();
        }
        animator.SetTrigger("isDead");


        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<CharacterController>().enabled = false;
    }

    public void RestoreHealth(int amount)
    {
        if (currentHealth + amount > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += amount;
        }

        if (SetHealth != null)
        {
            SetHealth(currentHealth);
        }
        CheckPercentage();
    }

    public void ReceiveAttackButtonStatus(bool receivedAttackPressed, bool receivedSpecialPressed) { 
        attackPressed = receivedAttackPressed;
        specialPressed = receivedSpecialPressed;
    }

    private void IncreaseMaxHealth(int amount) {
        //For ability ThickSkin
        stats.SetCurrentMaxHealth(stats.ObtainCurrentMaxHealth() - (amount - 5));
        stats.SetCurrentMaxHealth(stats.ObtainCurrentMaxHealth() + amount);

        maxHealth = stats.ObtainCurrentMaxHealth();
        stats.SetTemporaryMaxHealth(stats.ObtainCurrentMaxHealth());
        currentHealth = maxHealth;

        if (SetMaxHealth != null)
        {
            SetMaxHealth(maxHealth);
        }

        if (SetHealth != null)
        {
            SetHealth(currentHealth);
        }
    }

    private void ModifyMaxHealth() {
        //For Knife Special and DoubleHP
        stats.SetTemporaryMaxHealth(stats.ObtainTemporaryMaxHealth() * 2);
        maxHealth = stats.ObtainTemporaryMaxHealth();
        if (SetMaxHealth != null) { 
            SetMaxHealth(maxHealth);
        }
        RestoreHealth((int)currentHealth);
    }

    private void ResetMaxHealth() {
        stats.SetTemporaryMaxHealth(stats.ObtainTemporaryMaxHealth() / 2);
        maxHealth = stats.ObtainTemporaryMaxHealth();
        if (SetMaxHealth != null)
        {
            SetMaxHealth(maxHealth);
        }

        currentHealth = currentHealth / 2;
        if (SetHealth != null)
        {
            SetHealth(currentHealth);
        }
    }

    private void FillHealth() {
        maxHealth = stats.ObtainCurrentMaxHealth();
        SetMaxHealth?.Invoke(maxHealth);
        currentHealth = maxHealth; 
        SetHealth?.Invoke(currentHealth);
        CheckPercentage();
    }

    void DebugFillHealth() {
        currentHealth = maxHealth;
        SetHealth?.Invoke(currentHealth);
        CheckPercentage();
    }

    private void SetDeathDefiance(int numberOfTimes) {
        timesCanDefyDeath = numberOfTimes;
        //Debug.Log(timesCanDefyDeath);
    }

    private void CheckPercentage() {
        Debug.Log("Current " + currentHealth + " percentage " + 0.8f * maxHealth + " max : " + maxHealth);
        if (currentHealth < 0.8f * maxHealth)
        {
            if (PlayerLowerThanPercentage != null)
            {
                PlayerLowerThanPercentage();
            }
        }
        else {
            if (PlayerHigherThanPercentage != null) {
                PlayerHigherThanPercentage();
            }
        }
    }

    private void Invulnerable() {
        invulnerable = true;
    }

    private void Vulnerable() {
        invulnerable = false;
    }
}
