using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regular_Toster : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] protected float toastTime;
    [SerializeField] GameObject toastedBread;
    protected MainManager mainManager;
    protected zaciagnijZaciagnik zaciagnikScript;
    [SerializeField] AnimationCurve animationCurve;
    List<GameObject> smallLights = new List<GameObject>();

    //Vectors
    Vector3 endPosition = new Vector3(-0.04f, 2.26f, -8.53f);
    float m_Hue = 8, m_Saturation = 88, m_Value = 100, m_value2 = 53;
    
    void Start()
    {
        getInitialScripts();
    }
    public void startToasting()
    {
        StartCoroutine(Toast(0, toastedBread, new Quaternion(-0.707106829f, 0, 0, 0.707106829f), "tostWypieczony(Clone)",  endPosition));
    }

    //CORUTINES
    protected IEnumerator Toast(int lightNumber, GameObject toastType, Quaternion rotation, string toastCloneName, Vector3 jumpPosition)
    {
        if (mainManager.hasSpawned == false)
        {
            mainManager.hasSpawned = true;
            StartCoroutine(playSounds());
            zaciagnikScript.zaciagnij();
            float timeElapsed = 0;
            GameObject _wskaz = GameObject.FindGameObjectWithTag("pivot");
            lampSmallLights(lightNumber, 0);
            while (timeElapsed < toastTime)
            {
                if (_wskaz != null)
                {
                    _wskaz.transform.rotation = Quaternion.Lerp(_wskaz.transform.rotation, Quaternion.Euler(new Vector3(-140f, 0, 0)), (timeElapsed / 45) / toastTime);
                }
                Debug.Log(timeElapsed);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            zaciagnikScript.push();
            Instantiate(toastType, transform.position, rotation);
            StartCoroutine(jumpToasts(GameObject.Find(toastCloneName), jumpPosition));
            StartCoroutine(mainManager.lookAtToast(1));
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

