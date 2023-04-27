using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regular_Toster : MonoBehaviour
{
    // Start is called before the first frame update

    public float toastTime;
    public GameObject toastedBread;

    protected Vector3 offset = new Vector3 (0, 1f, 0);
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected IEnumerator ToastBread() 
    {
      float timeElapsed = 0;
      while (timeElapsed < toastTime)
      {
        Debug.Log(timeElapsed);
        timeElapsed += Time.deltaTime;
        yield return null;
      }
     Instantiate(toastedBread, transform.position + offset, new Quaternion(-0.707106829f,0,0,0.707106829f));
    }


    public void startToasting() 
    {
        StartCoroutine(ToastBread());
    }
}
