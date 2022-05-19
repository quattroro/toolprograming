using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    GraphicRaycaster graphicRaycaster;


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




    }

    public void MouseUp(Vector2 pos)
    {
        //canvas�� �ִ� graphicraycast�� �̿��� Ŭ���� ��ġ�� �ִ� ��ü���� �������� �޾ƿ´�.
        PointerEventData ped = new PointerEventData(null);
        ped.position = pos;
        List<RaycastResult> result = new List<RaycastResult>();
        graphicRaycaster.Raycast(ped, result);






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
