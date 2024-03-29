using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Action : MonoBehaviour
{
    Dictionary<string, int> dict = new Dictionary<string, int>(9)
    {
        {"Square00", 0}, {"Square10", 1}, {"Square20", 2}, {"Square30", 3}, {"Square01", 4}, {"Square11", 5}, {"Square21", 6}, {"Square31", 7}, {"Square02", 8}, {"Square12", 9}, {"Square22", 10}, {"Square32", 11}, {"Square03", 12}, {"Square13", 13}, {"Square23", 14}, {"Square33", 15},
    };

    public void Click()
    {
        var data = Controller.instance.data;

        if (transform.GetChild(0).gameObject.activeSelf is false && transform.GetChild(1).gameObject.activeSelf is false)
        {
            FirstClick();
        }
        else
        {
            AdditionalClick();
        }
    }
    void FirstClick()
    {
        var data = Controller.instance.data;

        if (data.player == 1)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            data.scores[dict[gameObject.name]] = data.player;
            CheckWin();
            data.player = 2;
        }
        else
        {
            transform.GetChild(1).gameObject.SetActive(true);
            data.scores[dict[gameObject.name]] = data.player;
            CheckWin();
            data.player = 1;
        }
        Controller.instance.UpdateCurrentPlayer();
    }

    void AdditionalClick()
    {
        var data = Controller.instance.data;

        if (data.player == 1)
        {
            if (transform.GetChild(0).gameObject.activeSelf)
            {
                return;
            }
            else
            {
                transform.GetChild(1).gameObject.SetActive(false);
                transform.GetChild(0).gameObject.SetActive(true);
                data.scores[dict[gameObject.name]] = data.player;
                CheckWin();
                BombCheck();
                data.player = 2;
            }
        }
        else
        {
            if (transform.GetChild(1).gameObject.activeSelf)
            {
                return;
            }
            else
            {
                transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(1).gameObject.SetActive(true);
                data.scores[dict[gameObject.name]] = data.player;
                CheckWin();
                BombCheck();
                data.player = 1;
            }
        }
    }
    void BombCheck()
    {
        var data = Controller.instance.data;
        int rand = Random.Range(0, 100);

        if (transform.GetChild(6).gameObject.activeSelf)
        {
            if (rand < 25)
            {
                Controller.instance.TriggerExplosion();
            }
        }
        else if (transform.GetChild(5).gameObject.activeSelf)
        {
            if (rand < 20)
            {
                Controller.instance.TriggerExplosion();
            }
            transform.GetChild(6).gameObject.SetActive(true);
        }
        else if (transform.GetChild(4).gameObject.activeSelf)
        {
            if (rand < 15)
            {
                Controller.instance.TriggerExplosion();
            }
            transform.GetChild(5).gameObject.SetActive(true);
        }
        else if (transform.GetChild(3).gameObject.activeSelf)
        {
            if (rand < 10)
            {
                Controller.instance.TriggerExplosion();
            }
            transform.GetChild(4).gameObject.SetActive(true);
        }
        else if (transform.GetChild(2).gameObject.activeSelf)
        {
            if (rand < 5)
            {
                Controller.instance.TriggerExplosion();
            }
            transform.GetChild(3).gameObject.SetActive(true);
        }
        else transform.GetChild(2).gameObject.SetActive(true);
        Controller.instance.UpdateCurrentPlayer();
    }

    public void CheckWin()
    {
        var data = Controller.instance.data;

        //Horizontal Win Conditions 
        if(data.scores[0] == data.scores[1] && data.scores[1] == data.scores[2] && data.scores[2] == data.scores[3])
        {
            Controller.instance.win1.SetActive(true);
            TriggerWin();
        }
        if (data.scores[4] == data.scores[5] && data.scores[5] == data.scores[6] && data.scores[6] == data.scores[7])
        {
            Controller.instance.win2.SetActive(true);
            TriggerWin();
        }
        if (data.scores[8] == data.scores[9] && data.scores[9] == data.scores[10] && data.scores[10] == data.scores[11])
        {
            Controller.instance.win3.SetActive(true);
            TriggerWin();
        }
        if (data.scores[12] == data.scores[13] && data.scores[13] == data.scores[14] && data.scores[14] == data.scores[15])
        {
            Controller.instance.win4.SetActive(true);
            TriggerWin();
        }

        //Vertical Win Conditions
        if (data.scores[0] == data.scores[4] && data.scores[4] == data.scores[8] && data.scores[8] == data.scores[12])
        {
            Controller.instance.win5.SetActive(true);
            TriggerWin();
        }
        if (data.scores[1] == data.scores[5] && data.scores[5] == data.scores[9] && data.scores[9] == data.scores[13])
        {
            Controller.instance.win6.SetActive(true);
            TriggerWin();
        }
        if (data.scores[2] == data.scores[6] && data.scores[6] == data.scores[10] && data.scores[10] == data.scores[14])
        {
            Controller.instance.win7.SetActive(true);
            TriggerWin();
        }
        if (data.scores[3] == data.scores[7] && data.scores[7] == data.scores[11] && data.scores[11] == data.scores[15])
        {
            Controller.instance.win8.SetActive(true);
            TriggerWin();
        }

        //CrissCross Win Conditions
        if (data.scores[0] == data.scores[5] && data.scores[5] == data.scores[10] && data.scores[10] == data.scores[15])
        {
            Controller.instance.win9.SetActive(true);
            TriggerWin();
        }
        if (data.scores[3] == data.scores[6] && data.scores[6] == data.scores[9] && data.scores[9] == data.scores[12])
        {
            Controller.instance.win10.SetActive(true);
            TriggerWin();
        }
    }

    public void TriggerWin()
    {
        var data = Controller.instance.data;

        if (data.player == 1)
        {
            data.playerOneWins++;
        }
        else
        {
            data.playerTwoWins++;
        }
        Controller.instance.TriggerCelebration();
        Controller.instance.UpdateScores();
    }
}