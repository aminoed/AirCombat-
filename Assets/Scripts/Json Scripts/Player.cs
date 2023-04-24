using System.Collections.Generic;

[System.Serializable]
public class Player {
    public string name;
    public string game;
    public List<Sessions> sessions = new List<Sessions>();
    public List<float> enemyPositions = new List<float>();
    public List<Questions> Qs = new List<Questions>();
    //public int stage;
    public float teamShootingPerformance;
}
