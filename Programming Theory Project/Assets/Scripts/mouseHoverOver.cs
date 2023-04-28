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
    void Start()
    {
        rendere = GetComponent<Renderer>();
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
        existingColor = rendere.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter() {
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
    }
}
