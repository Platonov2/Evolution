using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformController : MonoBehaviour
{
    public float rotationSpeed;
    private int targetAngle;
    private Quaternion oldAngle;
    private Vector3 targetPosition;
    private bool move = false;

    void Start()
    {
        targetAngle = 0;
        targetPosition = this.transform.position;
    }

    void Update()
    {
        if (move)
        {
            if (System.Math.Abs(transform.eulerAngles.y - targetAngle) > 0.09)
            {
                Quaternion target = Quaternion.Euler(270, targetAngle, 0);
                transform.rotation = Quaternion.Lerp(oldAngle, target, Time.deltaTime * rotationSpeed);
                oldAngle = transform.rotation;
            }
            else move = false;

            if (Vector3.Distance(transform.position, targetPosition) > 0.09)
            {
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * rotationSpeed);
            }
            else move = false;
        }
    }

    public void FlipCard()
    {
        if (targetAngle == 0)
            targetAngle = 180;
        else targetAngle = 0;
        move = true;
    }

    public void MoveTo(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
        move = true;
    }

    public IEnumerator Wait(int seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
