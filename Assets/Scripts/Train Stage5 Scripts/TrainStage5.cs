using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class TrainStage5 : MonoBehaviour {
    public Button button_trainStage5;
    // public Text trainStageMessage;

    IEnumerator Start()
    {
        /*string message = trainStageMessage.text;
        message += "\n\n Your gauge game score is - " + (float)PlayerPrefs.GetInt("corrDeviation") / (float)PlayerPrefs.GetInt("totalDeviation");
        message += "\n Computer Shooting Score - " + PlayerPrefs.GetInt("computerShootingScore");
        message += "\n Your Shooting Score - " + PlayerPrefs.GetInt("yourShootingScore");
        trainStageMessage.text = message;*/
        button_trainStage5.interactable = false;
        yield return new WaitForSeconds(2f);
        button_trainStage5.interactable = true;
        button_trainStage5.GetComponentInChildren<Text>().text = "继续";

        button_trainStage5.onClick.AddListener(moveToTrainStage5);
    }

    void moveToTrainStage5()
    {
        PlayerPrefs.SetInt("currentStage", 5);
        SceneManager.LoadScene("TrainingStage6");
    }
}
