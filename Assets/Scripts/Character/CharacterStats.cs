using UnityEngine.UI;
using UnityEngine;

public class CharacterStats : MonoBehaviour {
  
    public int maxHealth = 6;
    public int currentHealth { get; private set; }
    public Stat damage;
    public Stat health;

    public Slider healthSlider;

    public D6RNG damageRoll;
    
    void Awake ()
    {
        currentHealth = maxHealth;
        healthSlider.value = currentHealth;
        healthSlider = GetComponent<Slider>();
    }
    void ShowHealthBar()
    {
        healthSlider.value=1;
    }
   
    void Update()
    {
        ShowHealthBar();
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
   void OnGUI() 
    {
        if (GUI.Button(new Rect(100, 100, 75, 30), "Damage"))
        {
            TakeDamage(damageRoll.BtnAction());
        }
    }
  
    public virtual void Die () 
    {
        // Takes lethal damage
        // Will be overwritten with game over screen or ragdoll etc
        Debug.Log(transform.name + " died.");
    }
}
