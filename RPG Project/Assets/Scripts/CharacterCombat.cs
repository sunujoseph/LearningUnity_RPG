using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    // we can set an attack speed for each individual character
    // we can make attack speed influenced by character stats

    public float attackSpeed = 1f;
    private float attackCooldown = 0f;

    public float attackDelay = 0.6f;

    public event System.Action OnAttack;


    CharacterStats myStats;

    void Start()
    {
        myStats = GetComponent<CharacterStats>();
    }

    void Update()
    {
        attackCooldown -= Time.deltaTime;
    }


    public void Attack(CharacterStats targetStats)
    {
        // if attack cooldown is below 0 we can attack
        // right after we attack, we are ready to attack.
        // so we reset the cooldown
        // the greater the attackspeed, the smaller the cooldown

        if (attackCooldown <= 0f)
        {
            //targetStats.TakeDamage(myStats.damage.GetValue());

            StartCoroutine(DoDamage(targetStats, attackDelay));

            if (OnAttack != null)
            {
                OnAttack();
            }

            attackCooldown = 1f / attackSpeed;
        }
    }

    IEnumerator DoDamage (CharacterStats stats, float delay)
    {
        yield return new WaitForSeconds(delay);

        // take damage
        stats.TakeDamage(myStats.damage.GetValue());
    }

}
