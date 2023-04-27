using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sideCameraMovement : MonoBehaviour
{
    // Start is called before the first frame update
    float speed = 0.3f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {    if (GameObject.Find("Camera (1)") != null){
         transform.Rotate(0.0f, -Input.GetAxis("Horizontal") * speed, 0.0f);
    }
    }
}
