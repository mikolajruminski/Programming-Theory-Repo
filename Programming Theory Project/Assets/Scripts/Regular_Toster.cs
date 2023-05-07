using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regular_Toster : MonoBehaviour
{
    // Start is called before the first frame update

    public float toastTime;
    public GameObject toastedBread;
    protected MainManager mainManager;
    protected zaciagnijZaciagnik zaciagnikScript;
    public AnimationCurve animationCurve;

    public List<GameObject> smallLights = new List<GameObject>();

    private Vector3 endPosition = new Vector3(-0.04f, 2.26f, -8.53f);

    protected Vector3 offset = new Vector3(0, 1f, 0);
    float m_Hue = 8, m_Saturation = 88, m_Value = 100;
    void Start()
    {
      zaciagnikScript = GameObject.Find("Canvas").GetComponent<zaciagnijZaciagnik>();
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
        StartCoroutine(playSounds());
        zaciagnikScript.zaciagnij();
        float timeElapsed = 0;
        GameObject _wskaz = GameObject.FindGameObjectWithTag("pivot");
        lampSmallLights(0);
        while (timeElapsed < toastTime)
        {
            _wskaz.transform.rotation = Quaternion.Lerp(_wskaz.transform.rotation, Quaternion.Euler(new Vector3(-140f, 0,0)), (timeElapsed / 45)/toastTime);
            Debug.Log(timeElapsed);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        zaciagnikScript.push();
        Instantiate(toastedBread, transform.position, new Quaternion(-0.707106829f, 0, 0, 0.707106829f));
        StartCoroutine(jumpToasts(GameObject.Find("tostWypieczony(Clone)"), endPosition));
        StartCoroutine(mainManager.lookAtToast());
    }

    protected IEnumerator playSounds()
    {
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
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
        while (time < 1)
        {
          toastType.transform.position = Vector3.Lerp(toastType.transform.position, endPos, animationCurve.Evaluate(time));

          time += Time.deltaTime * lerpSpeed;
          yield return null;
        }
    }

    protected void lampSmallLights(int number) 
    {

        if (GameObject.Find("UltimateToster(Clone)") != null) 
        {
            foreach (GameObject smalLight in GameObject.FindGameObjectsWithTag("smallLight"))
            {
                smallLights.Add(smalLight);
            }
          smallLights[number].GetComponent<Renderer>().material.color = Color.HSVToRGB(m_Hue / 360, m_Saturation / 100, m_Value/100);
        }
    }
}

