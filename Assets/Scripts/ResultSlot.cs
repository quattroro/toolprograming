using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultSlot : BaseSlot
{
    public override void Start()
    {
        base.Start();
        slottype = EnumTypes.SlotTypes.Result;
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
