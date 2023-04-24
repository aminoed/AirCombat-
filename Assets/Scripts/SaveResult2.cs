using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveResult2 : MonoBehaviour
{
    public List<Transform> question = new List<Transform>();

    public Transform nextBtn;
    public Transform doneBtn;
    public Transform lastBtn;

    public Transform resultTrans;

    StreamWriter writer;
    int count = 1;

    public Text t3;
    public Text t5;

    public Slider s3;
    public Slider s5;

    public Text tips;
    private Questions qs;
    public Player player;
    // Start is called before the first frame update

    void Start()
    {
        qs = new Questions();
        qs.QuestionStageName = "stage 2";

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

    public void ChangeT3()
    {
        t3.text =((int)(s3.value * 100)).ToString() + "%";
    }

    public void ChangeT5()
    {
        t5.text = ((int)(s5.value * 100)).ToString() + "%";
    }

    public void LastPage()
    {
        count--;

        if (count == 3)
        {
            for (int i = 0; i < 5; i++)
            {
                question[i].gameObject.SetActive(false);

            }
            for (int i = 5; i < 10; i++)
            {
                question[i].gameObject.SetActive(false);

            }
            for (int i = 10; i < 15; i++)
            {
                question[i].gameObject.SetActive(true);

            }
            for (int i = 15; i < 16; i++)
            {
                question[i].gameObject.SetActive(false);

            }
            lastBtn.gameObject.SetActive(true);
            doneBtn.gameObject.SetActive(false);
            nextBtn.gameObject.SetActive(true);
        }


        if (count == 2)
        {
            for (int i = 0; i < 5; i++)
            {
                question[i].gameObject.SetActive(false);

            }
            for (int i = 5; i < 10; i++)
            {
                question[i].gameObject.SetActive(true);

            }
            for (int i = 10; i < 15; i++)
            {
                question[i].gameObject.SetActive(false);

            }
            for (int i = 15; i < 16; i++)
            {
                question[i].gameObject.SetActive(false);

            }
            lastBtn.gameObject.SetActive(true);
            doneBtn.gameObject.SetActive(false);
            nextBtn.gameObject.SetActive(true);
        }

        if (count == 1)
        {
            for (int i = 0; i < 5; i++)
            {
                question[i].gameObject.SetActive(true);
            }
            for (int i = 5; i < 10; i++)
            {
                question[i].gameObject.SetActive(false);

            }
            for (int i = 10; i < 15; i++)
            {
                question[i].gameObject.SetActive(false);

            }
            for (int i = 15; i < 16; i++)
            {
                question[i].gameObject.SetActive(false);
            }
            lastBtn.gameObject.SetActive(false);          
        }
    }

    public void NextPage()
    {
        Debug.Log(CheckDone());
        if (!CheckDone())
        {
            tips.text = "有问题未作答！";
            return;
        }
        else
        {
            tips.text = "";
        }

        count++;
        if(count == 2)
        {
            for (int i = 0; i < 5; i++)
            {
                question[i].gameObject.SetActive(false);
            }
            for (int i = 5; i < 10; i++)
            {
                question[i].gameObject.SetActive(true);

            }
            lastBtn.gameObject.SetActive(true);
        }

        if (count == 3)
        {
            for (int i = 0; i < 5; i++)
            {
                question[i].gameObject.SetActive(false);

            }
            for (int i = 5; i < 10; i++)
            {
                question[i].gameObject.SetActive(false);

            }
            for (int i = 10; i < 15; i++)
            {
                question[i].gameObject.SetActive(true);

            }
            for (int i = 15; i < 16; i++)
            {
                question[i].gameObject.SetActive(false);

            }

            nextBtn.gameObject.SetActive(true);
        }

        if (count == 4)
        {
            for (int i = 0; i < 5; i++)
            {
                question[i].gameObject.SetActive(false);

            }
            for (int i = 5; i < 10; i++)
            {
                question[i].gameObject.SetActive(false);

            }
            for (int i = 10; i < 15; i++)
            {
                question[i].gameObject.SetActive(false);

            }
            for (int i = 15; i < 16; i++)
            {
                question[i].gameObject.SetActive(true);

            }
            doneBtn.gameObject.SetActive(true);
            nextBtn.gameObject.SetActive(false);
        }
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
            if (question[i].name == "1")
                foreach (Transform t in question[i])
                {
                    Toggle to = t.GetComponent<Toggle>();
                    if (to.isOn)
                    {
                        qs.QuestionAnswer.Add(to.transform.name);
                    }
                }
            else if (question[i].name == "2")
                foreach (Transform t in question[i])
                {
                    Toggle to = t.GetComponent<Toggle>();
                    if (to.isOn)
                    {
                        qs.QuestionAnswer.Add(to.transform.name);
                    }
                }

            else if (question[i].name == "3")
            {
                qs.QuestionAnswer.Add(t3.text);
            }


            else if (question[i].name == "4")
                foreach (Transform t in question[i])
                {
                    Toggle to = t.GetComponent<Toggle>();
                    if (to.isOn)
                    {
                        qs.QuestionAnswer.Add(to.transform.name);
                    }
                }

            else if (question[i].name == "5")
            {
                qs.QuestionAnswer.Add(t5.text);
            }

            else
                foreach (Transform t in question[i])
                {
                    Toggle to = t.GetComponent<Toggle>();
                    if (to.isOn)
                    {
                        qs.QuestionAnswer.Add(to.transform.name);
                    }

                }
        }

        resultTrans.GetComponentInChildren<Text>().text = "感谢您的参与！";
        resultTrans.gameObject.SetActive(true);

        player.Qs.Add(qs);
        string json = JsonUtility.ToJson(player);
        PlayerPrefs.SetString("json", json);
        SceneManager.LoadScene("TrainStage3");
    }


    bool CheckDone()
    {

        for(int i = 0; i< question.Count; i++)
        {
            if(question[i].gameObject.activeSelf)
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

                if(!b)
                {
                    return false;
                }
            }
        }

        return true;
    }
}
