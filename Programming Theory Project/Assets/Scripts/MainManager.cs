using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject tosterType;
    private Vector3 spawnPlace = new Vector3(0f, 3.96f, -5.91f);
    [SerializeField] List<GameObject> buttons = new List<GameObject>();
    [SerializeField] List<GameObject> toasters = new List<GameObject>();
    public GameObject menuScreen;
    Regular_Toster regularTosterScript;
    private GameObject findInitialToster;
    bool isTosterPicked = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      SpawnButtons();
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

    void SpawnButtons()
    {
        if (isTosterPicked)
        {
            findInitialToster = GameObject.FindGameObjectWithTag("Toaster");
            if (findInitialToster.name == "RegularToster(Clone)")
            {
                buttons[0].SetActive(true);
            }
            else if (findInitialToster.name == "DeluxeToster(Clone)")
            {
                buttons[0].SetActive(true);
                buttons[1].SetActive(true);
            }
            else if (findInitialToster.name == "UberToster(Clone)")
            {
                buttons[0].SetActive(true);
                buttons[1].SetActive(true);
                buttons[2].SetActive(true);
            }
        }
    }

    public void SpawnPickedToster(int number) 
    {
      Instantiate(toasters[number], spawnPlace, transform.rotation);
      isTosterPicked = true;
      menuScreen.gameObject.SetActive(false);

    }
}
