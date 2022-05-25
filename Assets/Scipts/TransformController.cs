using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformController : MonoBehaviour
{
    public float rotationSpeed;
    private int targetAngle;
    private int targetRotateX;
    private int targetRotateY;
    private int targetRotateZ;
    private Quaternion oldAngle;
    private Quaternion oldRotation;
    private Vector3 targetPosition;
    private bool move = false;
    private bool flip = false;
    private bool rotate = false;
    private bool blockYellow = false;

    void Start()
    {
        targetAngle = 180;
        targetPosition = this.transform.position;
        var material = this.gameObject.GetComponent<Renderer>().material;
        material.EnableKeyword("_EMISSION");
    }

    void Update()
    {
        if (flip)
        {
            if (System.Math.Abs(transform.eulerAngles.y - targetAngle) > 0.09)
            {
                Quaternion target = Quaternion.Euler(270, targetAngle, 0);
                transform.rotation = Quaternion.Lerp(oldAngle, target, Time.deltaTime * rotationSpeed);
                oldAngle = transform.rotation;
            }
            else flip = false;
        }

        if (rotate)
        {
            if (System.Math.Abs(transform.eulerAngles.y - targetRotateZ) > 0.09)
            {
                //Debug.Log(transform.eulerAngles.x + " " + transform.eulerAngles.y + " " + transform.eulerAngles.z);
                Quaternion targetRotation = Quaternion.Euler(targetRotateX, targetRotateY, targetRotateZ);
                transform.rotation = Quaternion.Lerp(oldRotation, targetRotation, Time.deltaTime * rotationSpeed);
                oldRotation = transform.rotation;
            }
            else rotate = false;
        }

        if (move)
        {
            if (Vector3.Distance(transform.position, targetPosition) > 0.09)
            {
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * rotationSpeed);
            }
            else move = false;
        }
    }

    public void FlipCard()
    {
        //Debug.Log(transform.eulerAngles.x + " " + transform.eulerAngles.y + " " + transform.eulerAngles.z);

        if (targetAngle == 0)
            targetAngle = 180;
        else targetAngle = 0;
        flip = true;
    }

    public void Rotate()
    {
        //Debug.Log(targetRotateX + " " + targetRotateY + " " + targetRotateZ);
        if (targetRotateZ == -90)
        {
            targetRotateX = -90;
            targetRotateY = 0;
            targetRotateZ = 0;
            rotate = true;
        }
        else
        {
            targetRotateX = 90;
            targetRotateY = 90;
            targetRotateZ = -90;
            rotate = true;
        }
    }

    public void MoveTo(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
        move = true;
    }

    public void EnableHighLiteRed()
    {
        var material = this.gameObject.GetComponent<Renderer>().material;

        //Debug.Log("6");
        material.SetColor("_EmissionColor", new Color(0.2f, 0, 0));
        blockYellow = true;
    }

    public void DisableHighLiteRed()
    {
        var material = this.gameObject.GetComponent<Renderer>().material;

        material.SetColor("_EmissionColor", new Color(0.0f, 0, 0));
        blockYellow = false;
    }

    public void EnableHighLiteYellow()
    {
        if (!blockYellow)
        {
            var material = this.gameObject.GetComponent<Renderer>().material;

            material.SetColor("_EmissionColor", new Color(0.2f, 0.2f, 0));
        }
    }

    public void DisableHighLiteYellow()
    {
        if (!blockYellow)
        {
            var material = this.gameObject.GetComponent<Renderer>().material;

            material.SetColor("_EmissionColor", new Color(0.0f, 0, 0));
        }
    }
}
