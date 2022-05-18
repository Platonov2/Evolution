using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public float speed;
    public LayerMask foodLayer;
    public LayerMask yourCreatureLayer;
    public LayerMask collisionLayer;

    private GameObject choosenFood = null;
    private bool isDrag = false;
    private Vector3 startPosition;

    void Update()
    {
        if (isDrag)
        {
            RaycastHit hit;

            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit);

            if (hit.collider != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(hit.point.x, hit.point.y, -1), speed);
            }
        }
    }
     
    void OnMouseDown()
    {
        RaycastHit hit;

        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, foodLayer);

        if (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Food"))
        {
            isDrag = true;
            choosenFood = hit.collider.gameObject;
            startPosition = this.transform.position;
        }
    }

    void OnMouseUp()
    {
        RaycastHit hitYourCreature;

        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitYourCreature, Mathf.Infinity, yourCreatureLayer);

        if (hitYourCreature.collider != null)
        {
            Creature creatureScript = hitYourCreature.collider.gameObject.GetComponent<Creature>();

            if (creatureScript.StillHunger())
            {
                creatureScript.Feed(choosenFood);
                transform.position = new Vector3(transform.position.x, transform.position.y, 0);
                choosenFood.layer = LayerMask.NameToLayer("EatenFood");
                startPosition = transform.position;
            }
            else transform.position = startPosition;
        }
        else
        {
            transform.position = startPosition;
        }

        isDrag = false;
        
    }
}
