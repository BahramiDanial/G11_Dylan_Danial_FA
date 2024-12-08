using UnityEngine;

public class Pig : PhysicObject
{
    public float Toughness = 1.0f; // Extremely low toughness for testing
    public bool IsDestroyed { get; private set; } = false;

    public void DestroyPig()
    {
        IsDestroyed = true;
        Debug.Log($"Pig {name} destroyed!");
        Destroy(gameObject);
    }
}
