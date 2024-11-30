using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;


public class PhysicEngine : MonoBehaviour
{
    static PhysicEngine instance = null;
    public static PhysicEngine Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<PhysicEngine>();
            }
            return instance;
        }

    }
    public float dt = 0.02f;
    public Vector3 gravityAcceleration = new Vector3(0f, -10f, 0f);
    public List<PhysicObject> Objekts = new List<PhysicObject>();

    void FixedUpdate()
    {
        foreach (PhysicObject ObjektA in Objekts)
        {
            Vector3 prevPos = ObjektA.transform.position;
            Vector3 newPos = ObjektA.transform.position + ObjektA.velocity * dt;

            //position
            if (!ObjektA.isStatic)
            {
                ObjektA.transform.position = newPos;
            }

            // velocity
            ObjektA.velocity += gravityAcceleration * dt * ObjektA.gravityScale;

            //drag
            float dragCoefficient = 0.98f;
            ObjektA.velocity *= dragCoefficient;


            Debug.DrawLine(prevPos, newPos, Color.green, 10);
            Debug.DrawLine(ObjektA.transform.position, ObjektA.transform.position + ObjektA.velocity, Color.red);
        }
        foreach (PhysicObject Objekt in Objekts)
        {
            Objekt.GetComponent<Renderer>().material.color = Color.white;

        }


        for (int iA = 0; iA < Objekts.Count; iA++)
        {
            PhysicObject ObjektA = Objekts[iA];

            for (int iB = iA + 1; iB < Objekts.Count; iB++)
            {
                PhysicObject ObjektB = Objekts[iB];

                if (ObjektA == ObjektB) continue;



                bool isOverlapping = false;

                if (ObjektA.shape.GetShape() == G11FhysicShape.Shape.Sphere &&
                   ObjektB.shape.GetShape() == G11FhysicShape.Shape.Sphere)
                {
                    isOverlapping = CollideSpheres((FhysicShapeSphere)ObjektA.shape, (FhysicShapeSphere)ObjektB.shape);


                }
                else if (ObjektA.shape.GetShape() == G11FhysicShape.Shape.Sphere &&
                         ObjektB.shape.GetShape() == G11FhysicShape.Shape.Plane)
                {
                    isOverlapping = IsOverlappingSpheresPlane((FhysicShapeSphere)ObjektA.shape, (FhysicShapePlane)ObjektB.shape);

                }
                else if (ObjektA.shape.GetShape() == G11FhysicShape.Shape.Plane &&
                         ObjektB.shape.GetShape() == G11FhysicShape.Shape.Sphere)
                {

                    isOverlapping = IsOverlappingSpheresPlane((FhysicShapeSphere)ObjektB.shape, (FhysicShapePlane)ObjektA.shape);

                }
                if (isOverlapping)
                {
                    Debug.DrawLine(ObjektA.transform.position, ObjektB.transform.position, Color.red);


                    ObjektA.GetComponent<Renderer>().material.color = Color.red;
                    ObjektB.GetComponent<Renderer>().material.color = Color.red;

                }
            }
        }

    }
    public static bool CollideSpheres(FhysicShapeSphere sphereA, FhysicShapeSphere sphereB)
    {
        Vector3 displacement = sphereA.transform.position - sphereB.transform.position;
        float distance = displacement.magnitude;
        float overlap = (sphereA.radius - sphereB.radius) / distance;

        if (overlap > 0.0f)
        {
            Vector3 collisionNormal_BtoA = (displacement / distance); // points from B to A;
            Vector3 mtv = collisionNormal_BtoA * overlap;
            sphereA.transform.position += mtv * 0.5f;
            sphereB.transform.position -= mtv * 0.5f;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsOverlappingSpheres(PhysicObject ObjekA, PhysicObject ObjekB)
    {
        Debug.Log("Checking collision between: " + ObjekA.name + " and " + ObjekB.name);
        UnityEngine.Vector3 Displacement = ObjekA.transform.position - ObjekB.transform.position;
        float distance = Displacement.magnitude;


        float radiusA = ((FhysicShapeSphere)ObjekA.shape).radius;
        float radiusB = ((FhysicShapeSphere)ObjekB.shape).radius;

        return distance < radiusA + radiusB;
    }

    public bool IsOverlappingSpheresPlane(FhysicShapeSphere sphere, FhysicShapePlane plane)
    {
        Vector3 planeToSphere = sphere.transform.position - plane.transform.position;
        float positionAlongNorma1 = Vector3.Dot(planeToSphere, plane.Normal());

        float distanceToPlane = Mathf.Abs(positionAlongNorma1);

        float overlap = sphere.radius - distanceToPlane;
        if (overlap > 0)
        {
            sphere.transform.position += plane.Normal() * overlap;

            return true;
        }
        return false;

    }
}