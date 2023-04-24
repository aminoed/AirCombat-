using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;


public class stage3 : MonoBehaviour {

    public Button button_stage3;
    public Text tt;

    IEnumerator Start()
    {
        button_stage3.interactable = false;
        yield return new WaitForSeconds(2f);
        button_stage3.interactable = true;
        button_stage3.GetComponent<Text>().text = "Next";

        button_stage3.onClick.AddListener(moveToStage4);
    }

    void moveToStage4()
    {
        SceneManager.LoadScene("TrainStage1");
    }
}
