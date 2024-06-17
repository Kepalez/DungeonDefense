using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightWarriorAttack : MonoBehaviour, AttackBehavior
{
    private int attackPower;
    GameObject targetUnit;
    public AudioClip AttackSound;
    void Start(){
    }

    public void attack()
    {
        GetComponent<Animator>().SetTrigger("Attack");
        AudioSource ac = GetComponent<AudioSource>();
        ac.PlayOneShot(AttackSound);
    }
    public void startAttack(){
        targetUnit = GetComponent<AttackTowerBehavior>().currentTarget;
        attackPower = GetComponent<AttackTowerBehavior>().attackPower;
        if(targetUnit != null){
            targetUnit.GetComponent<Enemybehavior>().LoseHealth(attackPower);
        }
    }
}