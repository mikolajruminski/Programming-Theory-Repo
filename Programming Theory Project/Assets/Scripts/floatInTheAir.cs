using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floatInTheAir : MonoBehaviour
{
    [SerializeField] Vector3 pos1 = new Vector3(-0.04f, 2.26f, -8.53f);
    [SerializeField] Vector3 pos2 = new Vector3(-0.04f, 2.28f, -8.53f);
    [SerializeField] AnimationCurve _curve;
    [SerializeField] float speed = 1.19f;

    // Start is called before the first frame update
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
       Float();
    }

    void Float() 
    {
      float time = Mathf.PingPong(Time.time * speed, 1);
      transform.position = Vector3.Lerp(pos1, pos2, _curve.Evaluate(time));
    }

   
}
