using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Range(0, 1)]
    private int _whoseTurn; // 0 = x and 1 = o
    private int _turnCount;
    [SerializeField]
    private string[] _playIcons = { "X", "O" };
    [SerializeField]
    private int[] _markerdSpaces;

    private void Start()
    {
        if (!LevelManager.instance.IsMainMenuActive) SetupGame();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) LevelManager.instance.LoadScene("MainMenu");
    }

    void SetupGame()
    {
        _whoseTurn = 0;
        _turnCount = 0;
        
        for (int i = 0; i < _markerdSpaces.Length; i++)
        {
            _markerdSpaces[i] = -100;
        }
    }

    public void TictactoeButton(int witchNumber)
    {
        UIManager.instance.UpdateUIButtonPressed(witchNumber, _playIcons[_whoseTurn], _whoseTurn);

        _markerdSpaces[witchNumber] = _whoseTurn + 1;
        _turnCount++;

        if(_turnCount > 4) if(!WinnerCheck() && _turnCount >= 9) UIManager.instance.WinnerDisplay(true);
        //if(_turnCount >= 9) UIManager.instance.WinnerDisplay(true);

        if (_whoseTurn == 0) _whoseTurn = 1;
        else _whoseTurn = 0;
    }

    bool WinnerCheck()
    {
        int s1 = _markerdSpaces[0] + _markerdSpaces[1] + _markerdSpaces[2];
        int s2 = _markerdSpaces[3] + _markerdSpaces[4] + _markerdSpaces[5];
        int s3 = _markerdSpaces[6] + _markerdSpaces[7] + _markerdSpaces[8];
        int s4 = _markerdSpaces[0] + _markerdSpaces[3] + _markerdSpaces[6];
        int s5 = _markerdSpaces[1] + _markerdSpaces[4] + _markerdSpaces[7];
        int s6 = _markerdSpaces[2] + _markerdSpaces[5] + _markerdSpaces[8];
        int s7 = _markerdSpaces[0] + _markerdSpaces[4] + _markerdSpaces[8];
        int s8 = _markerdSpaces[2] + _markerdSpaces[4] + _markerdSpaces[6];
        var solutions = new int[] { s1, s2, s3, s4, s5, s6, s7, s8 }; //Values switching in 3 or 6
        for (int i = 0; i < solutions.Length; i++)
        {
            if (solutions[i] == 3 * (_whoseTurn + 1))
            {
                Debug.Log($"Player {_whoseTurn} has won");
                UIManager.instance.WinnerDisplay(_whoseTurn, i);
                return true;
            }
        }
        return false;
    }
}
