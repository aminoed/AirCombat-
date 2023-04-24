using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class trainStage1 : MonoBehaviour {

    public Button button_trainStage;

    IEnumerator Start()
    {
        button_trainStage.interactable = false;
        yield return new WaitForSeconds(2f);
        button_trainStage.interactable = true;
        button_trainStage.GetComponentInChildren<Text>().text = "继续";
        button_trainStage.onClick.AddListener(moveToTrainStage);
    }

    void moveToTrainStage()
    {
        PlayerPrefs.SetInt("currentStage", 1);
        SceneManager.LoadScene("TrainingStage2");
    }
}


