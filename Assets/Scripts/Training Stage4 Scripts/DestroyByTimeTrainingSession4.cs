using UnityEngine;

public class DestroyByTimeTrainingSession4 : MonoBehaviour
{

    public float boltLifetime;
    public float MTime;
   // 
    // public float M2Time;

    void Start()

    {
        if (gameObject.tag == "boltTrainingSession4")
        {
            Destroy(gameObject, boltLifetime);
        }

        if (gameObject.tag == "M1TrainingSession4")
        {
            Destroy(gameObject, MTime);
        }
        if (gameObject.tag == "M2TrainingSession4")
        {
            Destroy(gameObject, MTime);
        }

    }
}
