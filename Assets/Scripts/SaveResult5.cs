using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveResult5 : MonoBehaviour
{
    public List<Transform> question = new List<Transform>();

    public Transform doneBtn;

    public Transform resultTrans;

    StreamWriter writer;

    public InputField t1;

    public Text tips;
    private  Questions qs;
    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        qs = new Questions();
        qs.QuestionStageName = "stage 5";

        string json = PlayerPrefs.GetString("json");
        player = new Player();
        if (json != "")
        {
            player = JsonUtility.FromJson<Player>(json);
        }
    }

    // Update is called once per frame
    void Update()
    {
    
    }


    public void NextPage()
    {
        if (!CheckDone())
        {
            tips.text = "有问题未作答！";
            return;
        }
        else
        {
            tips.text = "";
        }
        for (int i = 0;i<question.Count;i++)
        {
            if(i < question.Count/2)
            {
                question[i].gameObject.SetActive(false);
            }
            else
            {
                question[i].gameObject.SetActive(true);
            }
        }
        doneBtn.gameObject.SetActive(true);
      
    }

    public void SaveTxt()
    {
        if (!CheckDone())
        {
            tips.text = "有问题未作答！";
            return;
        }
        else
        {
            tips.text = "";
        }

        for (int i = 0; i < question.Count; i++)
        {
            if (question[i].name == "question1")
            {
                qs.QuestionAnswer.Add(t1.text);
            }
            else
            {
                foreach (Transform t in question[i])
                {
                    Toggle to = t.GetComponent<Toggle>();
                    if (to.isOn)
                    {
                        qs.QuestionAnswer.Add(to.transform.name);
                    }

                }
            }        
        }


        resultTrans.GetComponentInChildren<Text>().text = "感谢参与！";
        resultTrans.gameObject.SetActive(true);

        player.Qs.Add(qs);
        string json = JsonUtility.ToJson(player);
        PlayerPrefs.SetString("json", json);
        SceneManager.LoadScene("TrainStage7");       
    }

    bool CheckDone()
    {

        for (int i = 0; i < question.Count; i++)
        {
            if (question[i].gameObject.activeSelf)
            {
                bool b = false;
                foreach (Transform t in question[i])
                {
                    Toggle to = t.GetComponent<Toggle>();

                    if (to == null || to.isOn)
                    {
                        b = true;
                    }
                }

                if (!b)
                {
                    return false;
                }
            }
        }

        return true;
    }
}
