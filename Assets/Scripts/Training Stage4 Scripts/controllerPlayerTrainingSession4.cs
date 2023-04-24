using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;
using System;

public class controllerPlayerTrainingSession4 : MonoBehaviour {

    public float speed;
    public Boundary boundary;
    public float tilt;
    public Text playingMode;
    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    public bool firstshot;
    public Modes mode;
    public Sessions session;

    private float nextFire;
    private System.DateTime startTimew;
    private Player player;
    private List<int> deviations;
    public int session4TimeLimitation;



    private void Start()
    {
        deviations = new List<int>();
        startTimew = System.DateTime.Now;
        session = new Sessions();
        session.session = "TRAINING";
        session.stage = PlayerPrefs.GetInt("currentStage");
        session.mode = "AA";
        session.totalScore = 0;
        session.totalBlackPlanes = 0;
        session.totalWhitePlanes = 0;
        session.timeOfShot = -1;
        mode = new Modes();
        updateSwitchingTimeNMode(0, "M");
        firstshot = false;
        string json = PlayerPrefs.GetString("json");
        player = new Player();
        //player = JsonUtility.FromJson<Player>(json);

        if (json != "")
        {
            player = JsonUtility.FromJson<Player>(json);
        }


        PlayerPrefs.SetInt("computerShootingScore", 0);
        PlayerPrefs.SetInt("yourShootingScore", 0);
        PlayerPrefs.SetString("switchTime", System.DateTime.Now.ToString());
    }

    void Update()
    {
        Debug.Log("Time has passed: " + System.DateTime.Now.Subtract(startTimew).Seconds + " seconds");
        if (Time.timeSinceLevelLoad >= session4TimeLimitation)
        {
            session.modes.Add(mode);
            player.sessions.Add(session);
            string json = JsonUtility.ToJson(player);
            PlayerPrefs.SetString("json", json);
            SceneManager.LoadScene("TrainStage5");
        }


        if (Input.GetKeyDown("space") && Time.time > nextFire && playingMode.text.Contains("Manual"))
        {
            //print("in mode manual space key");
            if(mode.intervalFirstPress == -1){
                updateFirstValuesForSwitching("intervalFirstPress");
            }
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            GetComponent<AudioSource>().Play();
        }
    }

    private void FixedUpdate()
    {
        if (playingMode.text.Contains("Manual"))
        {
            //print("in mode manual x axis input");
            float moveHorizontal = Input.GetAxis("Horizontal");
            //float moveVertical = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);
            if (mode.intervalFirstMove == -1)
            {
                updateFirstValuesForSwitching("intervalFirstMove");
            }
            GetComponent<Rigidbody>().velocity = movement * speed;
        }

        GetComponent<Rigidbody>().position = new Vector3
        (
            Mathf.Clamp(GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
        );

        GetComponent<Rigidbody>().rotation = Quaternion.Euler(0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
    }

    public void updateTimeOfShot()
    {
        if (!firstshot)
        {
            session.timeOfShot = System.DateTime.Now.Subtract(startTimew).Seconds;
            firstshot = true;
        }
    }

    public void updateTotalScore(int score)
    {
        session.totalScore += score;
    }

    public void updateTotalBlackPlanes(int count)
    {
        session.totalBlackPlanes += count;
    }

    public void updateTotalWhitePlanes(int count)
    {
        session.totalWhitePlanes += count;
    }

    public void updatePlanes(Planes newPlane, Vector3 enemyPosition)
    {
        newPlane.verticalDistance = enemyPosition.z - GetComponent<Rigidbody>().position.z;
        newPlane.time = System.DateTime.Now.Subtract(startTimew).Seconds;
        session.planes.Add(newPlane);
    }

    public void updateDeviations(Deviations deviation)
    {
        session.deviations.Add(deviation);
    }
    public void updateSwitchingTimeNMode(int switchTime, string followMode){
        mode.switchingTime = switchTime;
        mode.followingMode = followMode;
    }

    public void updateFirstValuesForSwitching(string parameter){
        if(parameter.Equals("intervalFirstShot")){
            mode.intervalFirstShot = System.DateTime.Now.Subtract(System.DateTime.Parse(PlayerPrefs.GetString("switchTime"))).Seconds;
        }else if (parameter.Equals("intervalFirstPress")){
            mode.intervalFirstPress = System.DateTime.Now.Subtract(System.DateTime.Parse(PlayerPrefs.GetString("switchTime"))).Seconds;
        }else{
            mode.intervalFirstMove = System.DateTime.Now.Subtract(System.DateTime.Parse(PlayerPrefs.GetString("switchTime"))).Seconds;
        }
    }

    public void updateModesToPlayer(){
        session.modes.Add(mode);
        mode = new Modes();
    }

    public void updateTotalDeviations(){
        deviations.Add(0);    // add one elements
    }

    public void updateCorrectDeviation(){
        deviations[deviations.Count() - 1] = 1;    // deviations.Count() - how many elements in list
    }

    public void updateEnemyPositions(float position)
    {
        player.enemyPositions.Add(position);
    }
}
