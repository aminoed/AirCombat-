using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SaveResult7 : MonoBehaviour
{
    public List<Transform> question = new List<Transform>();

    public Transform nextBtn;
    public Transform doneBtn;
    public Transform lastBtn;

    public Transform resultTrans;
    int count = 1;

    //it decide which page is the first
    bool randomPage;

    public Text t5; //Q13 总体团队多满意？
    public Text t9; //Q18 电脑的射击技能应该负多少责任？
    public Text t10;//Q19 自己的射击技能应该负多少责任？
    public Text t11;//Q20 模式切换决策应该负多少责任？

    public Slider s5;//Q13 总体团队多满意？
    public Slider s9;//Q18 电脑的射击技能应该负多少责任？
    public Slider s10;//Q19 自己的射击技能应该负多少责任？
    public Slider s11;//Q20 模式切换决策应该负多少责任？

    public InputField t12;
    public Text tips;
    private Questions qs;
    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        //set random seed. if is 1, then is true; if is 0, then is false.
        randomPage = (Random.Range(0,2)==1)?true:false;
        //change the top result according to the random seed.
        ShowTruResult(2,randomPage);
        ShowTruResult(5,randomPage);
        qs = new Questions();
        qs.QuestionStageName = "stage 7";      
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



    public void ChangeT5()
    {
        t5.text = ((int)(s5.value * 100)).ToString();
    }
    public void ChangeT9()
    {
        t9.text = ((int)(s9.value * 100)).ToString() + "%";
    }
    public void ChangeT10()
    {
        t10.text = ((int)(s10.value * 100)).ToString() + "%";
    }
    public void ChangeT11()
    {
        t11.text = ((int)(s11.value * 100)).ToString() + "%";
    }

    public void LastPage()
    {
        count--;
        if (count == 1)
        {
            for (int i = 0; i < 2; i++)
            {
                question[i].gameObject.SetActive(true);

            }
            for (int i = 2; i < 5; i++)
            {
                question[i].gameObject.SetActive(false);

            }
            for (int i = 5; i < 8; i++)
            {
                question[i].gameObject.SetActive(false);

            }
            lastBtn.gameObject.SetActive(false);
        }

        if (count == 2)
        {
            for (int i = 2; i < 5; i++)
            {
                question[i].gameObject.SetActive(randomPage);

            }
            for (int i = 5; i < 8; i++)
            {
                question[i].gameObject.SetActive(!randomPage);

            }           
        }
        if (count == 3)
        {
            for (int i = 2; i < 5; i++)
            {
                question[i].gameObject.SetActive(!randomPage);

            }

            for (int i = 5; i < 8; i++)
            {
                question[i].gameObject.SetActive(randomPage);

            }
            for (int i = 8; i < 13; i++)
            {
                question[i].gameObject.SetActive(false);

            }           
        }

        if (count == 4)
        {
            for (int i = 8; i < 13; i++)
            {
                question[i].gameObject.SetActive(true);

            }
            for (int i = 13; i < 18; i++)
            {
                question[i].gameObject.SetActive(false);

            }            
        }


        if (count == 5)
        {
            for (int i = 13; i < 18; i++)
            {
                question[i].gameObject.SetActive(true);

            }
            for (int i = 18; i < 21; i++)
            {
                question[i].gameObject.SetActive(false);

            }

            doneBtn.gameObject.SetActive(false);
            nextBtn.gameObject.SetActive(true);
            lastBtn.gameObject.SetActive(true);
        }
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
        count++;
        if (count == 2)
        {
            for (int i = 0; i < 2; i++)
            {
                question[i].gameObject.SetActive(false);

            }

            for (int i = 2; i < 5; i++)
            {
                question[i].gameObject.SetActive(randomPage);
                Debug.Log("randomPage = "+randomPage);
            }

            for (int i = 5; i < 8; i++)
            {
                question[i].gameObject.SetActive(!randomPage);

            }
            lastBtn.gameObject.SetActive(true);
        }

        if (count == 3)
        {
            for (int i = 2; i < 5; i++)
            {
                question[i].gameObject.SetActive(!randomPage);

            }
            for (int i = 5; i < 8; i++)
            {
                question[i].gameObject.SetActive(randomPage);

            }

            lastBtn.gameObject.SetActive(true);
        }
        if (count == 4)
        {
            for (int i = 2; i < 5; i++)
            {
                question[i].gameObject.SetActive(false);

            }

            for (int i = 5; i < 8; i++)
            {
                question[i].gameObject.SetActive(false);

            }
            for (int i = 8; i < 13; i++)
            {
                question[i].gameObject.SetActive(true);
            }          
            lastBtn.gameObject.SetActive(true);
        }

        if (count == 5)
        {
            for (int i = 8; i < 13; i++)
            {
                question[i].gameObject.SetActive(false);

            }
            for (int i = 13; i < 18; i++)
            {
                question[i].gameObject.SetActive(true);

            }

            lastBtn.gameObject.SetActive(true);
        }
       

        if (count == 6)
        {
            for (int i = 13; i < 18; i++)
            {
                question[i].gameObject.SetActive(false);

            }
            for (int i = 18; i < 21; i++)
            {
                question[i].gameObject.SetActive(true);

            }

            doneBtn.gameObject.SetActive(true);
            nextBtn.gameObject.SetActive(false);
        }
    }

    //when the questions change, the top result should also change.
    private void ShowTruResult(int index, bool isShow){
        question[index].gameObject.transform.GetChild(0).gameObject.SetActive(isShow);
        question[index].gameObject.transform.GetChild(1).gameObject.SetActive(!isShow);
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

            if (question[i].name == "13") //Q13 总体团队多满意？
            {
                //result += question[i].name + ":" + t5.text + " ";
                qs.QuestionAnswer.Add(t5.text);
            }
            else if (question[i].name == "18") //Q18 电脑的射击技能应该负多少责任？
            {
                //result += question[i].name + ":" + t9.text + " ";
                qs.QuestionAnswer.Add(t9.text);
            }
            else if (question[i].name == "19") //Q19 自己的射击技能应该负多少责任？
            {
                //result += question[i].name + ":" + t10.text + " ";
                qs.QuestionAnswer.Add(t10.text);
            }
            else if (question[i].name == "20")//Q20 模式切换决策应该负多少责任？
            {
                //result += question[i].name + ":" + t5.text + " ";
                qs.QuestionAnswer.Add(t11.text);
            }
            else if (question[i].name == "21") //您的学号?
            {
                //result += question[i].name + ":" + t12.text + " ";
                qs.QuestionAnswer.Add(t12.text);
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

        player.Qs.Add(qs);
        string json = JsonUtility.ToJson(player);
        PlayerPrefs.SetString("json", json);
        SaveItemInfo(json);
        resultTrans.GetComponentInChildren<Text>().text = "感谢您的参与！";
        resultTrans.gameObject.SetActive(true);
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


    public void SaveItemInfo(string json)
    {
        string path = player.name + "_" + System.DateTime.Now.GetHashCode() + ".json";

        using (FileStream fs = new FileStream(path, FileMode.Create))
        {
            using (StreamWriter writer = new StreamWriter(fs))
            {
                writer.Write(json);
            }
        }
    }
}
