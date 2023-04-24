using UnityEngine;

public class DestroyByContactTrainingStage3 : MonoBehaviour {

    public GameObject explosion;
    public int hitCount;
    public int m1Score;
    public int m2Score;

    private int m2HitCount = 1;
    private GameControllerTrainingStage3 gameController;
    private ControllerPlayerTrainingStage3 controlPlayer;
    private Planes plane;

    void Start()
    {
        plane = new Planes();
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameControllerTrainingStage3>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script in Training Stgae 3");
        }

        GameObject controllerPlayerObject = GameObject.FindWithTag("Player");
        if (controllerPlayerObject != null)
        {
            controlPlayer = controllerPlayerObject.GetComponent<ControllerPlayerTrainingStage3>();
        }
        if (controlPlayer == null)
        {
            Debug.Log("Cannot find 'controllerPlayer' script for training session 3");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary")
        {
            return;
        }

        if (other.tag == "Player")
        {
            return;
        }
        if (other.tag == "boltTrainingSession3" && gameObject.tag == "M2TrainingSession3")
        {
            if (m2HitCount == hitCount)
            {
                Instantiate(explosion, transform.position, transform.rotation);
                Destroy(other.gameObject);
                Destroy(gameObject);
                gameController.AddScore(m2Score);
                gameController.AddHitScore(1);
                if (!controlPlayer.firstshot)
                {
                    controlPlayer.UpdateTimeOfShot();
                }
                controlPlayer.UpdateTotalScore(m2Score);
                plane.planeType = "b";
                plane.speedType = 1;
                plane.mode = "A";
                plane.countBlackPlanes = controlPlayer.session.totalBlackPlanes;
                plane.countWhitePlanes = controlPlayer.session.totalWhitePlanes;
                controlPlayer.UpdatePlanes(plane, gameObject.GetComponent<Rigidbody>().position);
            }
            else
            {
                Instantiate(explosion, transform.position, transform.rotation);
                Destroy(other.gameObject);
                m2HitCount++;
            }
        }
        else if (other.tag == "boltTrainingSession3" && gameObject.tag == "M1TrainingSession3")
        {
            Instantiate(explosion, transform.position, transform.rotation);

            Destroy(other.gameObject);
            Destroy(gameObject);
            gameController.AddScore(m1Score);
            gameController.AddHitScore(1);
            if (!controlPlayer.firstshot)
            {
                controlPlayer.UpdateTimeOfShot();
            }
            controlPlayer.UpdateTotalScore(m1Score);
            Vector3 speed = gameObject.GetComponent<Rigidbody>().velocity;
            plane.planeType = "w";
            if (speed.z.Equals(-10))
            {
                plane.speedType = 1;
            }
            else
            {
                plane.speedType = 2;
            }
            plane.mode = "A";
            plane.countBlackPlanes = controlPlayer.session.totalBlackPlanes;
            plane.countWhitePlanes = controlPlayer.session.totalWhitePlanes;
            controlPlayer.UpdatePlanes(plane, gameObject.GetComponent<Rigidbody>().position);
        }
        
    }

}
