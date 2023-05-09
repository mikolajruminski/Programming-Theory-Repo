using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseHoverOver : MonoBehaviour
{
    Renderer rendere;
    Color existingColor;
    MainManager mainManager;
    [SerializeField] float m_Hue, m_Saturation, m_Value;
    Vector3 offset = new Vector3(0.005f, 0, 0);

    void Start()
    {
        rendere = GetComponent<Renderer>();
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
        existingColor = rendere.material.color;
    }

    private void OnMouseEnter()
    {
        if (mainManager.isInPlace)
        {
            ChangeColor();
        }
        else if (mainManager.closeUpCamera.gameObject != null && rendere.gameObject.name != "Cube" && mainManager.isSideCameraActive)
        {
            ChangeColor();
        }
    }
    private void OnMouseExit()
    {
        rendere.material.color = existingColor;
    }

    void ChangeColor()
    {
        rendere.material.color = Color.HSVToRGB(m_Hue / 360, m_Saturation / 100, m_Value / 100);
    }

    void OnMouseDown()
    {
        if (mainManager.isInPlace)
        {
            StartCoroutine(mainManager.SideToCloseupCamera(1));
        }

        if (mainManager.sideCamera.gameObject != null && rendere.gameObject.name != "Cube" && mainManager.isSideCameraActive)
        {
            StartCoroutine(pushButtons(offset));
        }
    }

    void OnMouseUp()
    {
        if (mainManager.sideCamera.gameObject != null && rendere.gameObject.name != "Cube" && mainManager.isSideCameraActive)
        {
            StartCoroutine(pushButtons(-offset));

        }
    }

    //IENUMERATORS
    IEnumerator pushButtons(Vector3 pushOffset)
    {
        if (mainManager.sideCamera.gameObject != null && rendere.gameObject.name != "Cube" && mainManager.isSideCameraActive)
        {
            float time = 0;

            while (time < 0.3f)
            {
                gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, gameObject.transform.position + pushOffset, time / 6);

                time += Time.deltaTime;
                yield return null;

            }
        }
    }

}
