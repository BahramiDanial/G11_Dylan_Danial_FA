using UnityEngine;

public class Pig : PhysicObject
{
    public float Toughness = 10.0f; // Adjustable toughness value
    public bool IsDestroyed { get; private set; } = false;

    private void Start()
    {
        // Set the default color of pigs to green
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = Color.green;
        }
    }

    public void DestroyPig()
    {
        IsDestroyed = true;
        Destroy(gameObject);
    }
}
