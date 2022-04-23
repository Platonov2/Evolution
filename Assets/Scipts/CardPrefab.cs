using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardPrefab : MonoBehaviour
{
    public float speed;
    bool isDrag = false;


    void Update()
    {
        if (isDrag)
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                Debug.Log(hit.collider.name);
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(hit.point.x, hit.point.y, -200), speed);
            }
        }
    }

    void OnMouseDown()
    {
        isDrag = true;
    }

    void OnMouseUp()
    {
        isDrag = false;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, 200), speed);
    }
}
