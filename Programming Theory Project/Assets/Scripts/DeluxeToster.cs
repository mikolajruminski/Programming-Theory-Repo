using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeluxeToster : Regular_Toster
{
    public GameObject toastedBagel;
    // Start is called before the first frame update
    void Start()
    {
        zaciagnikScript = GameObject.Find("Canvas").GetComponent<zaciagnijZaciagnik>();
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected IEnumerator ToastBagel()
    {
        if (mainManager.hasSpawned == false)
        {
            mainManager.hasSpawned = true;
            StartCoroutine(playSounds());
            float timeElapsed = 0;
            lampSmallLights(1, 0);
            GameObject _wskaz = GameObject.FindGameObjectWithTag("pivot");
            zaciagnikScript.zaciagnij();
            while (timeElapsed < toastTime)
            {
                _wskaz.transform.rotation = Quaternion.Lerp(_wskaz.transform.rotation, Quaternion.Euler(new Vector3(-140f, 0, 0)), (timeElapsed / 45) / toastTime);
                Debug.Log(timeElapsed);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            zaciagnikScript.push();
            Instantiate(toastedBagel, transform.position, gameObject.transform.rotation);
            StartCoroutine(jumpToasts(GameObject.Find("bagel wypieczony(Clone)"), new Vector3(-0.00700000022f, 2.25999999f, -8.28999996f)));
            StartCoroutine(mainManager.lookAtToast());

        }

    }

    public void startToastingBagel()
    {
        StartCoroutine(ToastBagel());
    }
}
