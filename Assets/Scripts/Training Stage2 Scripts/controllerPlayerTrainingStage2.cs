using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable] // so that we can see Boundary from inspectors, bcz Boundary is not a class from MonoBehaviour, it is an internal class
public class Boundary
{
    public float xMin, xMax, zMin, zMax;  // limit the position of plane within a scope of x-aix and z-aix
}

public class controllerPlayerTrainingStage2 : MonoBehaviour {

    public int sessionTimeLimitaton;  // how long the current session will last
    public float speed;   // speed indicates the speed of player's plane
    public Boundary boundary;
    public float tilt;
    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate; // the interval between two fires
    public bool firstshot;
    public Sessions session;

    private float nextFire;
    private System.DateTime startTime;
    public Player player;

    private void Start()
    {
        startTime = System.DateTime.Now;
        session = new Sessions();
        session.session = "TRAINING";
        session.stage = 2;
        session.mode = "M";
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
    void Update()
    {
        if(System.DateTime.Now.Subtract(startTime).TotalSeconds >= sessionTimeLimitaton){
           // SceneManager.LoadScene("TrainStage3");
            //SceneManager.SetActiveScene(SceneManager.GetSceneByName("TrainStage3"));
            player.sessions.Add(session);
            string json = JsonUtility.ToJson(player);
            PlayerPrefs.SetString("json", json);
            SceneManager.LoadScene("EndGame2");
        }
        if (Input.GetKeyDown("space") && Time.time > nextFire) // Fire!!!!!!!!
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            GetComponent<AudioSource>().Play();
        }
    }

    private void FixedUpdate()   // update the position of player's plane
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        // movement indicates the move direction from the keyboard
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f); // the player's plane can only be moved toward left or right in X-aix
        GetComponent<Rigidbody>().velocity = movement * speed; // speed indicates the speed of player's plane

        GetComponent<Rigidbody>().position = new Vector3
        (
            Mathf.Clamp(GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax),   // limit the position of plane within a scope of x-aix
            0.0f,                                                                             //  in y-aix
            Mathf.Clamp(GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)  //  limit the position of plane within a scope of z-aix
        );

       GetComponent<Rigidbody>().rotation = Quaternion.Euler(0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);   // I comment this one bcz I think it is useless, the player plane never tilts 
    }

    public void UpdateTimeOfShot(){
        if(!firstshot){
            session.timeOfShot = System.DateTime.Now.Subtract(startTime).Seconds;
            firstshot = true;
        }
    }

    public void UpdateTotalScore(int score){
        session.totalScore += score;
    }

    public void UpdateTotalBlackPlanes(int count){
        session.totalBlackPlanes += count;
    }

    public void UpdateTotalWhitePlanes(int count)
    {
        session.totalWhitePlanes += count;
    }

    public void UpdatePlanes(Planes newPlane, Vector3 enemyPosition){
        newPlane.verticalDistance = enemyPosition.z - GetComponent<Rigidbody>().position.z;
        newPlane.time = System.DateTime.Now.Subtract(startTime).Seconds;
        session.planes.Add(newPlane);
    }

    public void UpdateEnemyPositions(float position){
        player.enemyPositions.Add(position);
    }
}
