using UnityEngine;

public class MoverTrainingStage3 : MonoBehaviour {

    public float speedLow;
    public float speedHigh;
    public float boltSpeed;

    private ControllerPlayerTrainingStage3 controlPlayer;
    void Start () {

        int random = Random.Range(0, 100);

        GameObject controllerPlayerObject = GameObject.FindWithTag("Player");
        if (controllerPlayerObject != null)
        {
            controlPlayer = controllerPlayerObject.GetComponent<ControllerPlayerTrainingStage3>();
        }
        if (controlPlayer == null)
        {
            Debug.Log("Cannot find 'controllerPlayer' script for training session 3");
        }

        if(gameObject.tag == "M2TrainingSession3")
        {
            GetComponent<Rigidbody>().velocity = transform.forward * (-speedHigh);
            //controlPlayer.UpdateTotalBlackPlanes(1);
        }
        else if (gameObject.tag == "M1TrainingSession3")
        {
            GetComponent<Rigidbody>().velocity = transform.forward * speedLow;
            //controlPlayer.UpdateTotalWhitePlanes(1);
        }
        if (gameObject.tag == "boltTrainingSession3")
        {
            GetComponent<Rigidbody>().velocity = transform.forward * boltSpeed;
        }


    }

}
