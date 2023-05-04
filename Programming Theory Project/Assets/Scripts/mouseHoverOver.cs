using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseHoverOver : MonoBehaviour
{
    private Renderer rendere;
    // Start is called before the first frame update
    private Color existingColor;
    private MainManager mainManager;

    [SerializeField] float m_Hue;
    [SerializeField] float m_Saturation;
    [SerializeField] float m_Value;

    private Vector3 offset = new Vector3(0.005f, 0, 0);

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
            rendere.material.color = Color.HSVToRGB(m_Hue / 360, m_Saturation / 100, m_Value / 100);
        }
        else if (mainManager.sideCamera.gameObject != null && rendere.gameObject.name != "Cube")
        {
            rendere.material.color = Color.HSVToRGB(m_Hue / 360, m_Saturation / 100, m_Value / 100);
        }
    }
    private void OnMouseExit()
    {
        rendere.material.color = existingColor;
    }

    private void OnMouseDown()
    {
        StartCoroutine(mainManager.SideToCloseupCamera());
        if (mainManager.sideCamera.gameObject != null && rendere.gameObject.name != "Cube")
        {
            StartCoroutine(pushButtons(offset));
        }
    }

    private void OnMouseUp()
    {
        if (mainManager.sideCamera.gameObject != null && rendere.gameObject.name != "Cube")
        {
            StartCoroutine(pushButtons(-offset));

        }
    }

    //IENUMERATORS
    private IEnumerator pushButtons(Vector3 pushOffset)
    {
        if (mainManager.sideCamera.gameObject != null && rendere.gameObject.name != "Cube")
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
