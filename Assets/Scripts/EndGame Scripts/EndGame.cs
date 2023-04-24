using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour {

    public Text trainStageMessage;

    void Start()
    {
        string message = trainStageMessage.text;
        message += "\n\n 您的指针游戏绩效为: " + (float)PlayerPrefs.GetInt("corrDeviation") / (float)PlayerPrefs.GetInt("totalDeviation");
        //message += "\n Computer Shooting Score: " + PlayerPrefs.GetInt("computerShootingScore");
        //message += "\n Your Shooting Score: " + PlayerPrefs.GetInt("yourShootingScore");
        message += "\n 射击游戏的团队绩效为: " + PlayerPrefs.GetFloat("TeamShootingPerformance");
        trainStageMessage.text = message;
    }
}
