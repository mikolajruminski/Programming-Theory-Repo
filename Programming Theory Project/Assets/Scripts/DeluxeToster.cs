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
        Instantiate(toastedBagel, new Vector3(-0.175762713f,2.24206877f,-8.16699982f), gameObject.transform.rotation);
    }

    public void startToastingBagel()
    {
        StartCoroutine(ToastBagel());
    }
}
