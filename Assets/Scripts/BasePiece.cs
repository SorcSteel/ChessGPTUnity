using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject controller;
    public GameObject mavePlate;

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
            case "BlackPawn": this.GetComponent<SpriteRenderer>().sprite = BlackPawn; break;
            case "BlackKnight": this.GetComponent<SpriteRenderer>().sprite = BlackKnight; break;
            case "BlackBishop": this.GetComponent<SpriteRenderer>().sprite = BlackBishop; break;
            case "BlackRook": this.GetComponent<SpriteRenderer>().sprite = BlackRook; break;
            case "BlackQueen": this.GetComponent<SpriteRenderer>().sprite = BlackQueen; break;
            case "BlackKing": this.GetComponent<SpriteRenderer>().sprite = BlackKing; break;

            case "WhitePawn": this.GetComponent<SpriteRenderer>().sprite = WhitePawn; break;
            case "WhiteKnight": this.GetComponent<SpriteRenderer>().sprite = WhiteKnight; break;
            case "WhiteBishop": this.GetComponent<SpriteRenderer>().sprite = WhiteBishop; break;
            case "WhiteRook": this.GetComponent<SpriteRenderer>().sprite = WhiteRook; break;
            case "WhiteQueen": this.GetComponent<SpriteRenderer>().sprite = WhiteQueen; break;
            case "WhiteKing": this.GetComponent<SpriteRenderer>().sprite = WhiteKing; break;
        }
    }

    public void SetCoords()
    {
        float x = xBoard;
        float y = yBoard;

        // will probably need to change these values
        x *= 0.66f; 
        y *= 0.66f; 

        x += -2.3f;
        y += - 2.3f;

        this.transform.position = new Vector3(x,y, 1);
    }
}
