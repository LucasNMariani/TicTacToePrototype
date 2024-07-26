using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField]
    private GameObject[] _tictactoeSpaces;
    private List<Tuple<Button, TextMeshProUGUI>> _tttGridButtons = new List<Tuple<Button, TextMeshProUGUI>>();
    [SerializeField]
    private GameObject[] _turnIcons;
    [SerializeField]
    private GameObject _winnerPanel;
    [SerializeField]
    private TextMeshProUGUI _winnerText; 
    [SerializeField]
    private GameObject[] _winnerLineFeedback;
    [SerializeField]
    private TextMeshProUGUI[] _scores;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        if (!LevelManager.instance.IsMainMenuActive) SetupUI();
    }

    void SetupUI()
    {
        _turnIcons[0].SetActive(true);
        _turnIcons[1].SetActive(false);

        for (int i = 0; i < _tictactoeSpaces.Length; i++)
        {
            _tttGridButtons.Add(Tuple.Create(_tictactoeSpaces[i].GetComponent<Button>(), _tictactoeSpaces[i].GetComponentInChildren<TextMeshProUGUI>()));
            _tttGridButtons[i].Item1.interactable = true;
            _tttGridButtons[i].Item2.text = "";
        }
    }

    public void WinnerDisplay(int whoseWin, int indexSolution)
    {
        _scores[whoseWin].text = "1";
        _winnerPanel.gameObject.SetActive(true);
        if(whoseWin == 0) _winnerText.text = "Player X Wins!";
        else if(whoseWin == 1) _winnerText.text = "Player O Wins!";

        _winnerLineFeedback[indexSolution].SetActive(true);
        foreach (var buttom in _tttGridButtons) if (buttom.Item1.interactable) buttom.Item1.interactable = false;
    }

    public void WinnerDisplay(bool draw)
    {
        if(draw)
        {
            _winnerPanel.gameObject.SetActive(true);
            _winnerText.text = "Draw! Play Again";
        }
    }

    public void UpdateUIButtonPressed(int witchNumber, string whoseTurn, int playerTurn)
    {
        _tttGridButtons[witchNumber].Item2.text = whoseTurn;
        _tttGridButtons[witchNumber].Item1.interactable = false;
        if (playerTurn == 0) 
        { 
            _turnIcons[0].SetActive(false);
            _turnIcons[1].SetActive(true);
        }
        else
        {
            _turnIcons[0].SetActive(true);
            _turnIcons[1].SetActive(false);
        }
    }
}
