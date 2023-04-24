using UnityEngine;

public class DestroyByBoundaryTrainingStage2 : MonoBehaviour {

    void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    }

}
