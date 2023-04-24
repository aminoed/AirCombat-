using System;
using UnityEngine;
using UnityEngine.UI;

public class change : MonoBehaviour
{
    public GameObject changeBtn;
    public Text changeTxt;
    public Boolean mode;
    // Start is called before the first frame update
    void Start()
    {
        // changeBtn = getComponent<Button>();
        // changeTxt = changeBtn.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(changeTxt.text == "Button"){
            mode = true;
        }else{
            mode = false;
        }
        if(Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.O)){
            Debug.Log("Update both down!!!");
            // if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.O)){
                // Debug.Log("one up!!!");
                if(mode){
                    changeTxt.text = "changed";       
                }else{
                    changeTxt.text = "Button";
                }
            // }
        }
        
    }

    void FixedUpdate()
    {
        if(changeTxt.text == "Button"){
            mode = true;
        }else{
            mode = false;
        }
        if(Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.O)){
            Debug.Log("FixUpdate both down!!!");
            // if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.O)){
            //     Debug.Log("one up!!!");
            //     if(mode){
            //         changeTxt.text = "changed";       
            //     }else{
            //         changeTxt.text = "Button";
            //     }
            // }
        }
        
    }
}
