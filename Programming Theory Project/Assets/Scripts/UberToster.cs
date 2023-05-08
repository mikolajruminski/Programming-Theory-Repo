using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UberToster : DeluxeToster
{
    // Start is called before the first frame update
    public GameObject GrilledCheese;
    void Start()
    {
        zaciagnikScript = GameObject.Find("Canvas").GetComponent<zaciagnijZaciagnik>();
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    protected IEnumerator ToastGrilledCheese()
    {
        if (mainManager.hasSpawned == false)
        {
            mainManager.hasSpawned = true;
            StartCoroutine(playSounds());
            lampSmallLights(2, 0);
            zaciagnikScript.zaciagnij();
            GameObject _wskaz = GameObject.FindGameObjectWithTag("pivot");
            float timeElapsed = 0;
            while (timeElapsed < toastTime)
            {
                _wskaz.transform.rotation = Quaternion.Lerp(_wskaz.transform.rotation, Quaternion.Euler(new Vector3(-140f, 0, 0)), (timeElapsed / 45) / toastTime);
                Debug.Log(timeElapsed);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            zaciagnikScript.push();
            Instantiate(GrilledCheese, transform.position, transform.rotation);
            StartCoroutine(jumpToasts(GameObject.Find("grilled cheeese wypieczony(Clone)"), new Vector3(-0.00700000022f, 2.25999999f, -8.44799995f)));
            StartCoroutine(mainManager.lookAtToast());
        }

    }

    public void startToastingGrilledCheese()
    {
        StartCoroutine(ToastGrilledCheese());
    }
}
