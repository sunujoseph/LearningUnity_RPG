using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    // make it a Stat to have modifier for health
    public int maxHealth = 100;

    // any other class can get this value because of get
    // but we can only set this value within this class because of private
    public int currentHealth { get; private set; }

    public Stat damage;
    public Stat armor;

    
    void Awake()
    {
        // if we do this here it's fine, but any other class, it wouldt let us
        currentHealth = maxHealth;
    }


    void Update()
    {

        // test to take damage
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(10);
        }
    }


    public void TakeDamage(int damage)
    {
        //damage with armor calculation
        damage -= armor.GetValue();

        // so we don't get healed by taking damage. 
        // if armor exeeds damage, so damage never goes below 0
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage.");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // made a virtual because we can using for both the player and the enemy
    public virtual void Die()
    {
        // Die in some way
        // this method is meanth to be overwritten
        Debug.Log(transform.name + " died.");
    }

}
