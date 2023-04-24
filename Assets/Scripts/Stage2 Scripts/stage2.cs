using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class stage2 : MonoBehaviour {
   
    public Button button_stage2;

    void Start()
    {
        button_stage2.onClick.AddListener(moveToStage3);
    }

    void moveToStage3()
    {
        SceneManager.LoadScene("ExperienceForm");
    }
}
