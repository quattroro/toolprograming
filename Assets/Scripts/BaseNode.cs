using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseNode : MonoBehaviour
{
    Vector2Int Index;

    public bool _isactive;
    public bool _isclicked;

    public RectTransform rectTransform;

    public BaseSlot SettedSlot;

    public BaseSlot PreSlot;//��尡 Ŭ���Ǿ ���콺�� ����ٴҶ� ���� �ִ� ������ ��ġ�� ������ ���´�.



    public virtual void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    //������ �߰��뿡 �ִ� ��Ȱ�� ������� ���� ���� â���� ��ȣ�ۿ��ϴ� Ȱ��������� �����ϱ� ���� ������Ƽ
    public bool NodeIsActive
    {
        get
        {
            return _isactive;
        }
        set
        {
            _isactive = value;
        }
    }

    //Ŭ���� �������� �ƴ��� 
    public bool NodeIsClicked
    {
        get
        {
            return _isclicked;
        }
        set
        {
            _isclicked = value;

        }
    }

    public virtual BaseNode DivideNode()
    {
        BaseNode temp = null;
        if(IsStackAble()&&GetStack()>=2)
        {
            int cur = GetStack() / 2;
            
            temp = GameObject.Instantiate<BaseNode>(this);
            temp.transform.parent = this.transform.parent;
            temp.NodeIsClicked = true;
            temp.PreSlot = SettedSlot;
            temp.ChangeStack(0);
            temp.ChangeStack(cur);
            this.ChangeStack(-cur);
            //BaseNode temp = SettingNode;
            //SettingNode = null;
        }
        else
        {
            temp = SettedSlot.GetSettingNode();
        }

        return temp;
    }
    public virtual void ChangeStack(int val)
    {
        
    }

    public virtual int GetStack()
    {
        return -1;
    }

    public virtual int GetItemID()
    {
        return -1;
    }

    public virtual bool IsStackAble()
    {
        return false;
    }


    public virtual void Update()
    {
        
    }
}
