using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class TrainStage4 : MonoBehaviour {
    public Button button_trainStage4;

    IEnumerator Start()
    {
        button_trainStage4.interactable = false;
        yield return new WaitForSeconds(2f);
        button_trainStage4.interactable = true;
        button_trainStage4.GetComponentInChildren<Text>().text = "继续";

        button_trainStage4.onClick.AddListener(moveToTrainStage4);
    }

    void moveToTrainStage4()
    {
        PlayerPrefs.SetInt("currentStage", 4);
        SceneManager.LoadScene("TrainingStage4");
    }
}
