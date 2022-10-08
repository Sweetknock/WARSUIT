using TMPro;
using UnityEngine;

public class CharacterStats : MonoBehaviour {
  
    public int maxHealth = 6;
    public int currentHealth { get; private set; }
    public Stat damage;
    public Stat health;

    public TextMeshProUGUI healthBarStat;

    public D6RNG damageRoll;
    
    void Awake ()
    {
        currentHealth = maxHealth;
        healthBarStat.text = maxHealth.ToString();
    }

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.T)) 
        {
            TakeDamage(damageRoll.BtnAction());
        }
        healthBarStat.text = currentHealth.ToString();
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
