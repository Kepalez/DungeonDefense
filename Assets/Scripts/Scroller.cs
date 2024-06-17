using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Scroller : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private RawImage img;
    [SerializeField] private float x,y;
    [SerializeField] private bool bounce;
    [SerializeField] private float bounceLimit;
 
    // Update is called once per frame
    void Update()
    {
        img.uvRect = new Rect(img.uvRect.position + new Vector2(x,y)*Time.deltaTime,img.uvRect.size);
        if(bounce && Mathf.Abs(img.uvRect.x) >= bounceLimit) x*=-1;
    }
}
