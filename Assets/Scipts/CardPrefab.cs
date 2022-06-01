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
        // ������������� ����� ��������
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

            // ���� ������ ������, �� �������������� ������ �� ��������, ������� �� ����� ������
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
            // ��������� ����� ����� ������ ��� ��������� �� �������� � ����� �������
            if (creatureScript.CanAttack())
            {
                TransformController transformController = hitYourCreature.collider.gameObject.GetComponent<TransformController>();
                transformController.EnableHighLiteYellow();
                hoveredCreature = hitYourCreature.collider.gameObject;
            }
        }
        // ���������� ����� ��������� ��� ������������ �������
        else if (hitYourCreature.collider == null && hoveredCreature != null)// && attackingCreature == null)
        {
            TransformController transformController = hoveredCreature.GetComponent<TransformController>();
            hoveredCreature = null;
            transformController.DisableHighLiteYellow();
        }

        // ��� ������� �� Esc ��������� ������ ��������� ���������� ��������
        if (Input.GetKey(escape))
        {
            TransformController transformController = attackingCreature.GetComponent<TransformController>();
            transformController.DisableHighLiteRed();
            attackingCreature = null;
        }
    }

    // ��������� ������� �� �����
    void OnMouseDown()
    {
        RaycastHit hitHandCard;
        RaycastHit hitYourCreature;
        RaycastHit hitOpponentCreature;

        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitHandCard, Mathf.Infinity, yourHandCardLayer);
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitYourCreature, Mathf.Infinity, yourCreatureLayer);
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitOpponentCreature, Mathf.Infinity, opponentCreatureLayer);

        // ������������ ����� ������ ���� ����� �� ����
        if (hitHandCard.collider != null)
        {
            isDrag = true;
            choosenCard = hitHandCard.collider.gameObject;
            startPosition = this.transform.position;
        }

        // ��������� ������� �� ��������
        if (hitYourCreature.collider != null)
        {
            Creature creatureScript = hitYourCreature.collider.gameObject.GetComponent<Creature>();
            TransformController transformController = hitYourCreature.collider.gameObject.GetComponent<TransformController>();

            // ���� ������� ��������� ��������, �� ��� ����� � ��������� ���������
            if (hitYourCreature.collider.gameObject == attackingCreature)
            {
                transformController.DisableHighLiteRed();
                attackingCreature = null;
            }
            // ���� ������� �������� �� ������������ "������", �� ��� ���������� ��������� � ���������� ������� ������
            else if (creatureScript.CanAttack())
            {
                attackingCreature = hitYourCreature.collider.gameObject;
                transformController.EnableHighLiteRed();
            }
        }

        // ���� ������ �������, �� ��� ������� �� ������ �������� ��� ���������� �������
        if ((hitOpponentCreature.collider != null || (hitYourCreature.collider != null && attackingCreature != null && hitYourCreature.collider.gameObject != attackingCreature)))
        {
            Creature creatureScript = attackingCreature.GetComponent<Creature>();

            // ��������� �� ��� ��������
            if (hitOpponentCreature.collider != null)
            {
                creatureScript.Attack(hitOpponentCreature.collider.gameObject);
            }
            // ��������� �� �������� ���������
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

    // ��������� "����������" �����
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
            // ���� ���������� ����� �� ����� �������� ������ ��������, �� ��������� ����� �������� (-_-)
            if (hitEmptyCreature.collider != null)
            {
                player.CreateCreature(choosenCard);
            }
            // ���� ���������� ����� �� ��������, �� ��� ��������� � �������� �����������
            else if (hitYourCreature.collider != null || hitOpponentCreature.collider != null)
            {
                Creature creatureScript = null;
                TransformController transformController = null;

                // ����� �� ��� �������� ���� ������� �����������
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
            // ����������� ����� � ���� ��� �������� ����
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
