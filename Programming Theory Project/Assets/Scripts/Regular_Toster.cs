using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regular_Toster : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] protected float toastTime;
    public GameObject toastedBread;
    protected MainManager mainManager;
    protected zaciagnijZaciagnik zaciagnikScript;
    public AnimationCurve animationCurve;
    public List<GameObject> smallLights = new List<GameObject>();
    Vector3 zaciagnikInitialPosition;

    private Vector3 endPosition = new Vector3(-0.04f, 2.26f, -8.53f);

    protected Vector3 offset = new Vector3(0, 1f, 0);
    float m_Hue = 8, m_Saturation = 88, m_Value = 100, m_value2 = 53;
    void Start()
    {
        getInitialScripts();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void startToasting()
    {
        StartCoroutine(ToastBread());
    }

    //CORUTINES
    protected IEnumerator ToastBread()
    {
        if (mainManager.hasSpawned == false)
        {
            mainManager.hasSpawned = true;
            StartCoroutine(playSounds());
            zaciagnikScript.zaciagnij();
            float timeElapsed = 0;
            GameObject _wskaz = GameObject.FindGameObjectWithTag("pivot");
            lampSmallLights(0, 0);
            while (timeElapsed < toastTime)
            {
                _wskaz.transform.rotation = Quaternion.Lerp(_wskaz.transform.rotation, Quaternion.Euler(new Vector3(-140f, 0, 0)), (timeElapsed / 45) / toastTime);
                Debug.Log(timeElapsed);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            zaciagnikScript.push();
            Instantiate(toastedBread, transform.position, new Quaternion(-0.707106829f, 0, 0, 0.707106829f));
            StartCoroutine(jumpToasts(GameObject.Find("tostWypieczony(Clone)"), endPosition));
            StartCoroutine(mainManager.lookAtToast());
            mainManager.hasSpawned = true;
        }

    }

    protected IEnumerator playSounds()
    {
        mainManager.src.clip = mainManager.reflectorSounds[1];
        mainManager.src.Play();

        yield return new WaitForSeconds(toastTime);

        mainManager.src.clip = mainManager.reflectorSounds[2];
        mainManager.src.Play();
    }


    protected IEnumerator jumpToasts(GameObject toastType, Vector3 endPos)
    {

        float lerpSpeed = 0.1f;
        float time = 0;
        while (time < 0.1f)
        {
            toastType.transform.position = Vector3.Lerp(toastType.transform.position, endPos, animationCurve.Evaluate(time));

            time += Time.deltaTime * lerpSpeed;
            yield return null;
        }


    }

    public void lampSmallLights(int number, int option)
    {
        if (option == 0)
        {
            if (GameObject.Find("UltimateToster(Clone)") != null)
            {
                foreach (GameObject smalLight in GameObject.FindGameObjectsWithTag("smallLight"))
                {
                    smallLights.Add(smalLight);
                }
                smallLights[number].GetComponent<Renderer>().material.color = Color.HSVToRGB(m_Hue / 360, m_Saturation / 100, m_Value / 100);
            }
        }
        else
        {
            for (int i = 0; i < smallLights.Count; i++)
            {
                smallLights[i].GetComponent<Renderer>().material.color = Color.HSVToRGB(m_Hue / 360, m_Saturation / 100, m_value2 / 100);
            }

        }

    }

    protected void getInitialScripts()
    {
        zaciagnikScript = GameObject.Find("Canvas").GetComponent<zaciagnijZaciagnik>();
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
    }

}

