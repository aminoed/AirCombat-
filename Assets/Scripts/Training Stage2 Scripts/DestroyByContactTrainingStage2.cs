using UnityEngine;

public class DestroyByContactTrainingStage2 : MonoBehaviour {

    public GameObject explosion;
    public int hitCount;
    public int m1Score;
    public int m2Score;

    private int m2HitCount = 1;
    private Planes plane;

    private GameControllerTrainingStage2 gameController;

    private controllerPlayerTrainingStage2 controlPlayer;

    void Start()
    {
        plane = new Planes();

        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameControllerTrainingStage2>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script for training session 2");
        }

        GameObject controllerPlayerObject = GameObject.FindWithTag("Player");
        if (controllerPlayerObject != null)
        {
            controlPlayer = controllerPlayerObject.GetComponent<controllerPlayerTrainingStage2>();
        }
        if (controlPlayer == null)
        {
            Debug.Log("Cannot find 'controllerPlayer' script for training session 2");
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
        if (other.tag == "bolt" && gameObject.tag == "M2")
        {
            if (m2HitCount == hitCount) // black plane hit twice
            {
                Instantiate(explosion, transform.position, transform.rotation);
                Destroy(other.gameObject);
                Destroy(gameObject);
                gameController.AddScore(m2Score);
                gameController.AddHitScore(1);
                if(!controlPlayer.firstshot)
                {
                    controlPlayer.UpdateTimeOfShot();
                }
                controlPlayer.UpdateTotalScore(m2Score);
                plane.planeType = "b";
                plane.speedType = 1;
                plane.mode = "M";
                plane.countBlackPlanes = controlPlayer.session.totalBlackPlanes;
                plane.countWhitePlanes = controlPlayer.session.totalWhitePlanes;
                controlPlayer.UpdatePlanes(plane, gameObject.GetComponent<Rigidbody>().position);
            }
            else   // black plane hit one time
            {
                Instantiate(explosion, transform.position, transform.rotation); // it still explosion with one shot, but not destroyed
                Destroy(other.gameObject);
                m2HitCount++;
            }
        }
        else if (other.tag == "bolt" && gameObject.tag == "M1")   
        {
            // destroy white hazard
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
            if (speed.z.Equals(-10)){
                plane.speedType = 1;
            }else{
                plane.speedType = 2;
            }
            plane.mode = "M";
            plane.countBlackPlanes = controlPlayer.session.totalBlackPlanes;
            plane.countWhitePlanes = controlPlayer.session.totalWhitePlanes;
            controlPlayer.UpdatePlanes(plane, gameObject.GetComponent<Rigidbody>().position);
        }
        
    }

}
