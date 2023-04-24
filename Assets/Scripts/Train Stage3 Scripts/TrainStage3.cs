using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class TrainStage3 : MonoBehaviour {
    public Button button_trainStage3;

    IEnumerator Start()
    {
        button_trainStage3.interactable = false;
        yield return new WaitForSeconds(2f);
        button_trainStage3.interactable = true;
        button_trainStage3.GetComponentInChildren<Text>().text =  "继续";

        button_trainStage3.onClick.AddListener(moveToTrainStage3);
    }


    void moveToTrainStage3()
    {
        PlayerPrefs.SetInt("currentStage", 3);
        SceneManager.LoadScene("TrainingStage3");
    }
}
