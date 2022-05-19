using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ItemCraftTable : MonoBehaviour
{
    [Header("제작테이블 정보")]
    [SerializeField]
    private Vector2Int CraftingSlotSize = new Vector2Int(3, 3);
    [SerializeField]
    private int CraftResultSlotNum = 1;
    [SerializeField]
    private Vector2Int InventorySlotSize = new Vector2Int(9, 3);
    [SerializeField]
    private int QuickInvectoryNum = 9;

    [Header("슬롯 정보")]
    public BaseSlot CraftSlot;
    public BaseSlot ResultSlot;
    public BaseSlot InventorySlot;
    public BaseSlot QuickSlot;

    public BaseSlot[] CraftSlotArr;
    public BaseSlot[] InventorySlotArr;
    public BaseSlot[] QuickSlotArr;


    public BaseNode ClickedNode;

    private void Awake()
    {
        CraftSlotInit(72, 78);
        InventorySlotInit(72, 78);

    }

    public void CraftSlotInit(int xval, int yval)
    {
        BaseSlot temp;
        CraftSlotArr = new BaseSlot[CraftingSlotSize.x * CraftingSlotSize.y];
        CraftSlotArr[0] = CraftSlot;
        for (int y = 0; y < CraftingSlotSize.y; y++)
        {
            for (int x = 0; x < CraftingSlotSize.x; x++)
            {
                if (x != 0 || y != 0)
                {
                    temp = GameObject.Instantiate<BaseSlot>(CraftSlot);
                    temp.transform.parent = CraftSlot.transform.parent;
                    temp.transform.position = CraftSlot.transform.position + new Vector3(x * xval, -y * yval);
                    CraftSlotArr[x + (y * CraftingSlotSize.x)] = temp;
                }
            }
        }
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
            QuickSlotArr[x] = temp;
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

    

    // Update is called once per frame
    void Update()
    {
        

    }
}
