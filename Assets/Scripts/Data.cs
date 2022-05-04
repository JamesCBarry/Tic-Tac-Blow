using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data
{
    public static Data Instance;
    private void Awake() => Instance = this;

    public int player = 1;

    public int playerOneWins = 0;
    public int playerTwoWins = 0;

    public List<int> scores = Controller.instance.startingList;
}
