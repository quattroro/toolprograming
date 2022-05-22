using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftSlot : BaseSlot
{
    public override void Start()
    {
        base.Start();
        slottype = EnumTypes.SlotTypes.Craft;
    }
    //public delegate void InsertCraftSlotEvent(Vector2Int index);

    //basenode�� �Ȱ��� �����ϰ� ���� ���ԵǾ������� itemcrafttable�� �˷��ش�.
    public override void SetNode(BaseNode node)
    {
        //��带 ���� �ڽ��� ������ �ΰ� ũ�⸦ �����ش�.
        node.transform.parent = this.transform;
        node.rectTransform.sizeDelta = this.rectTransform.sizeDelta;
        node.transform.localPosition = new Vector3(0, 0, 0);
        if (node.NodeIsClicked)
            node.NodeIsClicked = false;
        node.SettedSlot = this;
        this.SettingNode = node;

        insertevent();
    }

    //������� �������ٴ� �̺�Ʈ�� �߻���Ų��.
    public override BaseNode GetSettingNode()
    {
        SettingNode.NodeIsClicked = true;
        SettingNode.PreSlot = this;
        BaseNode temp = SettingNode;
        SettingNode = null;
        pickupevent();
        return temp;
    }

}
