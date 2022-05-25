using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public KeyCode AllField;
    public KeyCode TopCreatures;
    public KeyCode RightCreatures;
    public KeyCode BottomCreatures;
    public KeyCode LeftCreatures;
    public KeyCode Test1;
    public KeyCode Test2;
    public float speed;
    public float speedRotation;

    void Start()
    {
        //this.transform.position = new Vector3(-1983, -1700, -1500);
        this.transform.position = new Vector3(-1983, -1100, -2500);
        //this.transform.rotation = new Quaternion(-20, 0, 0, 1);
        this.transform.Rotate(-22, 0, 0);
    }

    void Update()
    {
        if (Input.GetKey(AllField))
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(-1983, -1100, -1000), speed);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.Euler(-22, 0, 0), Time.deltaTime * speedRotation);
        }
        else if (Input.GetKey(TopCreatures))
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(-1983, -8, -1088), speed);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * speedRotation);
        }
        else if (Input.GetKey(BottomCreatures))
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(-1983, -706, -1088), speed);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * speedRotation);
        }
    }
}
