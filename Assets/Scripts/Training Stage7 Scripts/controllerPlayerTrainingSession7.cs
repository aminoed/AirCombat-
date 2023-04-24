using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class controllerPlayerTrainingSession7 : MonoBehaviour {

    public float speed; // player's speed in manual mode
    public Boundary boundary;
    public float tilt;
    public Text playingMode;
    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    public bool firstshot;
    public Modes mode;
    public Sessions session;
    public int stageTimeLimit;

    private float nextFire;
    private System.DateTime startTime;
    public Player player;
    private float ff;

    int bulletCount = 9;
    float bulletTime = 0;

    public Transform bullets;

    private void Start()
    {
        startTime = System.DateTime.Now;
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

        if (json != "")
        {
            player = JsonUtility.FromJson<Player>(json);
        }

        PlayerPrefs.SetInt("computerShootingScore", 0);
        PlayerPrefs.SetInt("yourShootingScore", 0);
        PlayerPrefs.SetString("switchTime", System.DateTime.Now.ToString());
        PlayerPrefs.SetFloat("TeamShootingPerformance", 0.0f);
    }

    void Update()
    {     
        ff = (float)(System.DateTime.Now.Subtract(startTime).TotalSeconds);
        if (ff>= stageTimeLimit)
        {
            session.modes.Add(mode);
            player.sessions.Add(session);

            string json = JsonUtility.ToJson(player);
            PlayerPrefs.SetString("json", json);
            // SaveItemInfo(json);
            SceneManager.LoadScene("EndGame7");
        }

        if (Input.GetKeyDown("space") && Time.time > nextFire && playingMode.text.Contains("手动"))
        {
            if(mode.intervalFirstPress == -1){
                updateFirstValuesForSwitching("intervalFirstPress");
            }
            nextFire = Time.time + fireRate;

            bulletCount--;
            if(bulletCount <= -2)
            {
                bulletCount = -1;
                return;
            }
            bullets.GetChild(bulletCount + 1).gameObject.SetActive(false);
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            GetComponent<AudioSource>().Play();
        }

        if(bulletCount == -1)
        {
            bulletTime += Time.deltaTime;
            if(bulletTime >= 5f)
            {
                bulletTime = 0;
                bulletCount = 9;
                foreach(Transform t in bullets)
                {
                    t.gameObject.SetActive(true);
                }
            }
        }
    }

    

    private void FixedUpdate()
    {
        if (playingMode.text.Contains("手动"))
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
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
            session.timeOfShot = System.DateTime.Now.Subtract(startTime).Seconds;
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
        newPlane.time = System.DateTime.Now.Subtract(startTime).Seconds;
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

    public void SaveItemInfo(string json)
    {
        string path = player.name + "_" + System.DateTime.Now.GetHashCode() + ".json";

        using (FileStream fs = new FileStream(path, FileMode.Create))
        {
            using (StreamWriter writer = new StreamWriter(fs))
            {
                writer.Write(json);
            }
        }
    }

    public void updateEnemyPositions(float position)
    {
        player.enemyPositions.Add(position);
    }
}
