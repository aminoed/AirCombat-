using UnityEngine;

public class DestroyByTimeTrainingStage3 : MonoBehaviour {

    public float lifetime;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
