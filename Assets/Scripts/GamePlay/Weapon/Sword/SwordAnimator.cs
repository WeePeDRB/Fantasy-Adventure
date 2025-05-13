using UnityEngine;

public class SwordAnimator : MonoBehaviour
{
    //
    // FIELDS
    //

    private Animator animator;
    private SwordController sword;

    private const string ATTACK = "Attack";

    //
    // FUNCTIONS
    //

    private void InitializeAnimator()
    {
        animator = GetComponent<Animator>();
        sword = GetComponentInParent<SwordController>();
    }

    private void AttackAnimate()
    {
        animator.SetTrigger(ATTACK);
    }

    private void ApplyDamage()
    {
        sword.ApplyDamage();
    }

    private void Start()
    {
        //
        InitializeAnimator();

        //
        sword.OnWeaponAttack += AttackAnimate;
    }
}