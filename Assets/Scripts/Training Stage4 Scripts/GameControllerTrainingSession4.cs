using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerTrainingSession4 : MonoBehaviour
{

    public GameObject[] hazards;
    public Vector3 spanwLeft;
    public Vector3 spawnRight;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
   // public Transform m2;
    public GameObject shot;
    public Transform shotSpawn;
    public GameObject player;
    public Text playingMode;
    public Text planesScore;
    public Text totalPlanes;
    public Text planesHit;
    public int planeHitCount;
    public int score;
    public string mode;

    public float playerMoveSpeed;

    private System.DateTime gameStartTime;

    private int count;
    private bool start;
    private Coroutine coroutine;
    private controllerPlayerTrainingSession4 controlPlayer;
    private int direction;

    private System.DateTime AAstartTime;
    //public float intervalTime;
    private System.DateTime startPanelTime;
    public GameObject panel;
    public float panelFreezeTime;

    public int session4intervalTime;

    public float autoShootWait;


    void Start()
    {
        GameObject controllerPlayerObject = GameObject.FindWithTag("Player");
        if (controllerPlayerObject != null)
        {
            controlPlayer = controllerPlayerObject.GetComponent<controllerPlayerTrainingSession4>();
        }
        if (controlPlayer == null)
        {
            Debug.Log("Cannot find 'controllerPlayer' script for training session 4");
        }

        planeHitCount = 0;
        score = 0;
        count = 0;
        updateScore();
        updateHitCount();
        updatePlanes();
        StartCoroutine(Spawnwaves());
        start = false;
        direction = 0;

        AAstartTime = System.DateTime.Now;  
        mode = "Manual";  //    Automation      Manual
        updatePlayingMode();

        gameStartTime = System.DateTime.Now;

        panel.SetActive(false);
        startPanelTime = System.DateTime.Now;

    }

    IEnumerator Spawnwaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                float x_value = Random.Range(spanwLeft.x, spawnRight.x);
                Vector3 spawnPosition = new Vector3(x_value, spanwLeft.y, spanwLeft.z);
                controlPlayer.updateEnemyPositions(x_value);
                Quaternion spawnRotation = Quaternion.identity;

                if (hazard.tag == "M2TrainingSession4")
                {
                    spawnRotation = Quaternion.Euler(0.0f, -180.0f, 0.0f);
                }
                Instantiate(hazard, spawnPosition, spawnRotation);

                count += 1;
                updatePlanes();
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

        }
    }

    private void FixedUpdate()
    {
        if ((int)(System.DateTime.Now.Subtract(startPanelTime).TotalSeconds) >= panelFreezeTime)
        {
            panel.SetActive(false);
        }


        if ((int)(System.DateTime.Now.Subtract(AAstartTime).Seconds) >= session4intervalTime)
        {
            AAstartTime = System.DateTime.Now;
            if (mode.Equals("Manual"))
            {
                mode = "Automation";
                updatePlayingMode();
                controlPlayer.updateModesToPlayer();
                controlPlayer.updateSwitchingTimeNMode((int)System.DateTime.Now.Subtract(gameStartTime).TotalSeconds, "A");
                PlayerPrefs.SetString("switchTime", System.DateTime.Now.ToString());
                panel.SetActive(true);
                startPanelTime = System.DateTime.Now;
            }
            else
            {
                mode = "Manual";
                updatePlayingMode();
                controlPlayer.updateModesToPlayer();
                controlPlayer.updateSwitchingTimeNMode((int)System.DateTime.Now.Subtract(gameStartTime).TotalSeconds, "M");
                PlayerPrefs.SetString("switchTime", System.DateTime.Now.ToString());
                panel.SetActive(true);
                startPanelTime = System.DateTime.Now;
            }
        }

        if (playingMode.text.Contains("Automation"))
        {
            if (controlPlayer.mode.intervalFirstMove == -1)
            {
                controlPlayer.updateFirstValuesForSwitching("intervalFirstMove");
            }

            if (!start)
            {
               // player.GetComponent<Rigidbody>().position = new Vector3
               //(
               //  Mathf.Clamp(player.GetComponent<Rigidbody>().position.x, -8.0f, 8.0f),
               //  0.0f,
               //  Mathf.Clamp(player.GetComponent<Rigidbody>().position.z, -1, 8)
               //);

                Vector3 movement = new Vector3(2.0f, 0.0f, 0.0f);
                player.GetComponent<Rigidbody>().velocity = movement * playerMoveSpeed;

                player.GetComponent<Rigidbody>().rotation = Quaternion.Euler(0.0f, 0.0f, player.GetComponent<Rigidbody>().velocity.x * -1);
                coroutine = StartCoroutine(shoot());
                start = true;
            }
            if ((int)player.GetComponent<Rigidbody>().position.x >= spawnRight.x - 0.5)
            {
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
        else
        {
            if (start)
            {
                start = false;
                StopCoroutine(coroutine);
            }
        }
    }

    IEnumerator shoot()
    {
        while (true)
        {
            if (controlPlayer.mode.intervalFirstPress == -1)
            {
                controlPlayer.updateFirstValuesForSwitching("intervalFirstPress");
            }
            Instantiate(shot, shotSpawn.transform.position, shotSpawn.rotation);
            yield return new WaitForSeconds(autoShootWait);
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        updateScore();
    }

    void updateScore()
    {
        planesScore.text = "团队得分: " + score;
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

    void updatePlayingMode()
    {
        playingMode.text = "当前模式:" + mode;
    }
}
