﻿using UnityEngine;

public class DestroyByContactTrainingSession7 : MonoBehaviour {

    public GameObject explosion;
    public int hitCount;
    public int m1Score;
    public int m2Score;

    private int m2HitCount = 1;
    private GameControllerTrainingSession7 gameController;
    private controllerPlayerTrainingSession7 controlPlayer;
    private Planes plane;

    void Start()
    {
        plane = new Planes();
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameControllerTrainingSession7>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script in Training Session 7");
        }

        GameObject controllerPlayerObject = GameObject.FindWithTag("Player");
        if (controllerPlayerObject != null)
        {
            controlPlayer = controllerPlayerObject.GetComponent<controllerPlayerTrainingSession7>();
        }
        if (controlPlayer == null)
        {
            Debug.Log("Cannot find 'controllerPlayer' script for training session 7");
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
        if (other.tag == "BoltTrainingSession7" && gameObject.tag == "M2TrainingSession7")
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
                    controlPlayer.updateTimeOfShot();
                }
                if(controlPlayer.mode.intervalFirstShot == -1){
                    controlPlayer.updateFirstValuesForSwitching("intervalFirstShot");
                }
                controlPlayer.updateTotalScore(m2Score);
                plane.planeType = "b";
                plane.speedType = 1;
                if(controlPlayer.playingMode.text.Contains("Manual")){
                    plane.mode = "M";
                    int score = PlayerPrefs.GetInt("yourShootingScore");
                    score += m2Score;
                    PlayerPrefs.SetInt("yourShootingScore", score);
                }else{
                    plane.mode = "A";
                    int score = PlayerPrefs.GetInt("computerShootingScore");
                    score += m2Score;
                    PlayerPrefs.SetInt("computerShootingScore", score);
                }
                plane.countBlackPlanes = controlPlayer.session.totalBlackPlanes;
                plane.countWhitePlanes = controlPlayer.session.totalWhitePlanes;
                controlPlayer.updatePlanes(plane, gameObject.GetComponent<Rigidbody>().position);
            }
            else
            {
                Instantiate(explosion, transform.position, transform.rotation);
                Destroy(other.gameObject);
                m2HitCount++;
            }
        }
        else if (other.tag == "BoltTrainingSession7" && gameObject.tag == "M1TrainingSession7")
        {
            Instantiate(explosion, transform.position, transform.rotation);

            Destroy(other.gameObject);
            Destroy(gameObject);
            gameController.AddScore(m1Score);
            gameController.AddHitScore(1);
            if (!controlPlayer.firstshot)
            {
                controlPlayer.updateTimeOfShot();
            }
            if (controlPlayer.mode.intervalFirstShot == -1)
            {
                controlPlayer.updateFirstValuesForSwitching("intervalFirstShot");
            }

            controlPlayer.updateTotalScore(m1Score);
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
            if (controlPlayer.playingMode.text.Contains("Manual"))
            {
                plane.mode = "M";
                int score = PlayerPrefs.GetInt("yourShootingScore");
                score += m1Score;
                PlayerPrefs.SetInt("yourShootingScore", score);
            }
            else
            {
                plane.mode = "A";
                int score = PlayerPrefs.GetInt("computerShootingScore");
                score += m1Score;
                PlayerPrefs.SetInt("computerShootingScore", score);
            }
            plane.countBlackPlanes = controlPlayer.session.totalBlackPlanes;
            plane.countWhitePlanes = controlPlayer.session.totalWhitePlanes;
            controlPlayer.updatePlanes(plane, gameObject.GetComponent<Rigidbody>().position);
        }
        int totalPlaneScore = controlPlayer.session.totalBlackPlanes * m2Score + controlPlayer.session.totalWhitePlanes * m1Score;
        float t = (1.0f * controlPlayer.session.totalScore) / (1.0f * totalPlaneScore);
        PlayerPrefs.SetFloat("TeamShootingPerformance", t);
        controlPlayer.player.teamShootingPerformance = t;
    }

}
