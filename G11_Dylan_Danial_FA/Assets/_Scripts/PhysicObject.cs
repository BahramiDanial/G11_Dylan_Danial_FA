using UnityEngine;

public class PhysicObject : MonoBehaviour
{
    public G11FhysicShape shape = null;
    public float drag = 0.1f;
    public float mass = 1.0f;
    public float gravityScale = 1;
    public bool isStatic;
    public Vector3 velocity = Vector3.zero;


    void Start()
    {
        shape = GetComponent<G11FhysicShape>();
        PhysicEngine.Instance.Objekts.Add(this);
    }

}
