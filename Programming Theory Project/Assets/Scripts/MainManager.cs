using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject tosterType;
    public GameObject Spawnplace;
    [SerializeField] List<GameObject> buttons = new List<GameObject>();
    [SerializeField] List<GameObject> toasters = new List<GameObject>();
    public GameObject menuScreen;
    Regular_Toster regularTosterScript;
    private GameObject findInitialToster;
    bool isTosterPicked = false;
    public float speed = 1;
    public Vector3 velocity = Vector3.zero;

    //cameras

    public Camera mainCamera, sideCamera;
    bool isInPlace = false;
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
            else if (findInitialToster.name == "deluxeToaster(Clone)")
            {
                buttons[0].SetActive(true);
                buttons[1].SetActive(true);
            }
            else if (findInitialToster.name == "UltimateToster(Clone)")
            {
                buttons[0].SetActive(true);
                buttons[1].SetActive(true);
                buttons[2].SetActive(true);
            }
        }
    }

    public void SpawnPickedToster(int number)
    {
        Instantiate(toasters[number], Spawnplace.transform.position, Quaternion.identity);
        isTosterPicked = true;
        menuScreen.gameObject.SetActive(false);
        StartCoroutine(fadeCamera());

    }

    IEnumerator fadeCamera()
    {
        float time = 0;

        while (time < 2.5f)
        {
            mainCamera.transform.position = Vector3.SmoothDamp(mainCamera.transform.position, sideCamera.transform.position,
            ref velocity, speed * Time.deltaTime);
            mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, sideCamera.transform.rotation, time/4);

            time += Time.deltaTime;
            yield return null;

        }
        mainCamera.transform.position =  sideCamera.transform.position;
        mainCamera.transform.rotation = sideCamera.transform.rotation;

        mainCamera.gameObject.SetActive(false);
        sideCamera.gameObject.SetActive(true);
        isInPlace = true;
    }
}
