using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DamageRoll : MonoBehaviour
{
    public Stat damage;
    public int damage;
    public TextMeshProUGUI damageUI;

    public int Damage { get => damageValue; set => damage = ; }

    void Update()
    {
        damageUI.text = damage.ToString();
    }

    void OnTriggerEnter()
    {
        UnityEngine.Debug.Log(" Damage UI is Working.");
        if (damage.gameObject.tag == "Damage Taken") 
        {
            damageValue++;
        }
    }
}
