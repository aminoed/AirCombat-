using UnityEngine;

public class DestroyByBoundaryTrainingSession7 : MonoBehaviour {

    void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    }

}
