using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sideCameraMovement : MonoBehaviour
{
    // Start is called before the first frame update
    MainManager mainManager;
    float speed = 0.3f;
    void Start()
    {
      mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (GameObject.Find("Camera (1)") != null && mainManager.isInPlace)
        {
            transform.Rotate(0.0f, -Input.GetAxis("Horizontal") * speed, 0.0f);

        }
    }
}
