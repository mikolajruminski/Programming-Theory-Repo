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

    protected Vector3 offset = new Vector3(0, 1f, 0);
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected IEnumerator ToastBread()
    {
        StartCoroutine(playSounds());
        StartCoroutine(dragZaciagnik());
        float timeElapsed = 0;
        while (timeElapsed < toastTime)
        {
            Debug.Log(timeElapsed);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        Instantiate(toastedBread, transform.position + offset, new Quaternion(-0.707106829f, 0, 0, 0.707106829f));
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

    protected IEnumerator dragZaciagnik()
    {
      zaciagnikScript = GameObject.Find("Canvas").GetComponent<zaciagnijZaciagnik>();

      zaciagnikScript.zaciagnij();
      yield return new WaitForSeconds(toastTime);
      zaciagnikScript.push();
    }


    public void startToasting()
    {
        StartCoroutine(ToastBread());
    }
}
