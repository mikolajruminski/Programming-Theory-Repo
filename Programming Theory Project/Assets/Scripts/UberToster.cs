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
   
    public void startToastingGrilledCheese()
    {
        StartCoroutine(Toast(2, GrilledCheese, transform.rotation, "grilled cheeese wypieczony(Clone)", new Vector3(-0.00700000022f, 2.25999999f, -8.44799995f)));
    }
}
