using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class UnitDrag : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    public GameObject unitPlaceholder;
    private GameObject activePlaceHolder;
    public GameObject unitPrefab;
    public int unitCost;
    private RectTransform tr;
    private CanvasGroup group;

    public void OnBeginDrag(PointerEventData eventData)
    {
        activePlaceHolder = Instantiate(unitPlaceholder,tr.position,Quaternion.identity,tr);
        GameManager.Instance.enableSpawnerStatusCheck();
        GetComponent<Image>().color = new Color(255,255,255,0.6f);
        group.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        var position = Input.mousePosition;
        activePlaceHolder.GetComponent<RectTransform>().position = position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("Dropped unit");
        bool possible = false;
        foreach (GameObject thing in eventData.hovered){
            if(thing.tag == "PlayerSpawner"){
                DropUnit dp = thing.GetComponent<DropUnit>(); 
                if(dp.occupied == false){
                    possible = dp.spawnUnit(unitPrefab);
                }
            }
        }
        Destroy(activePlaceHolder);
        GetComponent<Image>().color = new Color(255,255,255,1f);
        group.blocksRaycasts = true;
        GameManager.Instance.disableSpawnerStatusCheck();
        if(possible)
            GameManager.Instance.playerMoney -= unitCost;
    }

    void Update(){
        if(GameManager.Instance.playerMoney < unitCost){
            GetComponent<Image>().color = Color.red;
            group.blocksRaycasts = false;
        }else{
            if(activePlaceHolder == null){
                GetComponent<Image>().color = Color.white;
                group.blocksRaycasts = true;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        group = GetComponent<CanvasGroup>();
        tr = GetComponent<RectTransform>();
    }
}
