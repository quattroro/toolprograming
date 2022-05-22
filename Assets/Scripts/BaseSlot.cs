using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSlot : MonoBehaviour
{
    public Vector2Int SlotIndex;

    public BaseNode SettingNode;

    public RectTransform rectTransform;

    public delegate void InsertSlotEvent();
    protected InsertSlotEvent insertevent;

    public delegate void PickUpSlotEvent();
    protected PickUpSlotEvent pickupevent;

    protected EnumTypes.SlotTypes slottype;

    public EnumTypes.SlotTypes GetSlotTypes()
    {
        return slottype;
    }
    public virtual void SetNode(BaseNode node)
    {
        //노드를 슬록 자신의 하위로 두고 크기를 맞춰준다.
        node.transform.parent = this.transform;
        node.rectTransform.sizeDelta = this.rectTransform.sizeDelta;
        node.transform.localPosition = new Vector3(0, 0, 0);
        if (node.NodeIsClicked)
            node.NodeIsClicked = false;
        node.SettedSlot = this;
        this.SettingNode = node;
    }

    public virtual void InsertEvent(InsertSlotEvent _event)
    {
        insertevent += _event;
    }

    public virtual void PickUpEvent(PickUpSlotEvent _event)
    {
        pickupevent += _event;
    }

    public void RemoveNode()
    {
        if(SettingNode!=null)
        {
            GameObject.Destroy(SettingNode.gameObject);
            SettingNode = null;
        }
    }

    public void RemoveNode(BaseNode node)
    {
        if(SettingNode == node)
        {
            GameObject.Destroy(SettingNode.gameObject);
            SettingNode = null;
        }
    }

    public virtual BaseNode GetSettingNode()
    {
        SettingNode.NodeIsClicked = true;
        SettingNode.PreSlot = this;
        BaseNode temp = SettingNode;
        SettingNode = null;
        return temp;
    }

    //public BaseNode GetSettingNode_NoneDestroy()
    //{
        
    //    return SettingNode;
    //}

    public int GetSettingNodeID()
    {
        if (SettingNode == null)
            return -1;

        return SettingNode.GetItemID();
    }


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
