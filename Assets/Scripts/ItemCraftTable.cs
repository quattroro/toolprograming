using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ItemCraftTable : Singleton<ItemCraftTable>
{
    [Header("�������̺� ����")]
    [SerializeField]
    private Vector2Int CraftingSlotSize = new Vector2Int(3, 3);
    [SerializeField]
    private int CraftResultSlotNum = 1;
    [SerializeField]
    private Vector2Int InventorySlotSize = new Vector2Int(9, 3);
    [SerializeField]
    private int QuickInvectoryNum = 9;

    public List<CraftRecipe> recipelist;

    [Header("���� ����")]
    public BaseSlot CraftSlot;
    public BaseSlot ResultSlot;

    public ItemBag itemBag;

    ////////////////////////////////////////////////////
    //public BaseSlot InventorySlot;
    //public BaseSlot QuickSlot;


    //public BaseSlot[] InventorySlotArr;
    //public BaseSlot[] QuickSlotArr;
    ////////////////////////////////////////////////////
    
    public BaseSlot[] CraftSlotArr;

    public BaseNode ClickedNode;

    private void Awake()
    {
        CraftSlotInit(72, 78);
        ResultSlot.PickUpEvent(ResultPickUp);
    }

    //���â�� �ִ� �������� ������������ ����â�� �ִ� �����۵��� ������ �ϳ� ���̰ų� �����ش�.
    public void ResultPickUp()
    {
        for (int i = 0; i < CraftSlotArr.Length; i++)
        {
            if (CraftSlotArr[i].SettingNode != null)
            {
                if (CraftSlotArr[i].SettingNode.GetStack()>=2)
                {
                    CraftSlotArr[i].SettingNode.ChangeStack(-1);
                }
                else
                {
                    CraftSlotArr[i].RemoveNode();
                }
            }
        }
        CraftInsert();
    }

    //����â���ִ� �������� ���������� ���â�� �̹� �������� �������� �ش� ������ ��带 �����ְ�, �ش� �������� ����� ���¿����� ���۹��� �˻��ϰ� ����� ǥ�����ش�. 
    public void CraftItemPickUp()
    {
        if (ResultSlot.SettingNode!=null)
        {
            //Debug.Log("��� ������");
            ResultSlot.RemoveNode();
        }

        CraftInsert();
    }

    public void CraftSlotInit(int xval, int yval)
    {
        BaseSlot temp;
        CraftSlotArr = new BaseSlot[CraftingSlotSize.x * CraftingSlotSize.y];
        CraftSlotArr[0] = CraftSlot;
        CraftSlotArr[0].InsertEvent(CraftInsert);
        CraftSlotArr[0].PickUpEvent(CraftItemPickUp);

        for (int y = 0; y < CraftingSlotSize.y; y++)
        {
            for (int x = 0; x < CraftingSlotSize.x; x++)
            {
                if (x != 0 || y != 0)
                {
                    temp = GameObject.Instantiate<BaseSlot>(CraftSlot);
                    temp.transform.parent = CraftSlot.transform.parent;
                    temp.transform.position = CraftSlot.transform.position + new Vector3(x * xval, -y * yval);
                    temp.SlotIndex = new Vector2Int(x, y);
                    temp.InsertEvent(CraftInsert);//���۽����� ���Կ� ���� �������� �鷯������ ����� �Լ��� �븮�ڿ� ������ش�.
                    temp.PickUpEvent(CraftItemPickUp);

                    CraftSlotArr[x + (y * CraftingSlotSize.x)] = temp;
                }
            }
        }
    }

    //���� �ϳ��� ������ ��� ������ �����ؼ� ���������̺� �Ѱ��༭ �˻��Ѵ�.
    public void CraftInsert()
    {
        Debug.Log("�˻� ����?");
        int[] inputs = new int[CraftSlotArr.Length];
        int count = 0;

        for (int i = 0; i < CraftSlotArr.Length; i++)
        {
            if(CraftSlotArr[i].SettingNode!=null)
            {
                inputs[i] = CraftSlotArr[i].SettingNode.GetItemID();
                count++;
            }
            else
            {
                inputs[i] = 0;
            }
        }

        //�ش� �Լ��� �������� ������ â�� �ƹ��͵� ������ ��Ī�� ���� �ʴ´�.
        if (count == 0)
            return;

        BaseNode copyresultnode = CraftRecipe.SearchRecipe(recipelist, inputs);

        if(copyresultnode != null)
        {
            //������ ���� �߿��� �ش� ��带 ã�Ƽ� ������Կ� �־��ش�.
            //BaseNode copyresultnode = GameObject.Instantiate<BaseNode>(ItemDataLoader.Instance.itemnodes.Find(x => x.GetItemID() == resultcode));
            //copyresultnode.NodeIsActive = true;
            ResultSlot.SetNode(copyresultnode);
        }

    }




    public Vector2Int GetInventorySize
    {
        get
        {
            return InventorySlotSize;
        }
    }

    public Vector2Int GetSlotSize
    {
        get
        {
            return CraftingSlotSize;
        }
    }

    //������ ���濡�� �������̺�� �Ű��ش�.
    public void CraftSetItem(BaseSlot slot, BaseNode node)
    {

    }
    
    //���� ���̺��� �ٽ� ������ �������� �����ش�.
    public void CraftUnSetItem(BaseSlot slot, BaseNode node)
    {

    }



    // Update is called once per frame
    void Update()
    {
        

    }
}
