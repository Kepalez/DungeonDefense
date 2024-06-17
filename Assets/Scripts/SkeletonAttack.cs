using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAttack : MonoBehaviour, AttackBehavior
{
    private GameObject target;
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
        target = GetComponent<Enemybehavior>().currentTarget;
        int damage = GetComponent<Enemybehavior>().attackPower;
        if(target != null){
            target.GetComponent<Playerbehavior>().LoseHealth(damage);
        }
    }
}