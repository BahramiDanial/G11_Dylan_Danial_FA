using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

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
        foreach (PhysicObject ObjektA in Objekts.ToArray()) 
        {
            if (ObjektA == null)
            {
                Objekts.Remove(ObjektA); 
                continue;
            }

            // Skip static objects entirely
            if (ObjektA.isStatic)
            {
                continue;
            }

            Vector3 prevPos = ObjektA.transform.position;
            Vector3 newPos = ObjektA.transform.position + ObjektA.velocity * dt;

            // Position update
            ObjektA.transform.position = newPos;

            // Velocity update
            ObjektA.velocity += gravityAcceleration * dt * ObjektA.gravityScale;

            // Apply drag
            float dragCoefficient = 0.98f;
            ObjektA.velocity *= dragCoefficient;
        }

        // Handle collisions
        for (int iA = 0; iA < Objekts.Count; iA++)
        {
            PhysicObject ObjektA = Objekts[iA];

            for (int iB = iA + 1; iB < Objekts.Count; iB++)
            {
                PhysicObject ObjektB = Objekts[iB];
                if (ObjektA == null || ObjektB == null || ObjektA == ObjektB)
                {
                    continue;
                }

                // Skip static objects in collisions
                if (ObjektA.isStatic && ObjektB.isStatic)
                {
                    continue;
                }

                bool isOverlapping = false;

                // Sphere-Sphere collision detection
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
                    // Momentum calculation
                    Vector3 relativeVelocity = ObjektA.velocity - ObjektB.velocity;
                    float momentumA = ObjektA.mass * relativeVelocity.magnitude;
                    float momentumB = ObjektB.mass * relativeVelocity.magnitude;

                    // Check if either object is a Pig
                    Pig pigA = ObjektA.GetComponent<Pig>();
                    Pig pigB = ObjektB.GetComponent<Pig>();

                    if (pigA != null && !pigA.IsDestroyed)
                    {
                        if (momentumA > pigA.Toughness)
                        {
                            pigA.DestroyPig();
                        }
                    }

                    if (pigB != null && !pigB.IsDestroyed)
                    {
                        if (momentumB > pigB.Toughness)
                        {
                            pigB.DestroyPig();
                        }
                    }
                }
            }
        }
    }

    public static bool CollideSpheres(FhysicShapeSphere sphereA, FhysicShapeSphere sphereB)
    {
        Vector3 displacement = sphereA.transform.position - sphereB.transform.position;
        float distance = displacement.magnitude;
        float combinedRadius = sphereA.radius + sphereB.radius;

        if (distance < combinedRadius)
        {
            Vector3 collisionNormal = displacement.normalized;
            float overlap = combinedRadius - distance;

            sphereA.transform.position += collisionNormal * overlap * 0.5f;
            sphereB.transform.position -= collisionNormal * overlap * 0.5f;

            return true;
        }

        return false;
    }

    public bool IsOverlappingSpheresPlane(FhysicShapeSphere sphere, FhysicShapePlane plane)
    {
        Vector3 planeToSphere = sphere.transform.position - plane.transform.position;
        float positionAlongNormal = Vector3.Dot(planeToSphere, plane.Normal());

        float distanceToPlane = Mathf.Abs(positionAlongNormal);
        float overlap = sphere.radius - distanceToPlane;

        if (overlap > 0)
        {
            sphere.transform.position += plane.Normal() * overlap;
            return true;
        }

        return false;
    }
}
