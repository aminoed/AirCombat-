using UnityEngine;

public class DestroyByTimeTrainingSession7 : MonoBehaviour {

    public float lifetime;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
