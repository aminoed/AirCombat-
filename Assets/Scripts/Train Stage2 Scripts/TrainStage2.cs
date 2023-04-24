
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class TrainStage2 : MonoBehaviour {

    public Button button_trainStage2;
    IEnumerator Start()
    {
        button_trainStage2.interactable = false;
        yield return new WaitForSeconds(2f);
        button_trainStage2.interactable = true;
        button_trainStage2.GetComponentInChildren<Text>().text = "继续";

        button_trainStage2.onClick.AddListener(moveToTrainStage2);
    }


    void moveToTrainStage2()
    {
        PlayerPrefs.SetInt("currentStage", 2);
        SceneManager.LoadScene("TrainingStage2");
    }
}
