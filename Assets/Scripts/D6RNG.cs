using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// D6 RNG Roll for combat. Needs to be attached to attack script and animation.
public class D6RNG : MonoBehaviour
{
    
    public TextMesh largeText;

    public void BtnAction()
    {
        PickRandomNumber(6);
    }

    private void PickRandomNumber(int maxInt) {
        int randomNum = Random.Range(1, maxInt+1);
        Debug.Log(randomNum.ToString());
        largeText.text = randomNum.ToString();
    }

}
