using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUIHandler : MonoBehaviour
{
    public int toasterNumber;
    private Button button;
    MainManager mainManagerScript;

    public Vector3 positionOffset;
    // Start is called before the first frame update
    void Start()
    {
        mainManagerScript = GameObject.Find("MainManager").GetComponent<MainManager>();
        button = GetComponent<Button>();

        button.onClick.AddListener(passVariable);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void passVariable() 
    {
      mainManagerScript.SpawnPickedToster(toasterNumber, positionOffset);
    }
}
