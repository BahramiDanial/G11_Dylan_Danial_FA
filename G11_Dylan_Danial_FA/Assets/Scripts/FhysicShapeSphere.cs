using UnityEngine;

public class FhysicShapeSphere : G11FhysicShape
{
    public float radius = 1;
    private bool scaleUpdated = false;

    public override Shape GetShape()
    {
        return Shape.Sphere;
    }

    public void UpdateScale()
    {
        transform.localScale = new Vector3(radius, radius, radius) * 2f;
        scaleUpdated = true;
    }

    private void OnValidate()
    {
        UpdateScale();
    }

    private void Update()
    {
        // Only update scale if radius has changed
        if (!scaleUpdated)
        {
            UpdateScale();
        }
    }
}
