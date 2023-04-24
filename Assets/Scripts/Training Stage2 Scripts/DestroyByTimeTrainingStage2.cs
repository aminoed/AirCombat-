using UnityEngine;

public class DestroyByTimeTrainingStage2 : MonoBehaviour {

    public float lifetime;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
