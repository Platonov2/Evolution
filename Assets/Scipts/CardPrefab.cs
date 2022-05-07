using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardPrefab : MonoBehaviour
{
    public Player player;
    public float speed;
    public GameObject yourCreaturesField;
    public LayerMask yourCreaturesFieldLayer;
    public LayerMask yourCreatureLayer;
    public LayerMask opponentCreatureLayer;
    public LayerMask yourHandCardLayer;
    public GameObject emptyCard;
    public GameObject emptyCardPlaceholder;

    private bool isDrag = false;
    private GameObject choosenCard = null;
    private Vector3 startPosition;
    private float timeHolding;
    private int emptyCardIndex;
    private Vector3 newCreaturePosition;


    TransformController transformController;

    void Start()
    {
        transformController = GetComponent<TransformController>();
        emptyCardIndex = -1;
    }

    void Update()
    {
        // Перетягивание карты курсором
        if (isDrag)
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(hit.point.x, hit.point.y, -50), speed);
            }

            RaycastHit hitCreaturesField;
            RaycastHit hitCreature;

            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitCreaturesField, Mathf.Infinity, yourCreaturesFieldLayer);
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitCreature, Mathf.Infinity, yourCreatureLayer);

            if (hitCreaturesField.collider != null && hitCreature.collider == null)
            {
                timeHolding += Time.deltaTime;
                //Debug.Log(timeHolding);

                if (timeHolding > 0.8 && emptyCardIndex == -1)
                {
                    emptyCardIndex = player.GetLastIndex();
                    player.CreateEmptyCreature(emptyCard, emptyCardIndex);
                }
            }
            else
            {
                timeHolding = 0;

                if (emptyCardIndex != -1)
                {
                    player.RemoveEmptyCard();
                    emptyCard.transform.SetParent(emptyCardPlaceholder.transform, true);
                    emptyCardIndex = -1;
                }
            }
        }
    }

    /*void checkCreatureCreation(float deltaTime, Vector3 mousePosition)
    {
        this.timeHolding += deltaTime;

        if (timeHolding >= 10)
        {
            int index = player.GetNearLeftCardIndex(mousePosition);
            player.CreateCreature(emptyCard, index);
        }
    }*/

    // Обработка нажатия на карту
    void OnMouseDown()
    {
        RaycastHit hit;
        // Перетягивать можно только свои карты из руки
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, yourHandCardLayer))
        {
            //Debug.Log(hit.collider.name);
            isDrag = true;
            choosenCard = hit.collider.gameObject;
            startPosition = this.transform.position;
        }
    }

    // Обработка "отпускания" карты
    void OnMouseUp()
    {
        RaycastHit hitHandCard;
        RaycastHit hitCreaturesField;
        RaycastHit hitCreature;

        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitHandCard, Mathf.Infinity, yourHandCardLayer);
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitCreaturesField, Mathf.Infinity, yourCreaturesFieldLayer);
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitCreature, Mathf.Infinity, yourCreatureLayer);

        if (hitHandCard.collider != null)
        {
            // Возвращение карты в руку при неверном ходе
            if (hitCreaturesField.collider == null && hitCreature.collider == null)
            {
                //Debug.Log("Старт" + startPosition);
                //Debug.Log("Текущее положение" + this.transform.position);
                transform.position = startPosition;
                //transform.position = Vector3.MoveTowards(this.transform.position, startPosition, speed);
            }
            // Создание нового существа
            else if (hitCreaturesField.collider != null && hitCreature.collider == null)
            {
                //StartCoroutine(transformController.MoveTo(new Vector3(yourCreaturesField.transform.position.x, yourCreaturesField.transform.position.y, -1)));
                newCreaturePosition = emptyCard.transform.position;
                emptyCard.transform.SetParent(emptyCardPlaceholder.transform, true);
                //transformController.MoveTo(new Vector3(yourCreaturesField.transform.position.x, yourCreaturesField.transform.position.y, -1));
                transformController.MoveTo(newCreaturePosition);
                transformController.FlipCard();
                //transformController.Wait();
                //StartCoroutine(WaitAndCreateCreature(2, choosenCard));
                //player.CreateCreature(choosenCard, 0);
                //player.CreateCreature(choosenCard, emptyCardIndex, "YourCreature");
                player.CreateCreature(choosenCard);
            }

            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, -1), speed);

            isDrag = false;
            choosenCard = null;
        }
    }

    /*public IEnumerator WaitAndCreateCreature(float seconds, GameObject choosenCard)
    {
        yield return new WaitForSeconds(seconds);
        player.CreateCreature(choosenCard, 0, "YourCreature");
    }*/
}
