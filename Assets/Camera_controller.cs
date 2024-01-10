using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_controller : MonoBehaviour
{
    private Transform target;
    private Vector3 cameraDirection;
    public float height_distance;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        cameraDirection = new Vector3(target.transform.position.x,height_distance,target.transform.position.z-5f);
        transform.position = Vector3.Slerp(transform.position, cameraDirection, 10 * Time.deltaTime);
    }
}
