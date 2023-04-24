using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionByTime : MonoBehaviour {
    public float explosion_Time;

    void Start()

    {


        if (gameObject.tag == "Explosion")
        {
            Destroy(gameObject, explosion_Time);
        }
    }


}


