using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


//���⼭ ��� ���콺 �Է��� ����Ѵ�.
//���콺�� Ŭ���Ǹ� � ��ü�� ���õǾ��� �׷��׵Ǿ����� �Ǵ��ؼ� �������ش�.
public class InputManager : MonoBehaviour
{
    GraphicRaycaster graphicRaycaster;

    [Header("current vals")]
    bool NowClicked;

    BaseNode ClickedObj;


    private void Awake()
    {
        graphicRaycaster = GetComponentInParent<GraphicRaycaster>();
    }


    public void MouseDown(Vector2 pos)
    {
        //canvas�� �ִ� graphicraycast�� �̿��� Ŭ���� ��ġ�� �ִ� ��ü���� �������� �޾ƿ´�.
        PointerEventData ped = new PointerEventData(null);
        ped.position = pos;
        List<RaycastResult> result = new List<RaycastResult>();
        graphicRaycaster.Raycast(ped, result);

        BaseNode node = null;

        foreach(var a in result)
        {
            if (a.gameObject.tag == "Node")
            {
                node = a.gameObject.GetComponent<BaseNode>();
                if (node.NodeIsActive)//Ȱ�����
                {
                    if(!node.NodeIsClicked)//Ŭ���� ��尡 �ƴҶ�
                    {
                        ClickedObj = node.SettedSlot.GetSettingNode();
                    }
                }
                else//��Ȱ�����
                {
                    //�ش� ��尡 Ŭ���Ǹ� ������â�� �ش� �������� �߰����ش�.
                    BaseNode copyNode = GameObject.Instantiate<BaseNode>(node);
                    copyNode.NodeIsActive = true;
                    ItemBag.Instance.InsertItem(copyNode);
                }

                Debug.Log($"{a.gameObject.name} clicked");
            }
        }



    }

    public void MouseUp(Vector2 pos)
    {
        //canvas�� �ִ� graphicraycast�� �̿��� Ŭ���� ��ġ�� �ִ� ��ü���� �������� �޾ƿ´�.
        PointerEventData ped = new PointerEventData(null);
        ped.position = pos;
        List<RaycastResult> result = new List<RaycastResult>();
        graphicRaycaster.Raycast(ped, result);

        BaseNode node = null;
        BaseSlot slot = null;

        Debug.Log($"����{result.Count}");

        //�ƹ��͵� ���� ������ ��带 �θ� �ش� ���� �ı�
        if(result.Count==1)
        {
            if (ClickedObj != null)
            {
                GameObject.Destroy(ClickedObj.gameObject);
                ClickedObj = null;
                return;
            }
        }

        //���� ������ �ش� ��ġ�� �ִ� ������Ʈ�� Ȯ��
        foreach (var a in result)
        {
            //�׷����ϴ� ��尡 ������
            if(ClickedObj !=null)
            {
                //�ش� ���콺�� ��ġ���� ������ ã�´�.
                if(a.gameObject.tag == "Slot")
                {
                    slot = a.gameObject.GetComponent<BaseSlot>();
                    if (slot.GetSlotTypes()!=EnumTypes.SlotTypes.Result)
                    {
                        //ã�� ������ �� �����϶� �ش� ������ ��带 �ش� ��忡 �������ش�.
                        if (slot.SettingNode == null)
                        {
                            slot.SetNode(ClickedObj);
                            //ClickedObj = null;
                        }
                        else
                        {
                            slot = null;
                            //ClickedObj = null;
                        }
                    }
                    else
                    {
                        slot = null;
                    }
                }
                
            }
        }

        //�巡�� ���� ��尡 �ִµ� ���콺�� ��ġ�� ������ ������ ���� �����־��� �ڸ��� ���ư���.
        if(ClickedObj != null&&slot == null)
        {
            ClickedObj.PreSlot.SetNode(ClickedObj);
            ClickedObj = null;
        }

        if (ClickedObj != null)
            ClickedObj = null;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MouseDown(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            MouseUp(Input.mousePosition);
        }
    }
}
