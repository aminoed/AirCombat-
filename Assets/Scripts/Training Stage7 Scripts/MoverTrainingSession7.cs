using UnityEngine;

public class MoverTrainingSession7 : MonoBehaviour {

    public float speedLow;
    public float speedHigh;
    public float boltSpeed;
    private controllerPlayerTrainingSession7 controlPlayer;

    void Start () {

        int random = Random.Range(0, 100);
        float speed = speedLow * (1.2f);

        GameObject controllerPlayerObject = GameObject.FindWithTag("Player");
        if (controllerPlayerObject != null)
        {
            controlPlayer = controllerPlayerObject.GetComponent<controllerPlayerTrainingSession7>();
        }
        if (controlPlayer == null)
        {
            Debug.Log("Cannot find 'controllerPlayer' script for training session 7");
        }

        if (random % 2 == 0)
        {
            speed = speedHigh;
        }
        if(gameObject.tag == "M2TrainingSession7")
        {
            speed = speedLow * (1.2f);
            GetComponent<Rigidbody>().velocity = transform.forward * -(speed);
           // controlPlayer.updateTotalBlackPlanes(1);
        }
        else if (gameObject.tag == "M1TrainingSession7")
        {
            GetComponent<Rigidbody>().velocity = transform.forward * speed;
            //controlPlayer.updateTotalWhitePlanes(1);
        }
        if (gameObject.tag == "BoltTrainingSession7")
        {
            GetComponent<Rigidbody>().velocity = transform.forward * boltSpeed;
        }
    }

}
