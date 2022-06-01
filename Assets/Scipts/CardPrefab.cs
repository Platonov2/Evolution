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
    public KeyCode escape;

    private bool isDrag = false;
    private GameObject choosenCard = null;
    private Vector3 startPosition;
    private float timeHolding;
    private Vector3 newCreaturePosition;
    public static GameObject attackingCreature;
    public GameObject hoveredCreature;

    private Card cardScript;

    void Start()
    {
        cardScript = GetComponent<Card>();
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

            if (Input.GetKey(changeAbility))
            {
                cardScript.ChangeAbility();
            }
        }

        RaycastHit hitYourCreature;
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitYourCreature, Mathf.Infinity, yourCreatureLayer);

        if (hitYourCreature.collider != null)// && attackingCreature == null)
        {
            Creature creatureScript = hitYourCreature.collider.gameObject.GetComponent<Creature>();

            if (attackingCreature == null && choosenCard != null)
            {
                Card cardScript = GetComponent<Card>();

                if (creatureScript.CanAddAbility(cardScript.GetAbility()))
                {
                    TransformController transformController = hitYourCreature.collider.gameObject.GetComponent<TransformController>();
                    transformController.EnableHighLiteYellow();
                    hoveredCreature = hitYourCreature.collider.gameObject;
                }
            }

            // Если выбран хищник, то подсвечиваются только те существа, которых он может съесть
            if (attackingCreature != null && attackingCreature != this)
            {
                Creature attackerScript = attackingCreature.GetComponent<Creature>();
                bool canEat = attackerScript.CanEat(hitYourCreature.collider.gameObject);

                if (canEat)
                {
                    TransformController transformController = hitYourCreature.collider.gameObject.GetComponent<TransformController>();
                    transformController.EnableHighLiteYellow();
                    hoveredCreature = hitYourCreature.collider.gameObject;
                }
            }
            // Подсветка карты жёлтым цветом при наведении на готового к атаке хищника
            if (creatureScript.CanAttack())
            {
                TransformController transformController = hitYourCreature.collider.gameObject.GetComponent<TransformController>();
                transformController.EnableHighLiteYellow();
                hoveredCreature = hitYourCreature.collider.gameObject;
            }
        }
        // Отключение жёлтой подсветки при исчезновении курсора
        else if (hitYourCreature.collider == null && hoveredCreature != null)// && attackingCreature == null)
        {
            TransformController transformController = hoveredCreature.GetComponent<TransformController>();
            hoveredCreature = null;
            transformController.DisableHighLiteYellow();
        }

        // При нажатии на Esc происхдит снятие выделение атакующего существа
        if (Input.GetKey(escape))
        {
            TransformController transformController = attackingCreature.GetComponent<TransformController>();
            transformController.DisableHighLiteRed();
            attackingCreature = null;
        }
    }

    // Обработка нажатия на карту
    void OnMouseDown()
    {
        RaycastHit hitHandCard;
        RaycastHit hitYourCreature;
        RaycastHit hitOpponentCreature;

        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitHandCard, Mathf.Infinity, yourHandCardLayer);
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitYourCreature, Mathf.Infinity, yourCreatureLayer);
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitOpponentCreature, Mathf.Infinity, opponentCreatureLayer);

        // Перетягивать можно только свои карты из руки
        if (hitHandCard.collider != null)
        {
            isDrag = true;
            choosenCard = hitHandCard.collider.gameObject;
            startPosition = this.transform.position;
        }

        // Одиночное нажатие на существо
        if (hitYourCreature.collider != null)
        {
            Creature creatureScript = hitYourCreature.collider.gameObject.GetComponent<Creature>();
            TransformController transformController = hitYourCreature.collider.gameObject.GetComponent<TransformController>();

            // Если выбрано атакующее существо, то его атака и выделение снимается
            if (hitYourCreature.collider.gameObject == attackingCreature)
            {
                transformController.DisableHighLiteRed();
                attackingCreature = null;
            }
            // Если выбрано существо со способностью "хищник", то оно становится атакующим и выделяется красным цветом
            else if (creatureScript.CanAttack())
            {
                attackingCreature = hitYourCreature.collider.gameObject;
                transformController.EnableHighLiteRed();
            }
        }

        // Если хищник атакует, то при нажатии на другое существо оно становится жертвой
        if ((hitOpponentCreature.collider != null || (hitYourCreature.collider != null && attackingCreature != null && hitYourCreature.collider.gameObject != attackingCreature)))
        {
            Creature creatureScript = attackingCreature.GetComponent<Creature>();

            // Нападение на своё существо
            if (hitOpponentCreature.collider != null)
            {
                creatureScript.Attack(hitOpponentCreature.collider.gameObject);
            }
            // Нападение на существо оппонента
            else if (hitYourCreature.collider != null)
            {
                creatureScript.Attack(hitYourCreature.collider.gameObject);
            }

            TransformController transformController = attackingCreature.GetComponent<TransformController>();
            transformController.DisableHighLiteRed();

            creatureScript.Attack(hitYourCreature.collider.gameObject);

            attackingCreature = null;
        }
    }

    // Обработка "отпускания" карты
    void OnMouseUp()
    {
        RaycastHit hitHandCard;
        RaycastHit hitCreaturesField;
        RaycastHit hitYourCreature;
        RaycastHit hitEmptyCreature;
        RaycastHit hitOpponentCreature;

        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitHandCard, Mathf.Infinity, yourHandCardLayer);
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitCreaturesField, Mathf.Infinity, yourCreaturesFieldLayer);
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitYourCreature, Mathf.Infinity, yourCreatureLayer);
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitEmptyCreature, Mathf.Infinity, emptyCreatureLayer);
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitOpponentCreature, Mathf.Infinity, emptyCreatureLayer);

        if (hitHandCard.collider != null)
        {
            // Если перетянуть карту на место создания нового существа, то создастся новое существо (-_-)
            if (hitEmptyCreature.collider != null)
            {
                player.CreateCreature(choosenCard);
            }
            // Если перетянуть карту на существо, то она сыграется в качестве способности
            else if (hitYourCreature.collider != null || hitOpponentCreature.collider != null)
            {
                Creature creatureScript = null;
                TransformController transformController = null;

                // Узнаём на чьё существо была сыграна способность
                if (hitYourCreature.collider != null)
                {
                    creatureScript = hitYourCreature.collider.gameObject.GetComponent<Creature>();
                    transformController = hitYourCreature.collider.gameObject.GetComponent<TransformController>();
                }
                else if (hitOpponentCreature.collider != null)
                {
                    creatureScript = hitOpponentCreature.collider.gameObject.GetComponent<Creature>();
                    transformController = hitOpponentCreature.collider.gameObject.GetComponent<TransformController>();
                }

                Card cardScript = GetComponent<Card>();

                if (creatureScript.CanAddAbility(cardScript.GetAbility()))
                {
                    creatureScript.AddAbility(choosenCard, cardScript.GetAbility());
                    choosenCard.layer = LayerMask.NameToLayer("Ability");

                    transformController.DisableHighLiteYellow();
                }
                else transform.position = startPosition;
            }
            // Возвращение карты в руку при неверном ходе
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
