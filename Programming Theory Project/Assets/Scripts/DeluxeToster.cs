using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeluxeToster : Regular_Toster //inheritance
{
    public GameObject toastedBagel; //polymorphism
    
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

    public void startToastingBagel() // polymorphism
    {
        StartCoroutine(Toast(1, toastedBagel, gameObject.transform.rotation, "bagel wypieczony(Clone)", new Vector3(-0.00700000022f, 2.25999999f, -8.28999996f) ));
    }

}
