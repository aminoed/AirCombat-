using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerTrainingStage3 : MonoBehaviour
{

    public GameObject[] hazards;
    public Vector3 spanwLeft;
    public Vector3 spawnRight;
    public int hazardCount;  // each wave, there are five hazards
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public Transform m2;
    public Text planesScore;
    public Text totalPlanes;
    public Text planesHit;
    public int planeHitCount;
    public int score;
    public GameObject shot;
    public Transform shotSpawn;
    public GameObject player;
    public float playerMoveSpeed;
    public float autoShootWait;

    private int count;
    private ControllerPlayerTrainingStage3 controlPlayer;
    private int direction;

    void Start()
    {
        GameObject controllerPlayerObject = GameObject.FindWithTag("Player");
        if (controllerPlayerObject != null)
        {
            controlPlayer = controllerPlayerObject.GetComponent<ControllerPlayerTrainingStage3>();
        }
        if (controlPlayer == null)
        {
            Debug.Log("Cannot find 'controlPlayer' script for training session 3");
        }

        direction = 0;
        planeHitCount = 0;
        score = 0;
        count = 0;
        UpdateScore();
        UpdateHitCount();
        UpdatePlanes();
        StartCoroutine(Spawnwaves());  // to generate a loop of waves of hazards
        Vector3 movement = new Vector3(2.0f, 0.0f, 0.0f);
        player.GetComponent<Rigidbody>().velocity = movement * playerMoveSpeed;
        //player.GetComponent<Rigidbody>().AddRelativeForce(movement * 50, ForceMode.Acceleration);
        player.GetComponent<Rigidbody>().rotation = Quaternion.Euler(0.0f, 0.0f, player.GetComponent<Rigidbody>().velocity.x * -1);
        StartCoroutine(Shoot());
    }

    IEnumerator Spawnwaves()  // spawn waves of hazards
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard;
                if (i == 0 || i == 4)
                {
                    hazard = hazards[1];
                }
                else
                {
                    hazard = hazards[0];
                }

                float x_value = -14.0f + i * 7.5f;

                Vector3 spawnPosition = new Vector3(x_value, spanwLeft.y, spanwLeft.z);
                controlPlayer.UpdateEnemyPositions(x_value);
                Quaternion spawnRotation = Quaternion.identity; //Quaternion.identity - This quaternion corresponds to "no rotation" - the object is perfectly aligned with the world or parent axes.
                if (hazard.tag == "M2TrainingSession3")
                {
                    spawnRotation = Quaternion.Euler(0.0f, -180.0f, 0.0f);
                }

                if (hazard.tag == "M2TrainingSession3")
                {
                    controlPlayer.UpdateTotalBlackPlanes(1);
                }
                if (hazard.tag == "M1TrainingSession3")
                {
                    controlPlayer.UpdateTotalWhitePlanes(1);
                }

                Instantiate(hazard, spawnPosition, spawnRotation);

                count += 1;
                UpdatePlanes();
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

        }
    }

    IEnumerator Shoot(){    // this section implements the automation mode - player's plane automatically shoot
        while (true)
        {
            Instantiate(shot, shotSpawn.transform.position, shotSpawn.rotation);
            yield return new WaitForSeconds(autoShootWait);
        }
    }

    //maintaing player boundaries
    private void FixedUpdate()     // this section implements the automation mode - player's plane automatically move
    {
        //player.GetComponent<Rigidbody>().position = new Vector3
        //(
        //    Mathf.Clamp(player.GetComponent<Rigidbody>().position.x, -8.0f, 8.0f),  
        //    0.0f,
        //    Mathf.Clamp(player.GetComponent<Rigidbody>().position.z, -1.0f, 8.0f)
        //);

        if((int)player.GetComponent<Rigidbody>().position.x >= spawnRight.x - 0.5)
        {   // direction controls direction
            direction = 1;
            Vector3 movement = new Vector3(-2.0f, 0.0f, 0.0f);
            player.GetComponent<Rigidbody>().velocity = movement * playerMoveSpeed;
            player.GetComponent<Rigidbody>().rotation = Quaternion.Euler(0.0f, 0.0f, player.GetComponent<Rigidbody>().velocity.x * -1);
        }

        if ((int)player.GetComponent<Rigidbody>().position.x <= spanwLeft.x + 0.5)
        {
            direction = 0;
            Vector3 movement = new Vector3(2.0f, 0.0f, 0.0f);
            player.GetComponent<Rigidbody>().velocity = movement * playerMoveSpeed;
            player.GetComponent<Rigidbody>().rotation = Quaternion.Euler(0.0f, 0.0f, player.GetComponent<Rigidbody>().velocity.x * -1);
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        planesScore.text = "电脑得分: " + score;
    }

    void UpdatePlanes()
    {
        totalPlanes.text = "Total planes : " + count;
    }

    public void AddHitScore(int newHitCount)
    {
        planeHitCount += newHitCount;
        UpdateHitCount();
    }

    void UpdateHitCount()
    {
        planesHit.text = "Planes Hit : " + planeHitCount;
    }
}
