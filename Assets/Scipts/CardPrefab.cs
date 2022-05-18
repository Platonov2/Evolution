using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardPrefab : MonoBehaviour
{
    public Player player;
    public float speed;
    public LayerMask yourCreaturesFieldLayer;
    public LayerMask yourCreatureLayer;
    public LayerMask opponentCreatureLayer;
    public LayerMask yourHandCardLayer;
    public LayerMask emptyCreatureLayer;
    public GameObject emptyCreature;
    public KeyCode changeAbility;

    private bool isDrag = false;
    private GameObject choosenCard = null;
    private Vector3 startPosition;
    private float timeHolding;
    private Vector3 newCreaturePosition;

    private Card cardScript;

    void Start()
    {
        cardScript = GetComponent<Card>();
    }

    void Update()
    {
        // ѕерет€гивание карты курсором
        if (isDrag)
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(hit.point.x, hit.point.y, -50), speed);
            }

            if (Input.GetKey(changeAbility))
            {
                cardScript.ChangeAbility();
            }
        }
    }

    // ќбработка нажати€ на карту
    void OnMouseDown()
    {
        RaycastHit hit;

        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, yourHandCardLayer);
        // ѕерет€гивать можно только свои карты из руки
        if (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("YourHandCard"))
        {
            isDrag = true;
            choosenCard = hit.collider.gameObject;
            startPosition = this.transform.position;
        }
    }

    // ќбработка "отпускани€" карты
    void OnMouseUp()
    {
        RaycastHit hitHandCard;
        RaycastHit hitCreaturesField;
        RaycastHit hitYourCreature;
        RaycastHit hitEmptyCreature;

        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitHandCard, Mathf.Infinity, yourHandCardLayer);
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitCreaturesField, Mathf.Infinity, yourCreaturesFieldLayer);
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitYourCreature, Mathf.Infinity, yourCreatureLayer);
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitEmptyCreature, Mathf.Infinity, emptyCreatureLayer);

        if (hitHandCard.collider != null)
        {
            
            
            // —оздание нового существа
            if (hitEmptyCreature.collider != null)
            {
                player.CreateCreature(choosenCard);
            }
            else if (hitYourCreature.collider != null)
            {
                Creature creatureScript = hitYourCreature.collider.gameObject.GetComponent<Creature>();
                Card cardScript = GetComponent<Card>();

                if (!creatureScript.HaveAbility(cardScript.GetAbility()))
                {
                    creatureScript.AddAbility(choosenCard, cardScript.GetAbility());
                    choosenCard.layer = LayerMask.NameToLayer("Ability");
                }
                else transform.position = startPosition;
            }
            // ¬озвращение карты в руку при неверном ходе
            else
            {
                transform.position = startPosition;
            }

            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, -1), speed);

            isDrag = false;
            choosenCard = null;
        }
    }
}
