using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeluxeToster : Regular_Toster
{
    public GameObject toastedBagel;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    protected IEnumerator ToastBagel()
    {
        StartCoroutine(playSounds());
        float timeElapsed = 0;
        while (timeElapsed < toastTime)
        {
            Debug.Log(timeElapsed);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        Instantiate(toastedBagel, transform.position, gameObject.transform.rotation);
        StartCoroutine(jumpToasts(GameObject.Find("bagel wypieczony(Clone)"), new Vector3(-0.00700000022f,2.25999999f,-8.28999996f)));
        StartCoroutine(mainManager.lookAtToast());
    }

    public void startToastingBagel()
    {
        StartCoroutine(ToastBagel());
    }
}
