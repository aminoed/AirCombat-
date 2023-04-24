using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllerPlayerTrainingStage3 : MonoBehaviour {

    public float speed;
    public float session3Limitatiton;
    public Boundary boundary;
    public float tilt;
    public bool firstshot;
    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    public Sessions session;

    private float nextFire;
    private System.DateTime startTime;
    public Player player;

    private void Start()
    {
        startTime = System.DateTime.Now;
        session = new Sessions();
        session.session = "TRAINING";
        session.stage = 3;
        session.mode = "A";
        session.totalScore = 0;
        session.totalBlackPlanes = 0;
        session.totalWhitePlanes = 0;
        session.timeOfShot = -1;
        firstshot = false;
        string json = PlayerPrefs.GetString("json");
        player = new Player();
        //player = JsonUtility.FromJson<Player>(json);

        if (json != "")
        {
            player = JsonUtility.FromJson<Player>(json);
        }

    }

    private void Update()
    {
        if (System.DateTime.Now.Subtract(startTime).Seconds >= session3Limitatiton)
        {
            player.sessions.Add(session);
            string json = JsonUtility.ToJson(player);
            PlayerPrefs.SetString("json", json);
            SceneManager.LoadScene("TrainStage5");
        }
    }

    public void UpdateTimeOfShot()
    {
        if (!firstshot)
        {
            session.timeOfShot = System.DateTime.Now.Subtract(startTime).Seconds;
            firstshot = true;
        }
    }

    public void UpdateTotalScore(int score)
    {
        session.totalScore += score;
    }

    public void UpdateTotalBlackPlanes(int count)
    {
        session.totalBlackPlanes += count;
    }

    public void UpdateTotalWhitePlanes(int count)
    {
        session.totalWhitePlanes += count;
    }

    public void UpdatePlanes(Planes newPlane, Vector3 enemyPosition)
    {
        newPlane.verticalDistance = enemyPosition.z - GetComponent<Rigidbody>().position.z;
        newPlane.time = System.DateTime.Now.Subtract(startTime).Seconds;
        session.planes.Add(newPlane);
    }

    public void UpdateEnemyPositions(float position)
    {
        player.enemyPositions.Add(position);
    }
}
