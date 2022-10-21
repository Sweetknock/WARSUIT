using UnityEngine.UI;
using UnityEngine;

public class CharacterStats : MonoBehaviour {
  
    public int maxHealth = 6;
    private int currentHealth { get; set; }
    [SerializeField] GameObject canvas_pf;
    private Slider healthSlider;
    private D6RNG damageRoll;
    void Start ()
    {
        //instatiate canavs object in the scene 
        GameObject canvas = Instantiate(canvas_pf, new Vector3(0, 0, 0), Quaternion.identity);
        Debug.Log(canvas);
        //Set camera as main camera already on the scene.
        Camera camera = Camera.main;
        canvas.GetComponent<Canvas>().worldCamera = camera;

        //get components of canvas so that we can update them.
        healthSlider = canvas.GetComponentInChildren<Slider>();
        damageRoll = canvas.GetComponentInChildren<D6RNG>();

        //set current health of slider to max.
        currentHealth = maxHealth;
        healthSlider.value = 50;

    }
   
    //Update health bar with value stored in currentHealth which is modified by TakeDamage()
    void Update()
    {
        ShowHealthBar();
    }

    private void ShowHealthBar()
    {
        healthSlider.value = currentHealth;
    }

    //Continuously runs function while scene is active checking for button pushes.
    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 175, 130), "Damage"))
        {
            TakeDamage(damageRoll.BtnAction());
        }
    }

    //Take damage value from D6RNG roll, modify it, and substracto from character's
    //current helath.
    private void TakeDamage(int damage)
    {
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        if (damage > 3 && damage < 6)
            currentHealth -= 1;
        
        if (damage == 6)
            currentHealth -= 6;
        Debug.Log(transform.name + " takes " + damage + " damage.");
        print(currentHealth + " HP left.");
        if (currentHealth <= 0)
            {
                Die();
            }
    }

    // Takes lethal damage, i.e. current health is less than 0.
    // Will be overwritten with game over screen or ragdoll etc
    public virtual void Die () 
    {
        Debug.Log(transform.name + " died.");
    }
}
