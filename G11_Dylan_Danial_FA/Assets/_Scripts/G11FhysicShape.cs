using UnityEngine;

public abstract class G11FhysicShape : MonoBehaviour
{

    public enum Shape
    {
        Sphere,
        Plane,
        Halfspace
    }

    public abstract Shape GetShape();

}
