using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGoal : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "EnemyUnit"){
            other.gameObject.GetComponent<Enemybehavior>().LoseHealth(1000);
            GameManager.Instance.playerLoseLive();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
