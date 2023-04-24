using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class TrainStage6 : MonoBehaviour {

    public Button button_trainStage6;

    IEnumerator Start()
    {
        button_trainStage6.interactable = false;
        yield return new WaitForSeconds(2f);
        button_trainStage6.interactable = true;
        button_trainStage6.GetComponentInChildren<Text>().text = "继续";
        button_trainStage6.onClick.AddListener(moveToTrainStage6);
    }

    void moveToTrainStage6()
    {
        PlayerPrefs.SetInt("currentStage", 6);
        SceneManager.LoadScene("TrainingStage6");
    }
}
