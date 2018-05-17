using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Config", fileName = "Game Config")]
public class GameConfig : ScriptableObject
{
    public GameMode gameMode;
    public GameObject[] playersPrefabs;
    public List<GameObject> playersInGame;
}

public enum GameMode
{
    MODE_3x3 = 3,
    MODE_4x4 = 4
}