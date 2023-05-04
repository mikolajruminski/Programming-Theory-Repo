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
        Instantiate(GrilledCheese, transform.position, transform.rotation);
        StartCoroutine(jumpToasts(GameObject.Find("grilled cheeese wypieczony(Clone)"), new Vector3(-0.00700000022f,2.25999999f,-8.44799995f)));
    }

    public void startToastingGrilledCheese()
    {
        StartCoroutine(ToastGrilledCheese());
    }
}
