using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDateReader : CSVReader
{
    protected override void Process(string[] st)
    {
        new GameData(st);
    }
}
