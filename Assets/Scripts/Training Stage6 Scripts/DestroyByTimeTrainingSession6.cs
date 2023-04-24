using UnityEngine;

public class DestroyByTimeTrainingSession6 : MonoBehaviour {

    public float lifetime;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
