using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemNode : BaseNode
{
    public string itemName;
    public int itemCode;
    public Sprite itemSprite;
    



    public void InitSetting(int itemcode, string Name, Sprite sprite, Vector3 pos, Transform parent)
    {
        this.itemName = Name;
        this.itemCode = itemcode;
        this.itemSprite = sprite;

        this.transform.parent = parent;
        this.transform.position = pos;

        GetComponent<Image>().sprite = sprite;
        
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
