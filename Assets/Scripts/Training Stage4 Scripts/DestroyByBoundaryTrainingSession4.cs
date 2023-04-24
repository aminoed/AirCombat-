using UnityEngine;

public class DestroyByBoundaryTrainingSession4 : MonoBehaviour {

    void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    }

}
