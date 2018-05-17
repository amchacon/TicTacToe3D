using System;
using System.Collections.Generic;

public static class Extensions
{
    public static void AddNewWinnerLine(this List<WinnerLine> wll, GameMode gameMode, Tile a, Tile b, Tile c, Tile d = null)
    {
        WinnerLine newWL;
        switch (gameMode)
        {
            case GameMode.MODE_3x3:
                //3x3
                newWL = new WinnerLine
                {
                    lines = new List<Tile>() { a, b, c }
                };
                wll.Add(newWL);
                break;
            case GameMode.MODE_4x4:
                //4x4
                newWL = new WinnerLine
                {
                    lines = new List<Tile>() { a, b, c, d }
                };
                wll.Add(newWL);
                break;
        }
    }

    public static int ToUnixTime(this DateTime input)
    {
        return (int)input.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
    }
}
