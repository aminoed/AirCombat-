using UnityEngine;

public class DestroyByBoundaryTrainingSession6 : MonoBehaviour {

    void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    }

}
