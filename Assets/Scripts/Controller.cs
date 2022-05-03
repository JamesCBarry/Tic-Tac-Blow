using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading;

public class Controller : MonoBehaviour
{
    public static Controller instance;

    private void Awake() => instance = this;

    [SerializeField] private TMP_Text Player1Score;
    [SerializeField] private TMP_Text Player2Score;

    public List<GameObject> squareList = new List<GameObject>();
    public GameObject squares;
    public GameObject explosion;
    public GameObject temporaryExplosion;
    public Transform grid;

    public GameObject win1;
    public GameObject win2;
    public GameObject win3;
    public GameObject win4;
    public GameObject win5;
    public GameObject win6;
    public GameObject win7;
    public GameObject win8;

    public GameObject WinText;
    public GameObject TieText;
    public GameObject ExplosionText;
    public GameObject ResetButton;

    public Data data;

    int widthUnits = 3;
    int heightUnits = 3;

    int width = 300;
    int height = 300;

    Dictionary<string, int> dict = new Dictionary<string, int>(9);


    // Start is called before the first frame update
    void Start()
    {
        data = new Data();
        CreateGrid();
    }
    public void Update()
    {
        if (ResetButton.activeSelf)
        {
            WinText.transform.eulerAngles = WinText.transform.eulerAngles + new Vector3(0, 0, 1);
            TieText.transform.eulerAngles = TieText.transform.eulerAngles + new Vector3(0, 0, -1);
            ExplosionText.transform.eulerAngles = ExplosionText.transform.eulerAngles + new Vector3(0, 1, 0);
        }
    }

    public void TriggerCelebration()
    {
        WinText.gameObject.SetActive(true);
        ResetButton.gameObject.SetActive(true);
    }

    public void TriggerExplosion()
    {
        temporaryExplosion = Instantiate(explosion, new Vector2(0, 0), Quaternion.identity, grid);
        ExplosionText.gameObject.SetActive(true);
        ResetButton.gameObject.SetActive(true);
        if (data.player == 1)
        {
            data.playerOneWins--;
        }
        else
        {
            data.playerTwoWins--;
        }
        UpdateScores();
    }

    public void ResetGame()
    {
        ResetButton.SetActive(false);
        WinText.SetActive(false);
        ExplosionText.SetActive(false);
        TieText.SetActive(false);
        Destroy(temporaryExplosion);
        ResetGrid();
        CreateGrid();
        data.scores = new List<int>() { 3, 4, 5, 6, 7, 8, 9, 10, 11 };
    }

    public void UpdateScores()
    {
        Player1Score.text = "Player 1: " + data.playerOneWins;
        Player2Score.text = "Player 2: " + data.playerTwoWins;
    }

    void CreateGrid()
    {
        for (int y = 0; y < heightUnits; y++)
        {
            for (int x = 0; x < widthUnits; x++)
            {
                GameObject GameObject = Instantiate(squares, new Vector2(0, 0), Quaternion.identity, grid);
                GameObject.transform.localPosition = new Vector2(x * width - width, y * height - height);
                GameObject.name = "Square" + x + y;
                squareList.Add(GameObject);
            }
        }
    }

    void ResetGrid()
    {
        for (int i = 0; i < squareList.Count; i++)
        {
            Destroy(squareList[i]);
        }
        win1.gameObject.SetActive(false);
        win2.gameObject.SetActive(false);
        win3.gameObject.SetActive(false);
        win4.gameObject.SetActive(false);
        win5.gameObject.SetActive(false);
        win6.gameObject.SetActive(false);
        win7.gameObject.SetActive(false);
        win8.gameObject.SetActive(false);
    }
}