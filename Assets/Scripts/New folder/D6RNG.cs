using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// D6 RNG Roll for combat. Needs to be attached to attack script and animation.
public class D6RNG : MonoBehaviour
{

    public TextMeshProUGUI damageText;

    public int BtnAction()
    {
        return PickRandomNumber(6);
    }

    private int PickRandomNumber(int maxInt) {
        int randomNum = Random.Range(1, maxInt+1);
        damageText.text = randomNum.ToString();
        Debug.Log($"{damageText.text} is the random number.");
        return randomNum;
    }
}
