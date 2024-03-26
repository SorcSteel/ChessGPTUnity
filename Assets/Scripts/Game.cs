using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public GameObject chesspiece;
    private GameObject[,] positions = new GameObject[8, 8];
    private GameObject[] playerBlack = new GameObject[16];
    private GameObject[] playerWhite = new GameObject[16];
    private char currentPlayer = 'W';
    private bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        playerWhite = new GameObject[]
        {
            Create("whiteRook", 0, 0),
            Create("whiteKnight", 1, 0),
            Create("whiteBishop", 2, 0),
            Create("whiteQueen", 3, 0),
            Create("whiteKing", 4, 0),
            Create("whiteBishop", 5, 0),
            Create("whiteKnight", 6, 0),
            Create("whiteRook", 7, 0),
            Create("whitePawn", 0, 1),
            Create("whitePawn", 1, 1),
            Create("whitePawn", 2, 1),
            Create("whitePawn", 3, 1),
            Create("whitePawn", 4, 1),
            Create("whitePawn", 5, 1),
            Create("whitePawn", 6, 1),
            Create("whitePawn", 7, 1),
        };

        playerBlack = new GameObject[]
        {
            Create("blackRook", 0, 7),
            Create("blackKnight", 1, 7),
            Create("blackBishop", 2, 7),
            Create("blackQueen", 3, 7),
            Create("blackKing", 4, 7),
            Create("blackBishop", 5, 7),
            Create("blackKnight", 6, 7),
            Create("blackRook", 7, 7),
            Create("blackPawn", 0, 6),
            Create("blackPawn", 1, 6),
            Create("blackPawn", 2, 6),
            Create("blackPawn", 3, 6),
            Create("blackPawn", 4, 6),
            Create("blackPawn", 5, 6),
            Create("blackPawn", 6, 6),
            Create("blackPawn", 7, 6),
        };

        for(int i =0; i< playerWhite.Length; i++)
        {
            setPosition(playerBlack[i]);
            setPosition(playerWhite[i]);
        }
    }

    public GameObject Create(string name, int x, int y)
    {
        GameObject obj = Instantiate(chesspiece, new Vector3(0,0,1), Quaternion.identity);
        BasePiece basePiece = obj.GetComponent<BasePiece>();
        basePiece.name = name;
        basePiece.SetXBoard(x);
        basePiece.SetYBoard(y);
        basePiece.Activate();
        return obj;
    }

    public void setPosition(GameObject obj)
    {
        BasePiece basePiece = obj.GetComponent<BasePiece>();

        positions[basePiece.GetXboard(), basePiece.GetYBoard()] = obj;
    }

    public void setPositionEmpty(int x, int y)
    {
        positions[x, y] = null;
    }

    public GameObject getPosition(int x, int y)
    {
        return positions[x, y];
    }

    public bool positionOnBoard(int x, int y)
    {
        if(x < 0 || y < 0 || x >= positions.GetLength(0) || y>= positions.GetLength(1))
        {
            return false;
        }
        return true;
    }

    public char GetCurrentPlayer()
    {
        return currentPlayer;
    }

    public bool IsGameOver()
    {
        return gameOver;
    }

    public void NextTurn()
    {
        if(currentPlayer == 'W')
        {
            currentPlayer = 'B';
        }
        else
        {
            currentPlayer = 'W';
        }
    }

    public void Update()
    {
        if(gameOver && Input.GetMouseButtonDown(0))
        {
            gameOver = false;

            SceneManager.LoadScene("Game");
        }
    }

    public void Winner(char playerWinner)
    {
        string winner = "";
        if(playerWinner == 'W') winner = "White";
        if(playerWinner == 'B') winner = "Black";

        gameOver = true;

        GameObject.FindGameObjectWithTag("WinnerText").GetComponent<Text>().enabled = true;
        GameObject.FindGameObjectWithTag("WinnerText").GetComponent<Text>().text = winner + " is the winner";

        GameObject.FindGameObjectWithTag("RestartText").GetComponent<Text>().enabled = true;

    }
}
