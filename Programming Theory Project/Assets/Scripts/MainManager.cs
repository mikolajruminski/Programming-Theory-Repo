using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject tosterType;
    [SerializeField] List<GameObject> buttons = new List<GameObject>();
    Regular_Toster regularTosterScript;
    private GameObject findInitialToster;
    void Start()
    {
        findInitialToster = GameObject.FindGameObjectWithTag("Toaster");
      if (findInitialToster.name == "DeluxeToster")
        {
           buttons[1].SetActive(true);
        }
     else if (findInitialToster.name == "UberToster")
     {
        buttons[1].SetActive(true);
        buttons[2].SetActive(true);
     }
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void ToastBread()
    {
        tosterType = GameObject.FindGameObjectWithTag("Toaster");
        regularTosterScript = tosterType.GetComponent<Regular_Toster>();
        if (regularTosterScript != null)
        {
            regularTosterScript.startToasting();
        }
    }

    public void ToastBagel() 
    {
        DeluxeToster deluxeTosterScript;
        deluxeTosterScript = GameObject.FindGameObjectWithTag("Toaster").GetComponent<DeluxeToster>();
        if (deluxeTosterScript != null)
        {
            deluxeTosterScript.startToastingBagel();
        }
    }

     public void ToastGrilledCheese() 
    {
        UberToster uberTosterScript;
        uberTosterScript = GameObject.FindGameObjectWithTag("Toaster").GetComponent<UberToster>();
        if (uberTosterScript != null)
        {
            uberTosterScript.startToastingGrilledCheese();
        }
    }
}
