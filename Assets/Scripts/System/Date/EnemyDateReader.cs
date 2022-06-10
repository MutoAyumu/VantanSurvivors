using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDateReader : CSVReader
{
    protected override void Process(string[] st)
    {
        new EnemyDate(st);
    }
}
