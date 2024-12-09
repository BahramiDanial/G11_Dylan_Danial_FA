using UnityEngine;
using static G11FhysicShape;
public class FhysicShapePlane : G11FhysicShape
{
    public override Shape GetShape()
    {
        return Shape.Plane;
    }

    // Returns the position of the plane
    public Vector3 GetPosition()
    {
        return transform.position;
    }

    // Returns the normal of the plane (either custom or based on transform.up)
    public Vector3 Normal()
    {
        return transform.up.normalized; // Ensures the normal is always a unit vector
    }

}
