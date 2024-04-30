using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Transform player;
    public float sensitivity;
    public float xRot;
    public float minY = -90f;
    public float maxY = 90f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position;
        MouseLook();
    }

    void MouseLook(){
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivity;
        
        xRot -= mouseY;
        
        //clamp the vertical rotation
        xRot = Mathf.Clamp(xRot, minY, maxY);

        
        //rotate the camera
        transform.localRotation = Quaternion.Euler(xRot, 0, 0);
        
        //rotate the player with the camera
        player.Rotate(Vector3.up * mouseX);
    }
}
