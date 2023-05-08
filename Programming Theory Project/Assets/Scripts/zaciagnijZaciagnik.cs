using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zaciagnijZaciagnik : MonoBehaviour
{
    GameObject zaciagnik;
    Vector3 amountofZaciagniecie = new Vector3(0, 0.028f, 0);
    public void zaciagnij()
    {
        StartCoroutine(zaciagnijZaciagnikScript(amountofZaciagniecie, 5, 0.6f));
    }

    public void push()
    {
        StartCoroutine(zaciagnijZaciagnikScript((-amountofZaciagniecie / 3), 0.05f, 0.20f));
    }

    public IEnumerator zaciagnijZaciagnikScript(Vector3 distance, float time2, float duration)
    {
        zaciagnik = GameObject.FindGameObjectWithTag("zaciagnik");
       
        float time = 0;

        while (time < duration)
        {
            zaciagnik.transform.position = Vector3.Lerp(zaciagnik.transform.position, zaciagnik.transform.position - distance, (time / time2) / duration);
            
            time += Time.deltaTime;
            yield return null;
        }
    }
}
