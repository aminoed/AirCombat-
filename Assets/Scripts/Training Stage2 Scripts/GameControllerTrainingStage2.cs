using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerTrainingStage2 : MonoBehaviour {

    public GameObject[] hazards;
    public Vector3 spanwLeft;
    public Vector3 spawnRight;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public Transform m2;
    public Text planesScore;
    public Text totalPlanes;
    public Text planesHit;
    public int planeHitCount;
    public int score;

    private int count;
    private controllerPlayerTrainingStage2 controlPlayer;

    void Start () {

        GameObject controllerPlayerObject = GameObject.FindWithTag("Player");
        if (controllerPlayerObject != null)
        {
            controlPlayer = controllerPlayerObject.GetComponent<controllerPlayerTrainingStage2>();
        }
        if (controlPlayer == null)
        {
            Debug.Log("Cannot find 'controlPlayer' script for training session 2");
        }

        planeHitCount = 0;
        score = 0;
        count = 0;
        updateScore();
        updateHitCount();
        updatePlanes();
        StartCoroutine(Spawnwaves());
    }

   
    IEnumerator Spawnwaves()
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

                float x_position = -14.0f + i * 7.5f;
                controlPlayer.UpdateEnemyPositions(x_position);
                Vector3 spawnPosition = new Vector3(x_position, spanwLeft.y, spanwLeft.z);

                Quaternion spawnRotation = Quaternion.identity;
                if (hazard.tag == "M2")
                {
                    spawnRotation = Quaternion.Euler(0.0f, -180.0f, 0.0f);
                }

                if (hazard.tag == "M2")
                {
                    controlPlayer.UpdateTotalBlackPlanes(1);
                }
                if (hazard.tag == "M1")
                {
                    controlPlayer.UpdateTotalWhitePlanes(1);
                }

                Instantiate(hazard, spawnPosition, spawnRotation);
                count += 1;
                updatePlanes();
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        updateScore();
    }

    void updateScore()
    {
        planesScore.text = "手动得分: " + score;
    }

    void updatePlanes()
    {
        totalPlanes.text = "Total planes : " + count;
    }

    public void AddHitScore(int newHitCount)
    {
        planeHitCount += newHitCount;
        updateHitCount();
    }

    void updateHitCount()
    {
        planesHit.text = "Planes Hit : " + planeHitCount;
    }
}
