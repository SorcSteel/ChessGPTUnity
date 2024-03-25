using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePiece : MonoBehaviour
{
    public GameObject controller;
    public GameObject movePlate;

    private int xBoard = -1;
    private int yBoard = -1;

    private char player;

    public Sprite BlackPawn, BlackKnight, BlackBishop, BlackRook, BlackQueen, BlackKing; 
    public Sprite WhitePawn, WhiteKnight, WhiteBishop, WhiteRook, WhiteQueen, WhiteKing;

    public void Activate()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        // sets starting location
        SetCoords();

        switch (this.name)
        {
            case "blackPawn": this.GetComponent<SpriteRenderer>().sprite = BlackPawn; break;
            case "blackKnight": this.GetComponent<SpriteRenderer>().sprite = BlackKnight; break;
            case "blackBishop": this.GetComponent<SpriteRenderer>().sprite = BlackBishop; break;
            case "blackRook": this.GetComponent<SpriteRenderer>().sprite = BlackRook; break;
            case "blackQueen": this.GetComponent<SpriteRenderer>().sprite = BlackQueen; break;
            case "blackKing": this.GetComponent<SpriteRenderer>().sprite = BlackKing; break;

            case "whitePawn": this.GetComponent<SpriteRenderer>().sprite = WhitePawn; break;
            case "whiteKnight": this.GetComponent<SpriteRenderer>().sprite = WhiteKnight; break;
            case "whiteBishop": this.GetComponent<SpriteRenderer>().sprite = WhiteBishop; break;
            case "whiteRook": this.GetComponent<SpriteRenderer>().sprite = WhiteRook; break;
            case "whiteQueen": this.GetComponent<SpriteRenderer>().sprite = WhiteQueen; break;
            case "whiteKing": this.GetComponent<SpriteRenderer>().sprite = WhiteKing; break;
        }
    }

    public void SetCoords()
    {
        float x = xBoard;
        float y = yBoard;

        // will probably need to change these values
        x *= 0.97f; 
        y *= 0.97f; 

        x += -3.45f;
        y += -3.6f;

        this.transform.position = new Vector3(x,y, -1);
    }

    public int GetXboard(){ return xBoard; }
    public int GetYBoard(){ return yBoard; }
    public void SetXBoard(int x){ xBoard = x; }
    public void SetYBoard(int y){ yBoard = y; }

    public void OnMouseUp()
    {
        DestroyMovePlates();

        InitiateMovePlates();
    }

    public void DestroyMovePlates()
    {
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        for(int i = 0; i < movePlates.Length; i++)
        {
            Destroy(movePlates[i]);
        }
    }

    public void InitiateMovePlates()
    {
        switch (this.name)
        {
            case "whiteQueen":
            case "blackQueen":
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(1, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                LineMovePlate(-1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(1, -1);
                break;

            case "whiteKnight":
            case "blackKnight":
                LMovePlate();
                break;
            
            case "whiteBishop":
            case "blackBishop":
                LineMovePlate(1, 1);
                LineMovePlate(1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(-1, -1);
                break;
            
            case "whiteKing":
            case "blackKing":
                SurroundMovePlate();
                break;
            
            case "whiteRook":
            case "blackRook":
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                break;

            case "blackPawn":
                PawnMovePlate(xBoard, yBoard, -1);
                break;

             case "whitePawn":
                PawnMovePlate(xBoard, yBoard, -1);
                break;
        }
    }
}
