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
    public float speed;

    void Start()
    {
        this.transform.position = new Vector3(-1983, -1046, -2524);
    }

    void Update()
    {
        if (Input.GetKey(AllField))
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(-1983, -1046, -2524), speed);
        }
        else if (Input.GetKey(TopCreatures))
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(-1983, -8, -1233), speed);
        }
        else if (Input.GetKey(RightCreatures))
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(-2000, -1046, -2524), speed);
        }
        else if (Input.GetKey(BottomCreatures))
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(-1983, -706, -1088), speed);
        }
        else if (Input.GetKey(LeftCreatures))
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(-750, -1046, -2524), speed);
        }
    }
}
