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
      float timeElapsed = 0;
      while (timeElapsed < toastTime)
      {
        Debug.Log(timeElapsed);
        timeElapsed += Time.deltaTime;
        yield return null;
      }
     Instantiate(toastedBagel, transform.position + offset, gameObject.transform.rotation);
    }

    public void startToastingBagel() 
    {
        StartCoroutine(ToastBagel());
    }
}
