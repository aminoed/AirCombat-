using System;
using UnityEngine;
using Random = UnityEngine.Random;
using Debug = UnityEngine.Debug;


public class MoverTrainingSession6 : MonoBehaviour
{

    public float speedLow;
    public float speedHigh;
    public float boltspeed;

    public float random;

    private float timer = 0;
    private float rx = 0;
    private controllerPlayerTrainingSession6 controlPlayer;

    void Start()
    {

        random = Random.Range(-2.0f, 2.0f);
        GameObject controllerPlayerObject = GameObject.FindWithTag("Player");
        if (controllerPlayerObject != null)
        {
            controlPlayer = controllerPlayerObject.GetComponent<controllerPlayerTrainingSession6>();
        }
        if (controlPlayer == null)
        {
            Debug.Log("Cannot find 'controllerPlayer' script for training session 6");
        }

    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 0.5)
        {
            rx = Mathf.Sin(Time.time);
        }

        if (gameObject.tag == "M2TrainingSession6")
        {
            // transform.Translate(new Vector3(rx*random,0.0f,speedHigh*Time.deltaTime),Space.Self);
            transform.Translate(new Vector3(rx, 0.0f, speedHigh) * Time.deltaTime, Space.Self);
            // GetComponent<Rigidbody>().velocity = transform.forward * speedHigh;

            // Debug.Log("===========lz==== rx: "+rx);
            // controlPlayer.updateTotalBlackPlanes(1);
        }
        else if (gameObject.tag == "M1TrainingSession6")
        {
            transform.Translate(new Vector3(rx * random, 0.0f, speedLow * Time.deltaTime), Space.Self);
            // GetComponent<Rigidbody>().velocity = transform.forward * speedLow;
            //  controlPlayer.updateTotalWhitePlanes(1);
        }
        if (gameObject.tag == "BoltTrainingSession6")
        {
            GetComponent<Rigidbody>().velocity = transform.forward * boltspeed;
        }
    }
}
