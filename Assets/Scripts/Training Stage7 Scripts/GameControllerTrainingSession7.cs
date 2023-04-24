using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerTrainingSession7 : MonoBehaviour {

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
    public float panelFreezeTime;

    public Text SwitchMode;
    private string swicthMode;
    public string mode;

    private System.DateTime gameStartTime;
    private int count;
    private bool start;
    private Coroutine coroutine;
    private controllerPlayerTrainingSession7 controlPlayer;
    private controllerGaugeTrainingSession7 controllerGauge;
    private System.DateTime startPanelTime;
    private bool startPanel;
    private int direction;

    public float speed;

    public float autoShootWait;

    void Start () {
        GameObject controllerPlayerObject = GameObject.FindWithTag("Player");
        if (controllerPlayerObject != null)
        {
            controlPlayer = controllerPlayerObject.GetComponent<controllerPlayerTrainingSession7>();
        }
        if (controlPlayer == null)
        {
            Debug.Log("Cannot find 'controllerPlayer' script for training session 7");
        }

        GameObject gaugeControllerObject = GameObject.FindWithTag("GaugeController");
        if (gaugeControllerObject != null)
        {
            controllerGauge = gaugeControllerObject.GetComponent<controllerGaugeTrainingSession7>();
        }
        if (controllerGauge == null)
        {
            Debug.Log("Cannot find 'controllerGauge' script for training session 7");
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
        //startPanel = false;
        direction = 0;
 
        mode = "Manual";
        updatePlayingMode();

        swicthMode = "转至自动:左Shift + 字母o";
        updateSwitchMode();
    }

    IEnumerator Spawnwaves()
    {

        float[] birthPoint = { -14.0f + 0 * 3.5f, -14.0f + 1 * 3.5f, -14.0f + 2 * 3.5f, -14.0f + 3 * 3.5f, -14.0f + 4 * 3.5f, -14.0f + 5 * 3.5f, -14.0f + 6 * 3.5f, -14.0f + 7 * 3.5f, -14.0f + 8 * 3.5f, -14.0f + 9 * 3.5f };
        Vector3 spawnPosition1 = new Vector3(birthPoint[0], spanwLeft.y, spanwLeft.z);
        Vector3 spawnPosition2 = new Vector3(birthPoint[1], spanwLeft.y, spanwLeft.z);
        Vector3 spawnPosition3 = new Vector3(birthPoint[2], spanwLeft.y, spanwLeft.z);
        Vector3 spawnPosition4 = new Vector3(birthPoint[3], spanwLeft.y, spanwLeft.z);
        Vector3 spawnPosition5 = new Vector3(birthPoint[4], spanwLeft.y, spanwLeft.z);

        Vector3 spawnPosition6 = new Vector3(birthPoint[5], spanwLeft.y, spanwLeft.z);
        Vector3 spawnPosition7 = new Vector3(birthPoint[6], spanwLeft.y, spanwLeft.z);
        Vector3 spawnPosition8 = new Vector3(birthPoint[7], spanwLeft.y, spanwLeft.z);
        Vector3 spawnPosition9 = new Vector3(birthPoint[8], spanwLeft.y, spanwLeft.z);
        Vector3 spawnPosition10 = new Vector3(birthPoint[9], spanwLeft.y, spanwLeft.z);

        GameObject cloneHazard1 = hazards[0];
        GameObject cloneHazard2 = hazards[1];

        Quaternion spawnRotation1 = Quaternion.identity;
        Quaternion spawnRotation2 = Quaternion.Euler(0.0f, -180.0f, 0.0f);

       

        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < 10; i++)
            {
                if(i == 0)
                {
                    Instantiate(cloneHazard2, spawnPosition2, spawnRotation2);                   
                    Instantiate(cloneHazard2, spawnPosition8, spawnRotation2);
                    Instantiate(cloneHazard2, spawnPosition9, spawnRotation2);
                    controlPlayer.updateTotalBlackPlanes(3);
                    count += 3;
                   
                }
                if (i == 1)
                {
                    Instantiate(cloneHazard2, spawnPosition1, spawnRotation2);                 
                    Instantiate(cloneHazard2, spawnPosition5, spawnRotation2);                
                    Instantiate(cloneHazard2, spawnPosition8, spawnRotation2);
                    Instantiate(cloneHazard2, spawnPosition9, spawnRotation2);
                    Instantiate(cloneHazard2, spawnPosition10, spawnRotation2);
                  
                    controlPlayer.updateTotalBlackPlanes(5);
                    count += 5;

                }
                if (i == 2)
                {
                    Instantiate(cloneHazard2, spawnPosition1, spawnRotation2);
               
                    Instantiate(cloneHazard2, spawnPosition2, spawnRotation2);
                
                    Instantiate(cloneHazard2, spawnPosition3, spawnRotation2);
                  
                    Instantiate(cloneHazard2, spawnPosition4, spawnRotation2);
                  
                    Instantiate(cloneHazard2, spawnPosition5, spawnRotation2);
                    controlPlayer.updateTotalWhitePlanes(5);
                  
                    count += 5;

                }
                if (i == 3)
                {
                    Instantiate(cloneHazard2, spawnPosition1, spawnRotation2);

                    Instantiate(cloneHazard2, spawnPosition2, spawnRotation2);

                    Instantiate(cloneHazard2, spawnPosition3, spawnRotation2);

                    Instantiate(cloneHazard2, spawnPosition7, spawnRotation2);

                   
                    controlPlayer.updateTotalWhitePlanes(4);
                    count += 4;

                }
                if (i == 4)
                {
                    Instantiate(cloneHazard2, spawnPosition1, spawnRotation2);

                    Instantiate(cloneHazard2, spawnPosition3, spawnRotation2);

                    Instantiate(cloneHazard2, spawnPosition4, spawnRotation2);

                   

                    Instantiate(cloneHazard2, spawnPosition5, spawnRotation2);
                    controlPlayer.updateTotalWhitePlanes(4);
                    count += 4;

                }
                if (i == 5)
                {
                    Instantiate(cloneHazard2, spawnPosition5, spawnRotation2);

                    Instantiate(cloneHazard2, spawnPosition8, spawnRotation2);

                    Instantiate(cloneHazard2, spawnPosition9, spawnRotation2);

                    Instantiate(cloneHazard2, spawnPosition10, spawnRotation2);

                 
                    controlPlayer.updateTotalWhitePlanes(4);

                    count += 4;

                }
                if (i == 6)
                {
                    Instantiate(cloneHazard2, spawnPosition1, spawnRotation2);

                    Instantiate(cloneHazard2, spawnPosition5, spawnRotation2);

                    Instantiate(cloneHazard2, spawnPosition6, spawnRotation2);

                    Instantiate(cloneHazard2, spawnPosition7, spawnRotation2);

                    Instantiate(cloneHazard2, spawnPosition8, spawnRotation2);
                    controlPlayer.updateTotalWhitePlanes(5);
                    count += 5;

                }
                if (i == 7)
                {
                    Instantiate(cloneHazard2, spawnPosition3, spawnRotation2);

                    Instantiate(cloneHazard2, spawnPosition4, spawnRotation2);

                    Instantiate(cloneHazard2, spawnPosition5, spawnRotation2);

                    Instantiate(cloneHazard2, spawnPosition9, spawnRotation2);

                    Instantiate(cloneHazard2, spawnPosition10, spawnRotation2);
                    controlPlayer.updateTotalWhitePlanes(5);
                    count += 5;

                }
                if (i == 8)
                {
                    Instantiate(cloneHazard2, spawnPosition1, spawnRotation2);

                    Instantiate(cloneHazard2, spawnPosition6, spawnRotation2);

                    Instantiate(cloneHazard2, spawnPosition7, spawnRotation2);

                    Instantiate(cloneHazard2, spawnPosition8, spawnRotation2);

                   
                    controlPlayer.updateTotalWhitePlanes(4);
                    count += 4;

                }
                if (i == 9)
                {
                    Instantiate(cloneHazard2, spawnPosition1, spawnRotation2);

                    Instantiate(cloneHazard2, spawnPosition2, spawnRotation2);

                    Instantiate(cloneHazard2, spawnPosition3, spawnRotation2);

                    Instantiate(cloneHazard2, spawnPosition4, spawnRotation2);

                    Instantiate(cloneHazard2, spawnPosition5, spawnRotation2);
                    controlPlayer.updateTotalWhitePlanes(5);

                    count += 5;

                }
               
                //controlPlayer.updateEnemyPositions(x_value);


                updatePlanes();
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
        }
    }


    //IEnumerator Spawnwaves()
    //{

    //    float[] birthPoint = { -14.0f + 0 * 7.5f, -14.0f + 1 * 7.5f, -14.0f + 2 * 7.5f, -14.0f + 3 * 7.5f, -14.0f + 4 * 7.5f };
    //    yield return new WaitForSeconds(startWait);
    //    while (true)
    //    {
    //        for (int i = 0; i < hazardCount; i++)
    //        {
    //            GameObject hazard;
    //            if (i == 0 || i == 4)
    //            {
    //                hazard = hazards[1];
    //            }
    //            else
    //            {
    //                hazard = hazards[0];
    //            }
    //            float x_value = -14.0f + i * 7.5f;
    //            Vector3 spawnPosition = new Vector3(x_value, spanwLeft.y, spanwLeft.z);

    //            controlPlayer.updateEnemyPositions(x_value);
    //            Quaternion spawnRotation = Quaternion.identity;
    //            if (hazard.tag == "M2TrainingSession7")
    //            {
    //                spawnRotation = Quaternion.Euler(0.0f, -180.0f, 0.0f);
    //            }

    //            if (hazard.tag == "M2TrainingSession7")
    //            {
    //                controlPlayer.updateTotalBlackPlanes(1);
    //            }
    //            if (hazard.tag == "M1TrainingSession7")
    //            {
    //                controlPlayer.updateTotalWhitePlanes(1);
    //            }
    //            Instantiate(hazard, spawnPosition, spawnRotation);

    //            count += 1;
    //            updatePlanes();
    //            yield return new WaitForSeconds(spawnWait);
    //        }
    //        yield return new WaitForSeconds(waveWait);
    //    }
    //}

    // private void FixedUpdate()
    private void Update()
    {
        //if (System.DateTime.Now.Subtract(startPanelTime).Seconds >= panelFreezeTime)
        //{
        //    panel.SetActive(false);
        //}

        if (playingMode.text.Contains("手动"))
        {
            swicthMode = "转至自动:左Shift + 字母o";
            updateSwitchMode();
        }
        else
        {
            swicthMode = "转至手动:左Shift + 字母o";
            updateSwitchMode();
        }

        if (mode == "Automation")
        {
            //if (firstAutomation == -1)
            //{
            //    if((PlayerPrefs.GetInt("currentStage") == 7) || (PlayerPrefs.GetInt("currentStage") == 8)){
            //        startPanelTime = System.DateTime.Now;
            //        PlayerPrefs.SetString("switchTime", System.DateTime.Now.ToString());
            //        startPanel = true;
            //        firstAutomation = 0;
            //        StopCoroutine(coroutine);
            //    }
            //}
            //else
            //{
                if (controlPlayer.mode.intervalFirstMove == -1)
                {
                    controlPlayer.updateFirstValuesForSwitching("intervalFirstMove");
                }
                if (!start)
                {
                    Vector3 movement = new Vector3(2.0f, 0.0f, 0.0f);
                    player.GetComponent<Rigidbody>().velocity = movement * speed;
                    player.GetComponent<Rigidbody>().rotation = Quaternion.Euler(0.0f, 0.0f, player.GetComponent<Rigidbody>().velocity.x * -1);
                    coroutine = StartCoroutine(shoot());
                    start = true;
                }
                if ((int)player.GetComponent<Rigidbody>().position.x >= spawnRight.x - 0.5)
            {
                    direction = 1;
                    Vector3 movement = new Vector3(-2.0f, 0.0f, 0.0f);
                    player.GetComponent<Rigidbody>().velocity = movement * speed;
                    player.GetComponent<Rigidbody>().rotation = Quaternion.Euler(0.0f, 0.0f, player.GetComponent<Rigidbody>().velocity.x * -1);
                }

                if ((int)player.GetComponent<Rigidbody>().position.x <= spanwLeft.x + 0.5)
            {
                    direction = 0;
                    Vector3 movement = new Vector3(2.0f, 0.0f, 0.0f);
                    player.GetComponent<Rigidbody>().velocity = movement * speed;
                    player.GetComponent<Rigidbody>().rotation = Quaternion.Euler(0.0f, 0.0f, player.GetComponent<Rigidbody>().velocity.x * -1);
                }
            //}

            // if ((Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.O)) || (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.O)) || (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.O)) || (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.O))) 
            if(Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.O))
            {
                mode = "Manual";
                updatePlayingMode();
                controlPlayer.updateSwitchingTimeNMode((int)System.DateTime.Now.Subtract(gameStartTime).TotalSeconds, "M");
                controlPlayer.updateModesToPlayer();
                PlayerPrefs.SetString("switchTime", System.DateTime.Now.ToString());

                //if (controlPlayer.playingMode.text.Contains("Automation"))
                //{
                //    panel.SetActive(true);
                //}
            }
        }
        else
        {
            if (start)
            {
                start = false;
                StopCoroutine(coroutine);
            }

            // if ((Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.O)) || (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.O)) || (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.O)) || (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.O))) //  
            if(Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.O))
            {
                //firstAutomation++;
                //if (firstAutomation == 2)
                //{
                //    firstAutomation = -1;
                //}
                mode = "Automation";

                updatePlayingMode();
                controlPlayer.updateSwitchingTimeNMode((int)System.DateTime.Now.Subtract(gameStartTime).TotalSeconds, "A");
                controlPlayer.updateModesToPlayer();
                PlayerPrefs.SetString("switchTime", System.DateTime.Now.ToString());

                //startPanelTime = System.DateTime.Now;
                //if (controlPlayer.playingMode.text.Contains("Manual"))
                //{
                //    panel.SetActive(true);
                //}
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
        startPanelTime = System.DateTime.Now;
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
