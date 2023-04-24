using System.Collections.Generic;

[System.Serializable]
public class Sessions{
    public string session;
    public int stage;
    public string mode;
    public List<Deviations> deviations = new List<Deviations>();

    //Time in seconds when player shoots down 1st plane since game stage start
    public int timeOfShot = -1;
    public List<Planes> planes = new List<Planes>();
    public int totalWhitePlanes = -1;
    public int totalBlackPlanes = -1;
    public int totalScore = -1;
    public List<Modes> modes = new List<Modes>();

}
