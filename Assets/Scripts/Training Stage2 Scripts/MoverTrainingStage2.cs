using UnityEngine;

public class MoverTrainingStage2 : MonoBehaviour {

    public float speedLow;
    public float speedHigh;
    public float boltSpeed;

    // M1 is white hazard, with high speed;
    // M1 is black hazard, with low speed;

    private controllerPlayerTrainingStage2 controlPlayer;

    void Start () {

        int random = Random.Range(0, 100);
        GameObject controllerPlayerObject = GameObject.FindWithTag("Player");
        if (controllerPlayerObject != null)
        {
            controlPlayer = controllerPlayerObject.GetComponent<controllerPlayerTrainingStage2>();  //GetComponent is the primary way of accessing other components. 
        }
        if (controlPlayer == null)
        {
            Debug.Log("Cannot find 'controllerPlayer' script for training session 2");
        }

        if (gameObject.tag == "M2") // Black Plane
        {
            GetComponent<Rigidbody>().velocity = transform.forward * (-speedHigh);
           // controlPlayer.UpdateTotalBlackPlanes(1);
        }
        else if(gameObject.tag == "M1") // White Plane
        { 
            GetComponent<Rigidbody>().velocity = transform.forward * speedLow;
            //controlPlayer.UpdateTotalWhitePlanes(1);
        }


        if (gameObject.tag == "bolt")
        {
            GetComponent<Rigidbody>().velocity = transform.forward * boltSpeed;
        }
    }

}
