using UnityEngine;

public class MoverTrainingSession4 : MonoBehaviour {

    public float speedLow;
    public float speedHigh;
    public float boltSpeed;
    private controllerPlayerTrainingSession4 controlPlayer;

    void Start () {

        int random = Random.Range(0, 100);

        GameObject controllerPlayerObject = GameObject.FindWithTag("Player");
        if (controllerPlayerObject != null)
        {
            controlPlayer = controllerPlayerObject.GetComponent<controllerPlayerTrainingSession4>();
        }
        if (controlPlayer == null)
        {
            Debug.Log("Cannot find 'controllerPlayer' script for training session 4");
        }

        if(gameObject.tag == "M2TrainingSession4")
        {
            GetComponent<Rigidbody>().velocity = transform.forward * -(speedHigh);
            controlPlayer.updateTotalBlackPlanes(1);
        }
        else if (gameObject.tag == "M1TrainingSession4")
        {
            GetComponent<Rigidbody>().velocity = transform.forward * speedLow;
            controlPlayer.updateTotalWhitePlanes(1);
        }
        if (gameObject.tag == "boltTrainingSession4")
        {
            GetComponent<Rigidbody>().velocity = transform.forward * boltSpeed;
        }
    }

}
