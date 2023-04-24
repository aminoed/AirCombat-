using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class stage1 : MonoBehaviour {

    public Button button_stage1;
    public InputField pName;

    private Player player;
	
    void Start () {
        player = new Player();
        button_stage1.onClick.AddListener(moveToStage2);
    }
	
	void moveToStage2() {

        string sName = pName.text;

        if (!sName.Equals("") && !sName.Equals(""))
        {
            PlayerPrefs.SetInt("currentStage", 1);
            player.game = "AB_Low";
            player.name = player.game + sName;
            PlayerPrefs.SetString("json", JsonUtility.ToJson(player));
            SceneManager.LoadScene("TrainStage1");
        }
    }
}
