using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
//IDropHandler
public class DropUnit : MonoBehaviour
{
    public bool occupied = false;
    public GameObject statusDisplayer;
    public int depthLayer;



    public void OnMouseEnter(){
        //Debug.Log("Mouse over "+name);
    }
    void Awake(){
        statusDisplayer = transform.GetChild(0).gameObject;
    }
    void Update(){
        statusDisplayer.GetComponent<SpriteRenderer>().color = occupied ? new Color(0.72f,0.2f,0.1f,0.3f) : new Color(0.4f,1f,0.35f,0.3f);
    }
    public bool spawnUnit(GameObject unit){
        if(occupied) return false;
        occupied = true;
        GameObject spawnedUnit = Instantiate(unit,transform.position,Quaternion.identity);
        spawnedUnit.transform.parent = transform;
        spawnedUnit.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = depthLayer;
        return true;
    }

    public void showStatus(){
        statusDisplayer.SetActive(true);
    }

    internal void hideStatus()
    {
        statusDisplayer.SetActive(false);
    }
}
