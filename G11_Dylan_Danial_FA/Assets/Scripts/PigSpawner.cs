using UnityEngine;

public class PigSpawner : MonoBehaviour
{
    public GameObject pigPrefab; 

    void Start()
    {
        
        Instantiate(pigPrefab, transform.position, Quaternion.identity);
    }
}
