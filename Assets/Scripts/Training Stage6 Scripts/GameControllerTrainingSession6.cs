using System;
// using System.ComponentModel.DataAnnotations.Schema;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameControllerTrainingSession6 : MonoBehaviour
{

    public float playerMoveSpeed; // palye's speed in automation mode
    public GameObject[] hazards;
    public Vector3 spanwLeft;
    public Vector3 spawnRight;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public Transform m2;
    public GameObject shot;
    public Transform shotSpawn;
    public GameObject player;
    public Text playingMode;
    public Text planesScore;
    public Text totalPlanes;
    public Text planesHit;
    public int planeHitCount;
    public int score;
    public int firstAutomation;
    //public float panelFreezeTime;

    public Text SwitchMode;
    private string swicthMode;


    public string mode;

    private System.DateTime gameStartTime;
    private int count;
    private bool start;
    private Coroutine coroutine;
    private controllerPlayerTrainingSession6 controlPlayer;
    private controllerGaugeTrainingSession6 controllerGauge;
    // private System.DateTime startPanelTime;
    //private bool startPanel;
    private int direction;

    public float autoShootWait;

    void Start()
    {
        GameObject controllerPlayerObject = GameObject.FindWithTag("Player");
        if (controllerPlayerObject != null)
        {
            controlPlayer = controllerPlayerObject.GetComponent<controllerPlayerTrainingSession6>();
        }
        if (controlPlayer == null)
        {
            Debug.Log("Cannot find 'controllerPlayer' script for training session 6");
        }

        GameObject gaugeControllerObject = GameObject.FindWithTag("GaugeController");
        if (gaugeControllerObject != null)
        {
            controllerGauge = gaugeControllerObject.GetComponent<controllerGaugeTrainingSession6>();
        }
        if (controllerGauge == null)
        {
            Debug.Log("Cannot find 'controllerGauge' script for training session 6");
        }

        swicthMode = "";
        planeHitCount = 0;
        score = 0;
        count = 0;
        updateScore();
        updateHitCount();
        updatePlanes();
        StartCoroutine(Spawnwaves());
        start = false;
        firstAutomation = 0;
        // startPanel = false;
        direction = 0;

        mode = "Manual"; // Automation    Manual
        updatePlayingMode();

        swicthMode = "转至自动:左Shift + 字母o";
        updateSwitchMode();
    }

    IEnumerator Spawnwaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            // float lastSpawnX = 0;
            // float minDistance = 0;
            //set the generated number randomly;
            int genNum = Random.Range(1, hazardCount);
            // Debug.Log("============lz==== genNum: "+genNum);
            //一共设置三组，每组最少一个飞机都没有，最多放入所有飞机
            int a1 = Random.Range(0, genNum);
            int a2 = Random.Range(0, genNum - a1);
            int a3 = genNum - a1 - a2;
            Debug.Log("============lz==== a1: "+a1+", a2: "+a2+", a3: "+a3);

            float[] positions = new float[5];
            float x_value = Random.Range(-13.0f, 13.0f);
            int flag = 1;
            // for (int i = 0; i < genNum; i++)
            // {
            //     if (i == 0)
            //     {
            //         positions[0] = x_value;
            //         if (x_value >= 0)
            //         {
            //             flag = -1;
            //         }
            //     }
            //     else
            //     {
            //         positions[i] = positions[i - 1] + flag * Random.Range(1.5f, 3.0f);
            //         if (i == (a1 + 1) || i == (a2 + 1))
            //         {
            //             positions[i] = positions[i - 1] + flag*Random.Range(8.0f, 10.0f);
            //         }
            //     }
            //     Debug.Log("============lz==== position["+i+"] : "+positions[i]);
            // }
            for (int i = 0; i < genNum; i++)
            {
                GameObject hazard;
                Vector3 spawnPosition;
                //generate hazard's x_value randomly,from -13.0f to 13.0f
                // float x_value = Random.Range(-13.0f, 13.0f);

                //generate the hazard type randomly;
                hazard = hazards[Random.Range(0, hazards.Length)];
                float hazardScale = Random.Range(0, 1.0f);

                if (i == 0)
                {
                    positions[0] = x_value;
                    if (x_value >= 0)
                    {
                        flag = -1;
                    }
                }
                else
                {
                    positions[i] = positions[i - 1] + flag * Random.Range(1.5f, 3.0f);
                    if (i == (a1 + 1) || i == (a2 + 1))
                    {
                        positions[i] = positions[i - 1] + flag*Random.Range(8.0f, 10.0f);
                    }
                }
                Debug.Log("============lz==== position["+i+"] : "+positions[i]);

                spawnPosition = new Vector3(positions[i], spanwLeft.y, spanwLeft.z);

                controlPlayer.updateEnemyPositions(x_value);
                Quaternion spawnRotation = Quaternion.identity;

                if (hazard.tag == "M2TrainingSession6")
                {
                    spawnRotation = Quaternion.Euler(0.0f, -180.0f, 0.0f);
                }

                if (hazard.tag == "M2TrainingSession6")
                {
                    hazard.transform.localScale = Vector3.one * (hazardScale >= 0.7f ? 0.2f : 0.15f);
                    controlPlayer.updateTotalBlackPlanes(1);
                }
                if (hazard.tag == "M1TrainingSession6")
                {
                    hazard.transform.localScale = Vector3.one * (hazardScale >= 0.7f ? 0.5f : 0.4f);
                    controlPlayer.updateTotalWhitePlanes(1);
                }
                // Debug.Log("============lz==== originalScale: " + hazard.transform.localScale);
                Instantiate(hazard, spawnPosition, spawnRotation);
                // Debug.Log("============lz==== type: "+hazard.tag +" generate position: " + spawnPosition.x);
                count += 1;
                updatePlanes();
                // yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
        }
    }

    //fixedUpdate's frame is static, used to update physic movement; update's frame is used to update game's logical change.
    // private void FixedUpdate()
    private void Update()
    {
        if (playingMode.text.Contains("手动"))
        {
            swicthMode = "转至自动:左Shift + 字母o";
            updateSwitchMode();
        }
        if (playingMode.text.Contains("自动"))
        {
            swicthMode = "转至手动:左Shift + 字母o";
            updateSwitchMode();
        }


        if (playingMode.text.Contains("自动"))
        {
            if (controlPlayer.mode.intervalFirstMove == -1)
            {
                controlPlayer.updateFirstValuesForSwitching("intervalFirstMove");
            }
            if (!start)
            {
                Vector3 movement = new Vector3(2.0f, 0.0f, 0.0f);
                player.GetComponent<Rigidbody>().velocity = movement * playerMoveSpeed;
                player.GetComponent<Rigidbody>().rotation = Quaternion.Euler(0.0f, 0.0f, player.GetComponent<Rigidbody>().velocity.x * -1);
                coroutine = StartCoroutine(shoot());
                start = true;
            }
            if ((int)player.GetComponent<Rigidbody>().position.x >= spawnRight.x - 0.5)
            {
                direction = 1;
                Vector3 movement1 = new Vector3(-2.0f, 0.0f, 0.0f);
                player.GetComponent<Rigidbody>().velocity = movement1 * playerMoveSpeed;
                player.GetComponent<Rigidbody>().rotation = Quaternion.Euler(0.0f, 0.0f, player.GetComponent<Rigidbody>().velocity.x * -1);
            }

            if ((int)player.GetComponent<Rigidbody>().position.x <= spanwLeft.x + 0.5)
            {
                direction = 0;
                Vector3 movement2 = new Vector3(2.0f, 0.0f, 0.0f);
                player.GetComponent<Rigidbody>().velocity = movement2 * playerMoveSpeed;
                player.GetComponent<Rigidbody>().rotation = Quaternion.Euler(0.0f, 0.0f, player.GetComponent<Rigidbody>().velocity.x * -1);
            }

            // if ((Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.O))     ==> continue to press, no need;
            // || (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.O))  ==>
            // || (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.O)) 
            // || (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.O))) 
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.O))
            {
                mode = "Manual";
                updatePlayingMode();
                controlPlayer.updateModesToPlayer();
                controlPlayer.updateSwitchingTimeNMode((int)System.DateTime.Now.Subtract(gameStartTime).TotalSeconds, "M");
                PlayerPrefs.SetString("switchTime", System.DateTime.Now.ToString());

            }
        }

        else
        {
            if (start)
            {
                start = false;
                StopCoroutine(coroutine);
            }
            // if ((Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.O))    
            // || (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.O)) 
            // || (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.O)) 
            // || (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.O))) 
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.O))
            {
                mode = "Automation";
                updatePlayingMode();
                controlPlayer.updateModesToPlayer();
                controlPlayer.updateSwitchingTimeNMode((int)System.DateTime.Now.Subtract(gameStartTime).TotalSeconds, "A");
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

    public void changeToManual()
    {
        mode = "Manual";
        updatePlayingMode();
        controlPlayer.updateModesToPlayer();
        controlPlayer.updateSwitchingTimeNMode((int)System.DateTime.Now.Subtract(gameStartTime).TotalSeconds, "M");
        //startPanelTime = System.DateTime.Now;
    }

    void updatePlayingMode()
    {
        if (mode == "Manual")
        { playingMode.text = "当前模式:手动"; }

        if (mode == "Automation")
        { playingMode.text = "当前模式:自动"; }
    }

    void updateSwitchMode()
    {
        SwitchMode.text = swicthMode;
    }

}
