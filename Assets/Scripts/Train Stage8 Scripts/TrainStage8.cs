using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class TrainStage8 : MonoBehaviour {

    public Button button_trainStage8;
    //public Text trainStageMessage;

    IEnumerator Start()
    {
        /*string message = trainStageMessage.text;
        message += "\n\n Your gauge game score is - " + (float)PlayerPrefs.GetInt("corrDeviation") / (float)PlayerPrefs.GetInt("totalDeviation");
        message += "\n Computer Shooting Score - " + PlayerPrefs.GetInt("computerShootingScore");
        message += "\n Your Shooting Score - " + PlayerPrefs.GetInt("yourShootingScore");
        trainStageMessage.text = message;*/

        button_trainStage8.interactable = false;
        yield return new WaitForSeconds(2f);
        button_trainStage8.interactable = true;
        button_trainStage8.GetComponentInChildren<Text>().text = "继续";
        button_trainStage8.onClick.AddListener(moveToTrainStage8);
    }

    void moveToTrainStage8()
    {
        PlayerPrefs.SetInt("currentStage", 8);
        SceneManager.LoadScene("TrainingStage7");
    }
}
