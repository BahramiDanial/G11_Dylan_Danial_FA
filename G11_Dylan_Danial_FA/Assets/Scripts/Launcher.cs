using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public float angleDegrees = 30;
    public float speed = 100;
    public float startHeight = 1;

    public GameObject projectileToCopy;

    private void Update()
    {
        Vector3 launchVelocity = new Vector3(10, 16);
        Vector3 startPosition = new Vector3(0, startHeight, 0);


        if (Input.GetKeyDown(KeyCode.Space))
        {
            Mathf.Sin(angleDegrees);
            Debug.Log("Lunch");
            GameObject newObject = Instantiate(projectileToCopy);
            PhysicObject physicObject = newObject.GetComponent<PhysicObject>();



            physicObject.velocity = launchVelocity;

            physicObject.transform.position = startPosition;


        }
        Debug.DrawLine(startPosition, startPosition + launchVelocity, Color.red);
    }

}
