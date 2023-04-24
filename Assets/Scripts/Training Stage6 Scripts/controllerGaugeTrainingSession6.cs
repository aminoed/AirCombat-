using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;

public class controllerGaugeTrainingSession6 : MonoBehaviour
{

    private Vector3 MovingDirection;
    private int direction;
    private float RedSpeed;
    private float GreenSpeed;
    private float Speed;
    private System.DateTime startTime;
   // private System.DateTime OneMinTimerDeviation;
    private System.DateTime gameStartTime;
    private int minSavedCount;
    private float red_up = 7.0F;
    private float red_down = 3.0F;
    private int newStart = 0;
    private System.DateTime startPanelTime;
    private Deviations deviations;
    private controllerPlayerTrainingSession6 controlPlayer;
    private int countDeviations;
    private GameControllerTrainingSession6 gameController;
    public GameObject container;

    private string mode;

    public float Uplimit;
    public float Downlimit;
    public Text totalDeviations;
    public int deviationCount;
    public Text deviationsSaved;
    public int savedCount;

    public int gaugeSpeed;
    public int NumberofDeviations;

    public int runOutTime;
    //public int recordInterval; // the interval to record the deviations, now it is 60 seconds

    public Sessions session;
    private List<int> deviationsList;
    public List<double> correctionRateList;
    public double correctRate;

    public GameObject green;
    private void Start()
    {
        GameObject controllerPlayerObject = GameObject.FindWithTag("Player");
        if (controllerPlayerObject != null)
        {
            controlPlayer = controllerPlayerObject.GetComponent<controllerPlayerTrainingSession6>();
        }
        if (controlPlayer == null)
        {
            Debug.Log("Cannot find 'controllerPlayer' script for training session 5");
        }

        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameControllerTrainingSession6>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'gameController' script for training session 5");
        }


        countDeviations = 0;
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
        gameStartTime = System.DateTime.Now;
        //OneMinTimerDeviation = System.DateTime.Now;

        minSavedCount = 0;

        correctRate = 0;
        correctionRateList = new List<double>();   // deviations = new List<int>();
        deviationsList = new List<int>();

    }


    private void Update()
    {
            if (direction % 2 == 0 && System.DateTime.Now.Subtract(startTime).TotalSeconds >= runOutTime)
            { 
                if (newStart == 0)
                {
                  newStart = 1;
                  startTime = System.DateTime.Now;
                  if (deviations.deviationTimeInSec > 0)
                {
                    session.deviations.Add(deviations);
                }
                deviations = new Deviations();

                if (gameController.mode == "Manual")
                    {
                        deviations.mode = "M";
                    }
                    if (gameController.mode == "Automation")
                    {
                        deviations.mode = "A";
                    }

                deviations.deviationTimeInSec = (int)Math.Ceiling(System.DateTime.Now.Subtract(gameStartTime).TotalSeconds);
                MovingDirection = Vector3.back;
                MovingDirection.x = 0;

                }

                gameObject.transform.Translate(MovingDirection * Time.deltaTime * Speed);

                if (gameObject.transform.position.z < red_down)
                {
                    Speed = RedSpeed;
                }

                if (gameObject.transform.position.z <= Downlimit || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        savedCount += 1;
                        minSavedCount += 1;
                        updateSavedDeviations();
                        deviations.CorrectTimeInSec = (int)Math.Floor(System.DateTime.Now.Subtract(gameStartTime).TotalSeconds);
                    }
                    deviationCount += 1;
                    updateTotalDeviations();
                    countDeviations++;

                    gameObject.transform.position = green.transform.position;
                    Speed = GreenSpeed;
                    direction = UnityEngine.Random.Range(0, 100);
                    newStart = 0;
                }
            }
     
            if (direction % 2 != 0 && System.DateTime.Now.Subtract(startTime).TotalSeconds >= runOutTime)
            {
                if (newStart == 0)
                {
                newStart = 1;
                startTime = System.DateTime.Now;
                if (deviations.deviationTimeInSec > 0)
                {
                    session.deviations.Add(deviations);
                }
                deviations = new Deviations();

                if (gameController.mode == "Manual")
                    {
                        deviations.mode = "M";
                    }
                    if (gameController.mode == "Automation")
                    {
                        deviations.mode = "A";
                    }

                deviations.deviationTimeInSec = (int)Math.Ceiling(System.DateTime.Now.Subtract(gameStartTime).TotalSeconds);
                MovingDirection = Vector3.forward;
                MovingDirection.x = 0;

                }

                gameObject.transform.Translate(MovingDirection * Time.deltaTime * Speed);

                if (gameObject.transform.position.z > red_up)
                {
                    Speed = RedSpeed;
                }
                if (gameObject.transform.position.z >= Uplimit || Input.GetKeyDown(KeyCode.DownArrow))
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
        if (PlayerPrefs.GetInt("currentStage") == 5 && deviationCount != 0)
        {
            updateCorrectDeviation();
        }
        deviationsSaved.text = "Deviations Saved : " + savedCount;
    }

    public void updateCorrectDeviation()
    {
        if (deviationsList.Count() > 0)
        {
            deviationsList.Add(1);    // deviations.Count() - how many elements in list
        }
        else
        {
            deviationsList.Add(-1);
        }
    }
}

