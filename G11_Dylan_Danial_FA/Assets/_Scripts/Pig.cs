using UnityEngine;

public class Pig : PhysicObject
{
    public float Toughness = 10.0f; // Adjustable toughness value
    public bool IsDestroyed { get; private set; } = false;

    public void DestroyPig()
    {
        IsDestroyed = true;
        gameObject.SetActive(false); // Optionally deactivate the object
        Debug.Log("Pig destroyed due to high momentum collision!");
    }
}

