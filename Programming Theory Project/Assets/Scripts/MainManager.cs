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

    public Camera mainCamera, sideCamera, closeUpCamera;
    public List<GameObject> lights = new List<GameObject>();
    //
    public bool isInPlace = false;
    // sound effects

    public AudioSource src;
    public AudioClip src1;
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
                buttons[0].transform.localPosition = new Vector3(-124.400002f,-104.599998f,32);
                buttons[1].transform.localPosition = new Vector3(-124,-140.300003f,32);
                
            }
            else if (findInitialToster.name == "UltimateToster(Clone)")
            {
                buttons[0].SetActive(true);
                buttons[1].SetActive(true);
                buttons[2].SetActive(true);
            }
        }
    }

    public void SpawnPickedToster(int number, Vector3 position)
    {
        if (number == 0)
        {
        Instantiate(toasters[number], position, transform.rotation * Quaternion.Euler (0f, -90f, 0f));
        isTosterPicked = true;
        menuScreen.gameObject.SetActive(false);
        StartCoroutine(fadeCamera());
        }
        else 
        {
        Instantiate(toasters[number], position, Quaternion.identity);
        isTosterPicked = true;
        menuScreen.gameObject.SetActive(false);
        StartCoroutine(fadeCamera());
        }


    }

    IEnumerator fadeCamera()
    {
        float time = 0;

        while (time < 2.85f)
        {
            mainCamera.transform.position = Vector3.SmoothDamp(mainCamera.transform.position, sideCamera.transform.position,
            ref velocity, speed * Time.deltaTime);
            mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, sideCamera.transform.rotation, time / 12);

            time += Time.deltaTime;
            yield return null;

        }
        mainCamera.transform.position = sideCamera.transform.position;
        mainCamera.transform.rotation = sideCamera.transform.rotation;

        mainCamera.gameObject.SetActive(false);
        sideCamera.gameObject.SetActive(true);
        lights[0].gameObject.SetActive(true);
        lights[1].gameObject.SetActive(true);
        src.clip = src1;
        src.Play();
        yield return new WaitForSeconds(1);
        lights[2].gameObject.SetActive(true);
        src.Play();

        isInPlace = true;
    }

   public IEnumerator SideToCloseupCamera() 
    {

      float time = 0;
      isInPlace = false;
        while (time < 4f)
        {
            sideCamera.transform.position = Vector3.SmoothDamp(sideCamera.transform.position, closeUpCamera.transform.position,
            ref velocity, speed * Time.deltaTime);
            sideCamera.transform.rotation = Quaternion.Lerp(sideCamera.transform.rotation, closeUpCamera.transform.rotation, time / 36);

            time += Time.deltaTime;
            yield return null;

        }
         
        sideCamera.transform.position = closeUpCamera.transform.position;
        sideCamera.transform.rotation = closeUpCamera.transform.rotation;
        sideCamera.gameObject.SetActive(false);
        closeUpCamera.gameObject.SetActive(true);
    }
}
