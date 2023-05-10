using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sideCameraMovement : MonoBehaviour
{
    // Start is called before the first frame update
    MainManager mainManager;
    float speed = 0.4f;
    void Start()
    {
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        RotateCamera();
        SwitchBackToSideCamera();

    }

    void RotateCamera()
    {
        if (mainManager.sideCamera.gameObject != null && mainManager.isInPlace)
        {
            GameObject.Find("spawnPlace").transform.Rotate(0.0f, -Input.GetAxis("Horizontal") * speed, 0.0f);



        }
        else if (mainManager.closeUpCamera.gameObject != null && mainManager.isCameraCloseUp)
        {
            GameObject.Find("ToastPosition").transform.Rotate(0.0f, -Input.GetAxis("Horizontal") * speed, 0.0f);

        }

    }

    void SwitchBackToSideCamera()
    {
        if (mainManager.isCameraCloseUp && Input.GetKeyDown(KeyCode.L) && mainManager.toastViewCamera.gameObject != null)
        {
            StartCoroutine(mainManager.lookAtToast(2));
        }
    }

}
