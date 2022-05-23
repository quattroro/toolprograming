using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemNode : BaseNode
{
    public string itemName;
    public int itemCode;
    public Sprite itemSprite;

    public int _Stack;

    public bool _isstackable = false;

    public EnumTypes.ItemTypes itemtype;

    public Text stacktext;

    public override void Awake()
    {
        base.Awake();
        //stacktext = GetComponentInChildren<Text>();
    }

    public void InitSetting(int itemcode, string Name, Sprite sprite, Vector3 pos, string _type)
    {
        this.name = Name;
        this.itemName = Name;
        this.itemCode = itemcode;
        this.itemSprite = sprite;
        this.transform.localPosition = pos;

        //Debug.Log($"¾ÆÀÌÅÛ Å¸ÀÔ{_type}///");
        //Debug.Log($"///{EnumTypes.ItemTypes.Blocks.ToString()}///");
        
        if (_type == EnumTypes.ItemTypes.Blocks.ToString())
        {
            //Debug.Log($"¿ç·Îµé¾î¿È");
            this._isstackable = true;
            this.itemtype = EnumTypes.ItemTypes.Blocks;
        }
        else
        {
            this._isstackable = false;
            this.itemtype = EnumTypes.ItemTypes.Equips;
        }

        ChangeStack(+1);

        GetComponent<Image>().sprite = sprite;
        
    }

    public override void ChangeStack(int val)
    {
        if (val == 0)
        {
            _Stack = 0;
            return;
        }
            

        _Stack += val;

        if (_Stack > 99)
            _Stack = 99;
        if (_Stack < 1)
            _Stack = 1;


        if(_Stack == 1)
        {
            if (stacktext.gameObject.activeSelf)
                stacktext.gameObject.SetActive(false);
        }
        else
        {
            if (!stacktext.gameObject.activeSelf)
                stacktext.gameObject.SetActive(true);

            stacktext.text = string.Format("{0}", _Stack);
        }
    }

    public override void ItemMerge(BaseNode node)
    {
        if(node.GetItemID() == this.GetItemID())
        {
            this.ChangeStack(node.GetStack());
            GameObject.Destroy(node.gameObject);
        }
    }
    public override int GetStack()
    {
        return _Stack;
    }
    public override bool IsStackAble()
    {
        return _isstackable;
    }

    public override int GetItemID()
    {
        return itemCode;
    }


    public override void Update()
    {
        if(NodeIsActive)
        {
            if(NodeIsClicked)
            {
                this.transform.position = Input.mousePosition;
            }
        }
    }
}
