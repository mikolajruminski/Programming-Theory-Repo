using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UberToster : DeluxeToster
{
    // Start is called before the first frame update
    public GameObject GrilledCheese;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    protected IEnumerator ToastGrilledCheese()
    {
        StartCoroutine(playSounds());
        float timeElapsed = 0;
        while (timeElapsed < toastTime)
        {
            Debug.Log(timeElapsed);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        Instantiate(GrilledCheese, new Vector3(-0.0274314601f,2.58099985f,-8.36600018f), transform.rotation);
    }

    public void startToastingGrilledCheese()
    {
        StartCoroutine(ToastGrilledCheese());
    }
}
