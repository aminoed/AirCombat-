using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class TrainingStage1 : MonoBehaviour
{
    public int sessionTime;   //sessionTime indicates how long stage 1 will last
    public int runOutTime; // if deviations happened more than 3s, record as fails. 
    public GameObject container;
    private Vector3 MovingDirection;
    private int direction;   // // direction is even, downward; direction is odd, forward
    private float RedSpeed;
    private float GreenSpeed;
    private float Speed;
    private System.DateTime startTime;
    private System.DateTime endTime;
    private System.DateTime Stage1StartTime;
    private float red_up = 7.0F;
    private float red_down = 3.0F;
    private int newStart = 0;
    private Deviations deviations;
    private Sessions sessions;
    private Player player;
    

    public float Uplimit;
    public float Downlimit;
   // public float deviation;
    public Text totalDeviations;
    public int deviationCount;
    public Text deviationsSaved;
    public int savedCount;

    public float gaugeSpeed;
    public int NumberofDeviations;

    private AsyncOperation async;
    private bool sceneLoadSwitch;

    public GameObject green;
    private void Start()
    {
        sessions = new Sessions();
        sessions.session = "TRAINING";
        sessions.stage = 1;
        sessions.mode = "G";
        deviations = new Deviations();

        deviationCount = 0;
        savedCount = 0;
        updateSavedDeviations();
        updateTotalDeviations();

        direction = UnityEngine.Random.Range(0, 100);
        RedSpeed = (3.3f / 3.0f) * gaugeSpeed * 1.5f;
        GreenSpeed = (3.3f / ((60.0f / NumberofDeviations) - 1.55f)) * gaugeSpeed;
        Speed = GreenSpeed;

        startTime = System.DateTime.Now;
        Stage1StartTime = System.DateTime.Now;

        string json = PlayerPrefs.GetString("json");
        player = new Player();
        //player = JsonUtility.FromJson<Player>(json); // To convert the JSON back into an object, use JsonUtility.FromJson

        if (json != "")
        {
            player = JsonUtility.FromJson<Player>(json);
        }


    }

    void Update()
    {
        if (System.DateTime.Now.Subtract(Stage1StartTime).TotalSeconds >= sessionTime) //sessionTime
        {
            player.sessions.Add(sessions);
            string json = JsonUtility.ToJson(player);  // serialize it to JSON format by using JsonUtility.ToJson
            PlayerPrefs.SetString("json", json);
            SceneManager.LoadScene("TrainStage2");
            async = SceneManager.LoadSceneAsync("TrainStage2");
            if (!async.isDone) // if not success, result is true
            {
                sceneLoadSwitch = true;
                Debug.Log("go to stage 2 is not successful");
            }
            else
            {
                sceneLoadSwitch = false;
                Debug.Log("go to stage 2 is  successful");
            }
            if (sceneLoadSwitch == true)
            {
                SceneManager.LoadScene("TrainStage2");
            }


        }
        if (direction % 2 == 0 && System.DateTime.Now.Subtract(startTime).TotalSeconds >= 2)    // direction is even, downward
        {
            if (newStart == 0)   // a new start of loop
            {
                newStart = 1;
                startTime = System.DateTime.Now;
                if (deviations.deviationTimeInSec > 0)
                {
                    sessions.deviations.Add(deviations);
                }
                deviations = new Deviations();    
                deviations.mode = "G";
                deviations.deviationTimeInSec = (int)Math.Ceiling(System.DateTime.Now.Subtract(Stage1StartTime).TotalSeconds);
                MovingDirection = Vector3.back; //Vector3.back - Shorthand for writing Vector3(0, 0, -1), back to lower red zone
                MovingDirection.x = 0;
            }

            //Time.deltaTime - Use Time.deltaTime to move a GameObject in the y direction, at n units per second. Multiply n by Time.deltaTime and add to the y component.
            //transform.Translate - Moves the transform in the direction and distance of translation
           // gameObject.transform.Translate(MovingDirection * Time.deltaTime * Speed);  // the action of movement of cylinder

            if (gameObject.transform.position.z < red_down) // if entered into the lower red zone
            {
                Speed = RedSpeed;
            }
            gameObject.transform.Translate(MovingDirection * Time.deltaTime * Speed);  // I add this statement because when enters into red zone, the speed is updated to RedSpeed.

            if (gameObject.transform.position.z < Downlimit || Input.GetKeyDown(KeyCode.UpArrow)) // if reaches Downlimit of lower red zone
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))     
                {
                    savedCount += 1;
                    updateSavedDeviations();
                    deviations.CorrectTimeInSec = (int)Math.Floor(System.DateTime.Now.Subtract(Stage1StartTime).TotalSeconds);
                }
                deviationCount += 1;
                updateTotalDeviations();
                // this is original code: gameObject.transform.position = new Vector3(-12.0f, 2.5f, 5.0f);  // Vector3(-12.0f, 2.5f, 5.0f) 

                // I changed to following codes
                gameObject.transform.position = green.transform.position;


                Speed = GreenSpeed;
                direction = UnityEngine.Random.Range(0, 100);
                newStart = 0;
            }
        }

        if (direction % 2 != 0 && System.DateTime.Now.Subtract(startTime).TotalSeconds >= runOutTime) // direction is odd, forward
        {
            if (newStart == 0)
            {
                newStart = 1;
                startTime = System.DateTime.Now;
                if (deviations.deviationTimeInSec > 0)
                {
                    sessions.deviations.Add(deviations);
                }
                deviations = new Deviations();
                deviations.mode = "G";
                deviations.deviationTimeInSec = (int)Math.Ceiling(System.DateTime.Now.Subtract(Stage1StartTime).TotalSeconds);
                MovingDirection = Vector3.forward;
                MovingDirection.x = 0;
            }

            

            if (gameObject.transform.position.z > red_up)
            {
                Speed = RedSpeed;
            }

            gameObject.transform.Translate(MovingDirection * Time.deltaTime * Speed);


            if (gameObject.transform.position.z > Uplimit || Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    savedCount += 1;
                    updateSavedDeviations();
                    deviations.CorrectTimeInSec = (int)Math.Floor(System.DateTime.Now.Subtract(Stage1StartTime).TotalSeconds);
                }
                deviationCount += 1;
                updateTotalDeviations();
                // original codes: gameObject.transform.position = new Vector3(-12.0f, 2.5f, 5.0f);
                //  change to following codes
                gameObject.transform.position = green.transform.position;

                Speed = GreenSpeed;
                direction = UnityEngine.Random.Range(0, 100);
                newStart = 0;
            }
        }
    }

    void updateTotalDeviations()
    {
        totalDeviations.text = "Total Deviations : " + deviationCount;
    }

    void updateSavedDeviations()
    {
        deviationsSaved.text = "Deviations Saved : " + savedCount;
    }
}
