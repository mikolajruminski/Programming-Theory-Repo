using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regular_Toster : MonoBehaviour
{
    // Start is called before the first frame update

    public float toastTime;
    public GameObject toastedBread;
    MainManager mainManager;
    zaciagnijZaciagnik zaciagnikScript;
    public AnimationCurve animationCurve;

    private Vector3 endPosition = new Vector3(-0.04f, 2.26f, -8.53f);

    protected Vector3 offset = new Vector3(0, 1f, 0);
    void Start()
    {

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
       // StartCoroutine(dragZaciagnik());
        float timeElapsed = 0;
        while (timeElapsed < toastTime)
        {
            Debug.Log(timeElapsed);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        Instantiate(toastedBread, transform.position, new Quaternion(-0.707106829f, 0, 0, 0.707106829f));
        StartCoroutine(jumpToasts(GameObject.Find("tostWypieczony(Clone)"), endPosition));
        StartCoroutine(mainManager.lookAtToast());
    }

    protected IEnumerator playSounds()
    {
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
        mainManager.src.clip = mainManager.src2;
        mainManager.src.Play();

        yield return new WaitForSeconds(toastTime);

        mainManager.src.clip = mainManager.src3;
        mainManager.src.Play();
    }

    /*protected IEnumerator dragZaciagnik()
    {
        zaciagnikScript = GameObject.Find("Canvas").GetComponent<zaciagnijZaciagnik>();

        zaciagnikScript.zaciagnij();
        yield return new WaitForSeconds(toastTime);
        zaciagnikScript.push();
    }
    */

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
}

