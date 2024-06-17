using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   

[RequireComponent(typeof(Image))]

public class changeHeart : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Sprite[] sprites;
    private Image image;
        
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    public void setHeart(int index){
        if(index>=0){
            image.sprite = sprites[index];
        }
    }
}
