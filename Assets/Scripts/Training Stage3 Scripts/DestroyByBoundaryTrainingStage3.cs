using UnityEngine;

public class DestroyByBoundaryTrainingStage3 : MonoBehaviour {

    void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    }

}
