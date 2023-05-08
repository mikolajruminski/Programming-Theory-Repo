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

    public List<GameObject> lights = new List<GameObject>();
    [SerializeField] GameObject Canvas;

    public GameObject menuScreen;
    Regular_Toster regularTosterScript;
    zaciagnijZaciagnik zaciagnikScript;
    private GameObject findInitialToster;
    bool isTosterPicked = false;
    public float speed = 1;
    public Vector3 velocity = Vector3.zero;
    public bool isSideCameraActive = false;
    [SerializeField] private AnimationCurve _curve;

    public bool hasSpawned = false;

    //cameras

    public Camera mainCamera, sideCamera, closeUpCamera, toastViewCamera;
    //
    public bool isInPlace = false;
    public bool isCameraCloseUp = false;

    private mouseHoverOver mouseHoverOverScript;
    // sound effects

    public AudioSource src;
    public AudioSource srcMainManager;
    [SerializeField] public List<AudioClip> reflectorSounds = new List<AudioClip>();
    [SerializeField] List<AudioClip> swooshSounds = new List<AudioClip>();

    //Last lerping, 3 positions + rotation
    Vector3 rotPos1 = new Vector3(-19f, -269.81f, 0f);
    Vector3 rotPos2 = new Vector3(0, -180, 0);
    Vector3 pos1 = new Vector3(-2.073f, 1.53f, -7.375f);
    Vector3 pos2 = new Vector3(-0.03f, 1.53f, -6.913f);
    Vector3 toastViewCameraInitialPosition;
    Quaternion toastViewCameraInitialRotation;
    Vector3 closeUpCameraInitialPosition = new Vector3(-2.07299995f, 1.52999997f, -8.51399994f);
    Vector3 closeUpCameraInitialRotation = new Vector3(0, -269.891f, 0);
    void Start()
    {
        StartCoroutine(loadMenu());
        toastViewCameraInitialPosition = toastViewCamera.transform.position;
        toastViewCameraInitialRotation = toastViewCamera.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ToastBread()
    {
        tosterType = GameObject.FindGameObjectWithTag("Toaster");
        getScriptForToaster();
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
                buttons[0].transform.localPosition = new Vector3(-93.8000031f, -44.9000015f, 32);

            }
            else if (findInitialToster.name == "deluxeToaster(Clone)")
            {
                buttons[0].SetActive(true);
                buttons[1].SetActive(true);
                buttons[0].transform.localPosition = new Vector3(-124.400002f, -104.599998f, 32);
                buttons[1].transform.localPosition = new Vector3(-124, -140.300003f, 32);

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
            Instantiate(toasters[number], position, transform.rotation * Quaternion.Euler(0f, -90f, 0f));
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

    //COROUTINES
    IEnumerator fadeCamera()
    {
        float time = 0;

        while (time < 3.5f)
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
        src.clip = reflectorSounds[0];
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
        isSideCameraActive = true;
        SpawnButtons();
    }

    public IEnumerator lookAtToast()
    {
        float lerpSpeed = 0.2f;
        float time = 0;
        srcMainManager.clip = swooshSounds[0];
        srcMainManager.Play();

        while (time < 0.10f)
        {
            closeUpCamera.transform.rotation = Quaternion.Lerp(closeUpCamera.transform.rotation, Quaternion.Euler(rotPos1), _curve.Evaluate(time));
            time += Time.deltaTime * lerpSpeed;
            yield return null;
        }


        time = 0;

        srcMainManager.clip = swooshSounds[2];
        srcMainManager.Play();

        while (time < 0.05f)
        {
            closeUpCamera.transform.position = Vector3.Lerp(closeUpCamera.transform.position, pos1, _curve.Evaluate(time));

            time += Time.deltaTime * lerpSpeed;
            yield return null;
        }



        time = 0;

        srcMainManager.clip = swooshSounds[3];
        srcMainManager.Play();

        while (time < 0.10f)
        {
            closeUpCamera.transform.position = Vector3.Lerp(closeUpCamera.transform.position, pos2, _curve.Evaluate(time));
            closeUpCamera.transform.rotation = Quaternion.Lerp(closeUpCamera.transform.rotation, Quaternion.Euler(rotPos2), _curve.Evaluate(time));

            time += Time.deltaTime * lerpSpeed;
            yield return null;
        }



        time = 0;

        srcMainManager.clip = swooshSounds[4];
        srcMainManager.Play();

        while (time < 0.12f)
        {
            closeUpCamera.transform.position = Vector3.Lerp(closeUpCamera.transform.position, toastViewCameraInitialPosition, _curve.Evaluate(time));
            closeUpCamera.transform.rotation = Quaternion.Lerp(closeUpCamera.transform.rotation, toastViewCameraInitialRotation, _curve.Evaluate(time));

            time += Time.deltaTime * lerpSpeed;
            yield return null;
        }
        closeUpCamera.transform.position = toastViewCameraInitialPosition;
        closeUpCamera.transform.rotation = toastViewCameraInitialRotation;
        
        closeUpCamera.gameObject.SetActive(false);
        toastViewCamera.gameObject.SetActive(true);
        toastViewCamera.transform.position = toastViewCameraInitialPosition;
        toastViewCamera.transform.rotation = toastViewCameraInitialRotation;
        isCameraCloseUp = true;
    }

    public IEnumerator lookAtToasterFromToastView()
    {
        getScriptForToaster();
        regularTosterScript.lampSmallLights(0, 1);
        float lerpSpeed = 0.2f;
        float time = 0;
        time = 0;

        srcMainManager.clip = swooshSounds[4];
        srcMainManager.Play();

        while (time < 0.12f)
        {
            toastViewCamera.transform.position = Vector3.Lerp(toastViewCamera.transform.position, pos2, _curve.Evaluate(time));
            toastViewCamera.transform.rotation = Quaternion.Lerp(toastViewCamera.transform.rotation, Quaternion.Euler(new Vector3(359.677063f, 177.963547f, 359.689362f)), _curve.Evaluate(time));

            time += Time.deltaTime * lerpSpeed;
            yield return null;
        }


        time = 0;
        srcMainManager.clip = swooshSounds[3];
        srcMainManager.Play();

        while (time < 0.10f)
        {
            toastViewCamera.transform.position = Vector3.Lerp(toastViewCamera.transform.position, pos1, _curve.Evaluate(time));

            time += Time.deltaTime * lerpSpeed;
            yield return null;
        }

        time = 0;

        srcMainManager.clip = swooshSounds[2];
        srcMainManager.Play();
        destroyToasts();
        
        while (time < 0.08f)
        {
            toastViewCamera.transform.position = Vector3.Lerp(toastViewCamera.transform.position, closeUpCameraInitialPosition, _curve.Evaluate(time));
            toastViewCamera.transform.rotation = Quaternion.Lerp(toastViewCamera.transform.rotation, Quaternion.Euler(closeUpCameraInitialRotation), _curve.Evaluate(time));

            time += Time.deltaTime * lerpSpeed;
            yield return null;
        }

        closeUpCamera.transform.position = closeUpCameraInitialPosition;
        closeUpCamera.transform.rotation = Quaternion.Euler(closeUpCameraInitialRotation);

        isSideCameraActive = true;
        isCameraCloseUp = false;

        toastViewCamera.gameObject.SetActive(false);
        closeUpCamera.gameObject.SetActive(true);

        hasSpawned = false;
    }

    IEnumerator loadMenu()
    {
        yield return new WaitForSeconds(2);
        Canvas.gameObject.SetActive(true);
        src.clip = reflectorSounds[3];
        lights[3].gameObject.SetActive(true);
        src.Play();
    }

    void destroyToasts() 
    {
        foreach (GameObject toastInstances in GameObject.FindGameObjectsWithTag("Toast")) 
        {
            Destroy(toastInstances);
        }
    }

    void getScriptForToaster() 
    {
        regularTosterScript = tosterType.GetComponent<Regular_Toster>();
        zaciagnikScript = GameObject.Find("Canvas").GetComponent<zaciagnijZaciagnik>();
    }
}
