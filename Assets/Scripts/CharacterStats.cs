using UnityEngine;

public class CharacterStats : MonoBehaviour {
  
    public int maxHealth = 6;
    public int currentHealth { get; private set; }
    public Stat damage;
    public Stat health;
    
    void Awake ()
    {
        currentHealth = maxHealth;
    }

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.T)) 
        {
            TakeDamage(1);
        }
    }
    public void TakeDamage (int damage)
    {
        damage = Mathf.Clamp(damage, 0, int.MaxValue);
        currentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage.");

            if (currentHealth <= 0)
            {
                Die();
            }
    }

    public virtual void Die () 
    {
        // Takes lethal damage
        // Will be overwritten with game over screen or ragdoll etc
        Debug.Log(transform.name + " died.");
    }
}
