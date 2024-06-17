using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CountTxt : MonoBehaviour
{
    // Start is called before the first frame update

    public TextMeshProUGUI txt;

    public void UpdateTxt(int amount,string buff){
        txt.text = amount.ToString()+" "+ buff;
    }
}
