using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zaciagnijZaciagnik : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject zaciagnik;
    private Vector3 amountofZaciagniecie = new Vector3(0,0.003f,0);
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void zaciagnij()
    {
        StartCoroutine(zaciagnijZaciagnikScript(amountofZaciagniecie, 0.6f));
    }

    public void push() 
    {
        StartCoroutine(zaciagnijZaciagnikScript(-amountofZaciagniecie, 0.2f));
    }

    public IEnumerator zaciagnijZaciagnikScript(Vector3 distance, float duration)
    {
        zaciagnik = GameObject.FindGameObjectWithTag("zaciagnik");

        float time = 0;

        while (time < duration)
        {
            zaciagnik.transform.position = Vector3.Lerp(zaciagnik.transform.localPosition, zaciagnik.transform.localPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

    }
}
