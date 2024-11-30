using UnityEngine;
using static G11FhysicShape;
public class FhysicShapePlane : G11FhysicShape
{
    public override Shape GetShape()
    {
        return Shape.Plane;
    }
    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public Vector3 Normal()
    {
        return transform.up;
    }
}
