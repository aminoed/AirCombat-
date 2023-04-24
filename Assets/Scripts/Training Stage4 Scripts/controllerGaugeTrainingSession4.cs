using UnityEngine;
using UnityEngine.UI;
using System;

public class controllerGaugeTrainingSession4 : MonoBehaviour {

    private Vector3 MovingDirection;
    private int direction;
    private float RedSpeed;
    private float GreenSpeed;
    private float Speed;
    private System.DateTime startTime;
    
    private System.DateTime OneMinTimerDeviation;
    private System.DateTime gameStartTime;
    private int minSavedCount;
    private float red_up = 7.0F;
    private float red_down = 3.0F;
    private int newStart = 0;
   
    private Deviations deviations;
    private controllerPlayerTrainingSession4 controlPlayer;
    private GameControllerTrainingSession4 gameController;
    private int countDeviations;
   // public int recordInterval; // the interval to record the deviations, now it is 60 seconds

    public float Uplimit;
    public float Downlimit;
    //public float deviation;
    public Text totalDeviations;
    public int deviationCount;
    public Text deviationsSaved;
    public int savedCount;
    public Text playingMode;
    private string mode;

    public float gaugeSpeed;
    public int NumberofDeviations;

    public int runOutTime;
    public GameObject green;

    private void Start()
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

        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameControllerTrainingSession4>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'gameController' script for training session 4");
        }
        countDeviations = 0;
        deviations = new Deviations();
        deviationCount = 0;
        savedCount = 0;
        updateSavedDeviations();
        updateTotalDeviations();
        direction = UnityEngine.Random.Range(0, 100);
        RedSpeed = (3.3f / 3.0f) * gaugeSpeed * 1.5f;
        GreenSpeed = (3.3f / ((60.0f / NumberofDeviations) - 1.55f))* gaugeSpeed;
        Speed = GreenSpeed;
        startTime = System.DateTime.Now;
       
        gameStartTime = System.DateTime.Now;
        OneMinTimerDeviation = System.DateTime.Now;

        minSavedCount = 0;
    }

    private void Update()
    {
        mode = playingMode.text;

        if (System.DateTime.Now.Subtract(OneMinTimerDeviation).TotalSeconds >= gameController.session4intervalTime)
        {       
            countDeviations = 0;
            OneMinTimerDeviation = System.DateTime.Now;
        }
        else if (System.DateTime.Now.Subtract(OneMinTimerDeviation).TotalSeconds < gameController.session4intervalTime && countDeviations < NumberofDeviations)
        {
            if (direction % 2 == 0 && System.DateTime.Now.Subtract(startTime).TotalSeconds >= runOutTime)
            {
                deviations = new Deviations();
                if (newStart == 0)
                {                  
                    deviations.deviationTimeInSec = (int)Math.Ceiling(System.DateTime.Now.Subtract(gameStartTime).TotalSeconds);
                    MovingDirection = Vector3.back;
                    MovingDirection.x = 0;

                    if (gameController.mode == "Manual")
                    {
                        deviations.mode = "M";
                    }
                    if (gameController.mode == "Automation")
                    {
                        deviations.mode = "A";
                    }

                    newStart = 1;
                    startTime = System.DateTime.Now;
                }

                gameObject.transform.Translate(MovingDirection * Time.deltaTime * Speed);

                if (gameObject.transform.position.z < red_down)
                {
                    Speed = RedSpeed;
                }
                if (gameObject.transform.position.z < Downlimit || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    deviationCount += 1;
                    updateTotalDeviations();
                    countDeviations++;
                    if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        savedCount += 1;
                        minSavedCount += 1;
                        updateSavedDeviations();
                        deviations.CorrectTimeInSec = (int)Math.Floor(System.DateTime.Now.Subtract(gameStartTime).TotalSeconds);
                    }
                    gameObject.transform.position = green.transform.position;
                    Speed = GreenSpeed;
                    direction = UnityEngine.Random.Range(0, 100);
                    newStart = 0;
                }

                if (deviations.deviationTimeInSec > 0)
                {
                    controlPlayer.updateDeviations(deviations);
                }

            }

            if (direction % 2 != 0 && System.DateTime.Now.Subtract(startTime).TotalSeconds >= runOutTime)
            {
                deviations = new Deviations();
                if (newStart == 0)
                {                   
                    deviations.deviationTimeInSec = (int)Math.Ceiling(System.DateTime.Now.Subtract(gameStartTime).TotalSeconds);
                    MovingDirection = Vector3.forward;
                    MovingDirection.x = 0;
                    if (gameController.mode == "Manual")
                    {
                        deviations.mode = "M";
                    }
                    if (gameController.mode == "Automation")
                    {
                        deviations.mode = "A";
                    }
                    newStart = 1;
                    startTime = System.DateTime.Now;
                }

                gameObject.transform.Translate(MovingDirection * Time.deltaTime * Speed);

                if (gameObject.transform.position.z > red_up)
                {
                    Speed = RedSpeed;
                }
                if (gameObject.transform.position.z > Uplimit || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    deviationCount += 1;
                    updateTotalDeviations();
                    countDeviations++;
                    if (Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        savedCount += 1;
                        minSavedCount += 1;
                        updateSavedDeviations();
                        deviations.CorrectTimeInSec = (int)Math.Floor(System.DateTime.Now.Subtract(gameStartTime).TotalSeconds);
                    }
                    gameObject.transform.position = green.transform.position;
                    Speed = GreenSpeed;
                    direction = UnityEngine.Random.Range(0, 100);
                    newStart = 0;
                }
                if (deviations.deviationTimeInSec > 0)
                {
                    controlPlayer.updateDeviations(deviations);
                }
            }
        }
    }


    void updateTotalDeviations()
    {
        PlayerPrefs.SetInt("totalDeviation", deviationCount);
        totalDeviations.text = "Total Deviations : " + deviationCount;
    }

    void updateSavedDeviations()
    {
        PlayerPrefs.SetInt("corrDeviation", savedCount);
        deviationsSaved.text = "Deviations Saved : " + savedCount;
    }
}
