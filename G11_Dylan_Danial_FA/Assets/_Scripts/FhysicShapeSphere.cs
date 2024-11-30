using UnityEngine;

public class FhysicShapeSphere : G11FhysicShape
{
    public float radius = 1;
    public override Shape GetShape()
    {
        return Shape.Sphere;
    }
    public void UpdateScale()
    {
        transform.localScale =
            new Vector3(radius, radius, radius) * 2f;
    }
    private void OnValidate()
    {
        UpdateScale();
    }

    private void Update()
    {
        UpdateScale();
    }
}