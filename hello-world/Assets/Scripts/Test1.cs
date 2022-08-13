using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test1 : MonoBehaviour
{

    public float speed = 0.5f;
    public float boundary = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0f, 0f, speed);
        if (transform.position.z < -boundary || transform.position.z > boundary) speed *= -1f;
    }
    
}
