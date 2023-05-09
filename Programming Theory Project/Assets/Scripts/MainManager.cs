using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    //GameObjects
    GameObject tosterType;
    [SerializeField] GameObject Canvas;
    [SerializeField] GameObject menuScreen;
    GameObject findInitialToster;
    [SerializeField] List<GameObject> buttons = new List<GameObject>();
    [SerializeField] List<GameObject> toasters = new List<GameObject>();
    [SerializeField] List<GameObject> lights = new List<GameObject>();

    //Scripts
    Regular_Toster regularTosterScript;
    zaciagnijZaciagnik zaciagnikScript;
    DeluxeToster deluxeTosterScript;
    UberToster uberTosterScript;
    //Variables
    bool isTosterPicked = false;
    [SerializeField] float speed = 1f;
    float speed2 = 130;
    public bool isSideCameraActive = false;
    [SerializeField] private AnimationCurve _curve;
    public bool hasSpawned = false;
    //cameras

    public Camera mainCamera, sideCamera, closeUpCamera, toastViewCamera;
    public bool isInPlace = false;
    public bool isCameraCloseUp = false;

    // sound effects
    public AudioSource src;
    public AudioSource srcMainManager;
    [SerializeField] public List<AudioClip> reflectorSounds = new List<AudioClip>();
    [SerializeField] List<AudioClip> swooshSounds = new List<AudioClip>();

    //Last lerping, 3 positions + rotation

    [SerializeField] Vector3 velocity = Vector3.zero;
    Vector3 rotPos1 = new Vector3(-19f, -269.81f, 0f);
    Vector3 rotPos2 = new Vector3(0, -180, 0);
    Vector3 pos1 = new Vector3(-2.073f, 1.53f, -7.375f);
    Vector3 pos2 = new Vector3(-0.03f, 1.53f, -6.913f);
    Vector3 mainCameraInitialPosition, toastViewCameraInitialPosition,  sideCameraInitialPosition, closeUpCameraInitialPosition;
    Quaternion mainCameraInitialRotation, toastViewCameraInitialRotation, sideCameraInitialRotation, closeUpCameraInitialRotation;



    void Start()
    {
        StartCoroutine(loadMenu());
        getInitialCameraTransforms();

    }

    void LateUpdate()
    {
        if (sideCamera.gameObject != null && Input.GetKey(KeyCode.L) && isInPlace)
        {
            StartCoroutine(fadeMainCamera(2));
        }
        else if (closeUpCamera.gameObject != null && Input.GetKey(KeyCode.N) && isSideCameraActive)
        {
            StartCoroutine(SideToCloseupCamera(2));
        }
    }

    public void Toast(int number)
    {
        switch (number)
        {
            case 0:
                tosterType = GameObject.FindGameObjectWithTag("Toaster");
                getScriptForToaster();
                if (regularTosterScript != null)
                {
                    regularTosterScript.startToasting();
                }
                break;

            case 1:
                deluxeTosterScript = GameObject.FindGameObjectWithTag("Toaster").GetComponent<DeluxeToster>();
                if (deluxeTosterScript != null)
                {
                    deluxeTosterScript.startToastingBagel();
                }
                break;

            case 2:
                uberTosterScript = GameObject.FindGameObjectWithTag("Toaster").GetComponent<UberToster>();
                if (uberTosterScript != null)
                {
                    uberTosterScript.startToastingGrilledCheese();
                }
                break;
        }
    }

    void SpawnButtons(int number)
    {
        if (isTosterPicked && number == 0)
        {
            findInitialToster = GameObject.FindGameObjectWithTag("Toaster");
            if (findInitialToster.name == "RegularToster(Clone)")
            {
                buttons[0].SetActive(true);
                buttons[0].transform.localPosition = new Vector3(-93.8000031f, -44.9000015f, 32);

            }
            else if (findInitialToster.name == "deluxeToaster(Clone)")
            {
                for (int i = 0; i < buttons.Count - 1; i++)
                {
                    buttons[i].SetActive(true);
                }

                buttons[0].transform.localPosition = new Vector3(-124.400002f, -104.599998f, 32);
                buttons[1].transform.localPosition = new Vector3(-124, -140.300003f, 32);

            }
            else if (findInitialToster.name == "UltimateToster(Clone)")
            {
                for (int i = 0; i < buttons.Count; i++)
                {
                    buttons[i].SetActive(true);
                }
            }
        }
        else
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].SetActive(false);
            }
        }
    }

    public void SpawnPickedToster(int number, Vector3 position)
    {
        if (number == 0)
        {
            destroyObjects(1);
            Instantiate(toasters[number], position, transform.rotation * Quaternion.Euler(0f, -90f, 0f));
            isTosterPicked = true;
            menuScreen.gameObject.SetActive(false);
            StartCoroutine(fadeMainCamera(1));
        }
        else
        {
            destroyObjects(1);
            Instantiate(toasters[number], position, Quaternion.identity);
            isTosterPicked = true;
            menuScreen.gameObject.SetActive(false);
            StartCoroutine(fadeMainCamera(1));
        }


    }

    //COROUTINES
    IEnumerator fadeMainCamera(int number)
    {
        float time = 0;

        switch (number)
        {
            case 1:

                time = 0;

                while (time < 3.5f)
                {
                    mainCamera.transform.position = Vector3.SmoothDamp(mainCamera.transform.position, sideCameraInitialPosition,
                    ref velocity, speed2 * Time.deltaTime);
                    mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, sideCameraInitialRotation, time / 12);

                    time += Time.deltaTime;
                    yield return null;

                }
                mainCamera.transform.position = sideCameraInitialPosition;
                sideCamera.transform.position = sideCameraInitialPosition;
                sideCamera.transform.rotation = sideCameraInitialRotation;
                mainCamera.transform.rotation = sideCameraInitialRotation;

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

                break;

            case 2:

                time = 0;
                isInPlace = false;

                while (time < 3.5f)
                {
                    sideCamera.transform.position = Vector3.SmoothDamp(sideCamera.transform.position, mainCameraInitialPosition,
                    ref velocity, speed2 * Time.deltaTime);
                    sideCamera.transform.rotation = Quaternion.Lerp(sideCamera.transform.rotation, mainCameraInitialRotation, time / 36);

                    time += Time.deltaTime;
                    yield return null;

                }
                sideCamera.transform.position = mainCameraInitialPosition;
                sideCamera.transform.rotation = mainCameraInitialRotation;
                mainCamera.transform.position = mainCameraInitialPosition;
                mainCamera.transform.rotation = mainCameraInitialRotation;

                mainCamera.gameObject.SetActive(true);
                sideCamera.gameObject.SetActive(false);

                lights[0].gameObject.SetActive(false);
                lights[1].gameObject.SetActive(false);
                src.clip = reflectorSounds[0];
                src.Play();
                yield return new WaitForSeconds(1);
                lights[2].gameObject.SetActive(false);
                src.Play();
                Canvas.gameObject.SetActive(true);
                menuScreen.gameObject.SetActive(true);

                break;

        }



    }

    public IEnumerator SideToCloseupCamera(int number)
    {
        float time = 0;

        switch (number)
        {
            case 1:

                time = 0;
                isInPlace = false;
                while (time < 3.5f)
                {
                    sideCamera.transform.position = Vector3.SmoothDamp(sideCamera.transform.position, closeUpCameraInitialPosition,
                    ref velocity, speed * Time.deltaTime);
                    sideCamera.transform.rotation = Quaternion.Lerp(sideCamera.transform.rotation, closeUpCameraInitialRotation, time / 48);

                    time += Time.deltaTime;
                    yield return null;

                }

                sideCamera.transform.position = closeUpCameraInitialPosition;
                closeUpCamera.transform.position = closeUpCameraInitialPosition;
                closeUpCamera.transform.rotation = closeUpCameraInitialRotation;
                sideCamera.transform.rotation = closeUpCameraInitialRotation;
                sideCamera.gameObject.SetActive(false);
                closeUpCamera.gameObject.SetActive(true);
                isSideCameraActive = true;
                SpawnButtons(0);

                break;

            case 2:

                time = 0;
                isSideCameraActive = false;
                SpawnButtons(1);
                while (time < 3.2f)
                {
                    closeUpCamera.transform.position = Vector3.SmoothDamp(closeUpCamera.transform.position, sideCameraInitialPosition,
                    ref velocity, speed * Time.deltaTime);
                    closeUpCamera.transform.rotation = Quaternion.Lerp(closeUpCamera.transform.rotation, sideCameraInitialRotation, time / 16);

                    time += Time.deltaTime;
                    yield return null;

                }

                closeUpCamera.transform.position = sideCameraInitialPosition;
                sideCamera.transform.position = sideCameraInitialPosition;
                closeUpCamera.transform.rotation = sideCameraInitialRotation;
                sideCamera.transform.rotation = sideCameraInitialRotation;

                sideCamera.gameObject.SetActive(true);
                closeUpCamera.gameObject.SetActive(false);

                isInPlace = true;

                break;
        }


    }


    public IEnumerator lookAtToast(int number)
    {
        switch (number)
        {
            case 1:

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

                break;

            case 2:

                getScriptForToaster();
                regularTosterScript.lampSmallLights(0, 1);
                lerpSpeed = 0.2f;
                time = 0;
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
                destroyObjects(0);

                while (time < 0.08f)
                {
                    toastViewCamera.transform.position = Vector3.Lerp(toastViewCamera.transform.position, closeUpCameraInitialPosition, _curve.Evaluate(time));
                    toastViewCamera.transform.rotation = Quaternion.Lerp(toastViewCamera.transform.rotation, closeUpCameraInitialRotation, _curve.Evaluate(time));

                    time += Time.deltaTime * lerpSpeed;
                    yield return null;
                }

                closeUpCamera.transform.position = closeUpCameraInitialPosition;
                closeUpCamera.transform.rotation = closeUpCameraInitialRotation;

                isSideCameraActive = true;
                isCameraCloseUp = false;

                toastViewCamera.gameObject.SetActive(false);
                closeUpCamera.gameObject.SetActive(true);

                hasSpawned = false;

                break;

        }

    }

    IEnumerator loadMenu()
    {
        yield return new WaitForSeconds(2);
        Canvas.gameObject.SetActive(true);
        src.clip = reflectorSounds[3];
        lights[3].gameObject.SetActive(true);
        src.Play();
    }

    void destroyObjects(int number)
    {
        switch (number)
        {
            case 0:
                foreach (GameObject toastInstances in GameObject.FindGameObjectsWithTag("Toast"))
                {
                    Destroy(toastInstances);
                }
                break;
            case 1:
                foreach (GameObject toasterInstances in GameObject.FindGameObjectsWithTag("Toaster"))
                {
                    Destroy(toasterInstances);
                }
                break;
        }

    }

    void getScriptForToaster()
    {
        tosterType = GameObject.FindGameObjectWithTag("Toaster");
        regularTosterScript = tosterType.GetComponent<Regular_Toster>();
        zaciagnikScript = GameObject.Find("Canvas").GetComponent<zaciagnijZaciagnik>();
    }

    void getInitialCameraTransforms()
    {
        toastViewCameraInitialPosition = toastViewCamera.transform.position;
        toastViewCameraInitialRotation = toastViewCamera.transform.rotation;
        mainCameraInitialPosition = mainCamera.transform.position;
        mainCameraInitialRotation = mainCamera.transform.rotation;
        sideCameraInitialPosition = sideCamera.transform.position;
        sideCameraInitialRotation = sideCamera.transform.rotation;
        closeUpCameraInitialPosition = closeUpCamera.transform.position;
        closeUpCameraInitialRotation = closeUpCamera.transform.rotation;

    }
}
