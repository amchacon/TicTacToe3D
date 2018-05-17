//TODO: create a method to load and parse from savedfiles

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public struct ReplayData
{
    public int playerID;
    public string tileID;
    public Vector3 tilePosition;

    public ReplayData(int playerID, Tile tileClicked)
    {
        this.playerID = playerID;
        tileID = tileClicked.Id;
        tilePosition = tileClicked.transform.position;
    }
}

public class ReplayManager
{
    string path = Application.dataPath + @"/Replay/TempReplay.json";
    public Dictionary<int, ReplayData> replayInfo;

    public ReplayManager()
    {
        replayInfo = new Dictionary<int, ReplayData>();
    }

    public void UpdateInfo(int index, int pID, Tile t)
    {
        ReplayData r = new ReplayData(pID, t);
        replayInfo.Add(index, r);
        PersistInfo(r);
    }

    /// <summary>
    /// If the game closes unexpectedly in the middle of a match, the replay file will not be lost, being able to review until the crash
    /// </summary>
    private void PersistInfo<T>(T obj)
    {
        if(!Directory.Exists(Application.dataPath + @"/Replay/"))
        {
            Directory.CreateDirectory(Application.dataPath + @"/Replay/");
        }

        using (StreamWriter stream = new StreamWriter(path, true))
        {
            string objJson = JsonUtility.ToJson(obj);
            stream.WriteLine(objJson);
        }
    }

    /// <summary>
    /// At the end of the match, the final file is renamed with the timestamp of that moment
    /// </summary>
    public void PersistFinalReplayFile()
    {
        if (File.Exists(path))
        {
            string newFile = Application.dataPath + @"/Replay/FinalReplay_" + DateTime.Now.ToUnixTime() + ".json";
            if (File.Exists(newFile))
            {
                File.Delete(newFile);
            }
            File.Move(path, newFile);
        }
    }
}