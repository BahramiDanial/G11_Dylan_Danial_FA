using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motion : MonoBehaviour
{
    public float x, y = 0;
    public float a = 1;
    public float b = 3;
    public float t = 0;

    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        float dt = 1.0f / 60.0f;
        x = x + (-Mathf.Sin(t * a) * a * b * dt);
        y = y + (-Mathf.Cos(t * a) * a * b * dt);

        transform.position = new Vector3(x, y, transform.position.z);

        t += dt;
    }
}
