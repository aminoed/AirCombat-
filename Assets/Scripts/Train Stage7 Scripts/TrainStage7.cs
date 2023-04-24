using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class TrainStage7 : MonoBehaviour {

    public Button button_trainStage7;

    IEnumerator Start()
    {
        button_trainStage7.interactable = false;
        yield return new WaitForSeconds(2f);
        button_trainStage7.interactable = true;
        button_trainStage7.GetComponentInChildren<Text>().text = "继续";
        button_trainStage7.onClick.AddListener(moveToTrainStage7);
    }

    void moveToTrainStage7()
    {
        PlayerPrefs.SetInt("currentStage", 7);
        SceneManager.LoadScene("TrainingStage7");
    }
}
