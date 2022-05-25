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
    private bool move = false;
    private Vector3 targetPosition;

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

        if (move)
        {
            if (Vector3.Distance(transform.position, targetPosition) > 0.09)
            {
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * speed);
            }
            else move = false;
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
                TransformController transformController = hitYourCreature.collider.gameObject.GetComponent<TransformController>();
                CardPrefab cardPrefab = hitYourCreature.collider.gameObject.GetComponent<CardPrefab>();

                creatureScript.FeedRed(choosenFood);
                transformController.DisableHighLiteRed();
                CardPrefab.attackingCreature = null;
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

    public void MoveTo(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
        move = true;
    }
}
