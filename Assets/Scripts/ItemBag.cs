using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���Ŀ� ������ ����Ͽ�
//������ ������ �������̺�� ���������� �����Ѵ�.
public class ItemBag : Singleton<ItemBag>
{
    [SerializeField]
    private Vector2Int InventorySlotSize = new Vector2Int(9, 3);
    [SerializeField]
    private int QuickInvectoryNum = 9;

    public BaseSlot InventorySlot;
    public BaseSlot QuickSlot;

    public BaseSlot[] InventorySlotArr;
    public BaseSlot[] QuickSlotArr;

    private void Awake()
    {
        InventorySlotInit(72, 78);
    }


    public void InventorySlotInit(int xval, int yval)
    {
        BaseSlot temp;
        InventorySlotArr = new BaseSlot[InventorySlotSize.x * InventorySlotSize.y];
        InventorySlotArr[0] = InventorySlot;
        for (int y = 0; y < InventorySlotSize.y; y++)
        {
            for (int x = 0; x < InventorySlotSize.x; x++)
            {
                if (x != 0 || y != 0)
                {
                    temp = GameObject.Instantiate<BaseSlot>(InventorySlot);
                    temp.transform.parent = InventorySlot.transform.parent;
                    temp.transform.position = InventorySlot.transform.position + new Vector3(x * xval, -y * yval);
                    temp.SlotIndex = new Vector2Int(x, y);
                    InventorySlotArr[x + (y * InventorySlotSize.x)] = temp;

                }
            }
        }
        QuickSlotArr = new BaseSlot[QuickInvectoryNum];
        QuickSlotArr[0] = QuickSlot;
        for (int x = 1; x < QuickInvectoryNum; x++)
        {
            temp = GameObject.Instantiate<BaseSlot>(QuickSlot);
            temp.transform.parent = QuickSlot.transform.parent;
            temp.transform.position = QuickSlot.transform.position + new Vector3(x * xval, 0);
            temp.SlotIndex = new Vector2Int(x, 0);
            
            QuickSlotArr[x] = temp;
        }

    }

    //�ش� ��� �ı�
    public void DeleteItem(BaseSlot slot, BaseNode node)
    {
        slot.RemoveNode(node);
    }

    //�̸� ������� ��� ��ü�� �Ѱܹ޾Ƽ� ����
    public void InsertItem(BaseSlot slot, BaseNode node)
    {
        if(slot.SettingNode==null)
        {
            slot.SetNode(node);
        }
    }

    //�̸� ������� ��� ��ü�� �Ѱܹ޾Ƽ� ����
    public void InsertItem(BaseNode node)
    {
        BaseSlot tempslot = null;
        bool flag = false;


        //���� ���� ������ �������� ������ �ش� �������� ������ �������� �ش�.
        for (int y = 0; y < InventorySlotSize.y; y++)
        {
            for (int x = 0; x < InventorySlotSize.x; x++)
            {
                if(InventorySlotArr[x + y * (InventorySlotSize.x)].SettingNode != null)
                {
                    
                    if (InventorySlotArr[x + y * (InventorySlotSize.x)].GetSettingNodeID() == node.GetItemID())
                    {
                        if (node.IsStackAble())
                        {
                            InventorySlotArr[x + y * (InventorySlotSize.x)].SettingNode.ChangeStack(+1);
                            GameObject.Destroy(node.gameObject);
                            return;
                        }
                    }
                }
            }
        }

        for (int x = 0; x < QuickInvectoryNum; x++)
        {
            if(QuickSlotArr[x + 0].GetSettingNodeID() == node.GetItemID())
            {
                if (node.IsStackAble())
                {
                    QuickSlotArr[x + 0].SettingNode.ChangeStack(+1);
                    GameObject.Destroy(node.gameObject);
                    return;
                }
            }
            
        }


        //����ִ� ���� �� ã�´�.
        for (int y = 0; y < InventorySlotSize.y; y++)
        {
            for (int x = 0; x < InventorySlotSize.x; x++)
            {
                //������ ��������� �ش� ���Կ� ������ ��带 �־��ش�.
                if (InventorySlotArr[x + y * (InventorySlotSize.x)].SettingNode == null)
                {
                    tempslot = InventorySlotArr[x + y * (InventorySlotSize.x)];
                    flag = true;
                    break;
                }

            }
            if (flag)
            {
                break;
            }
        }

        //�κ��丮�� ���� á���� �����Կ��� ã�´�.
        if (tempslot == null)
        {
            for (int x = 0; x < QuickInvectoryNum; x++)
            {
                if (QuickSlotArr[x + 0].SettingNode == null)
                {
                    tempslot = QuickSlotArr[x + 0];
                    break;
                }
            }
        }

        if (tempslot == null)
        {
            GameObject.Destroy(node.gameObject);
            return;
        }

        tempslot.SetNode(node);
        
    }

}
