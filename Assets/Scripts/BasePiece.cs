using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            case "blackPawn": this.GetComponent<SpriteRenderer>().sprite = BlackPawn; player = 'B'; break;
            case "blackKnight": this.GetComponent<SpriteRenderer>().sprite = BlackKnight; player = 'B'; break;
            case "blackBishop": this.GetComponent<SpriteRenderer>().sprite = BlackBishop; player = 'B'; break;
            case "blackRook": this.GetComponent<SpriteRenderer>().sprite = BlackRook; player = 'B'; break;
            case "blackQueen": this.GetComponent<SpriteRenderer>().sprite = BlackQueen; player = 'B'; break;
            case "blackKing": this.GetComponent<SpriteRenderer>().sprite = BlackKing; player = 'B'; break;

            case "whitePawn": this.GetComponent<SpriteRenderer>().sprite = WhitePawn; player = 'W'; break;
            case "whiteKnight": this.GetComponent<SpriteRenderer>().sprite = WhiteKnight; player = 'W'; break;
            case "whiteBishop": this.GetComponent<SpriteRenderer>().sprite = WhiteBishop; player = 'W'; break;
            case "whiteRook": this.GetComponent<SpriteRenderer>().sprite = WhiteRook; player = 'W'; break;
            case "whiteQueen": this.GetComponent<SpriteRenderer>().sprite = WhiteQueen; player = 'W'; break;
            case "whiteKing": this.GetComponent<SpriteRenderer>().sprite = WhiteKing; player = 'W'; break;
        }
    }

    public void SetCoords()
    {
        float x = xBoard;
        float y = yBoard;

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
        Scene scene = SceneManager.GetActiveScene();
        string sceneName = scene.name;
        if(scene.name == "Game")
        {
            if(!controller.GetComponent<Game>().IsGameOver() && controller.GetComponent<Game>().GetCurrentPlayer() == player)
            {
                DestroyMovePlates();

                InitiateMovePlates();
            }
        }
        else
        {
            if(!controller.GetComponent<PlayAgainstAI>().IsGameOver() && controller.GetComponent<PlayAgainstAI>().GetCurrentPlayer() == player)
            {
                DestroyMovePlates();

                InitiateMovePlates();
            }
        }
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
                PawnMovePlate(xBoard, yBoard -1);
                break;

             case "whitePawn":
                PawnMovePlate(xBoard, yBoard + 1);
                break;
        }
    }

    public void LineMovePlate(int xIncrement, int yIncrement)
    {
        Scene scene = SceneManager.GetActiveScene();
        string sceneName = scene.name;
        if (sceneName == "Game")
        {
            Game sc = controller.GetComponent<Game>();
            int x = xBoard + xIncrement;
            int y = yBoard + yIncrement;

            while (sc.positionOnBoard(x, y) && sc.getPosition(x, y) == null)
            {
                MovePlateSpawn(x, y);
                x += xIncrement;
                y += yIncrement;
            }

            if (sc.positionOnBoard(x, y) && sc.getPosition(x, y).GetComponent<BasePiece>().player != player)
            {
                MovePlateAttackSpawn(x, y);
            }
        }
        else
        {
            PlayAgainstAI sc = controller.GetComponent<PlayAgainstAI>();
            int x = xBoard + xIncrement;
            int y = yBoard + yIncrement;

            while (sc.positionOnBoard(x, y) && sc.getPosition(x, y) == null)
            {
                MovePlateSpawn(x, y);
                x += xIncrement;
                y += yIncrement;
            }

            if (sc.positionOnBoard(x, y) && sc.getPosition(x, y).GetComponent<BasePiece>().player != player)
            {
                MovePlateAttackSpawn(x, y);
            }
        }

        
    }

    public void LMovePlate()
    {
        PointMovePlate(xBoard + 1, yBoard + 2);
        PointMovePlate(xBoard - 1, yBoard + 2);
        PointMovePlate(xBoard + 2, yBoard + 1);
        PointMovePlate(xBoard + 2, yBoard - 1);
        PointMovePlate(xBoard + 1, yBoard - 2);
        PointMovePlate(xBoard - 1, yBoard - 2);
        PointMovePlate(xBoard - 2, yBoard + 1);
        PointMovePlate(xBoard - 2, yBoard - 1);
    }

    public void SurroundMovePlate()
    {
        PointMovePlate(xBoard, yBoard + 1);
        PointMovePlate(xBoard, yBoard - 1);
        PointMovePlate(xBoard - 1, yBoard + 0);
        PointMovePlate(xBoard - 1, yBoard - 1);
        PointMovePlate(xBoard - 1, yBoard + 1);
        PointMovePlate(xBoard + 1, yBoard + 0);
        PointMovePlate(xBoard + 1, yBoard - 1);
        PointMovePlate(xBoard + 1, yBoard + 1);
    }

    public void PointMovePlate(int x, int y)
    {
        Scene scene = SceneManager.GetActiveScene();
        string sceneName = scene.name;
        if (sceneName == "Game")
        {
            Game sc = controller.GetComponent<Game>();
            if (sc.positionOnBoard(x, y))
            {
                GameObject cp = sc.getPosition(x, y);

                if (cp == null)
                {
                    MovePlateSpawn(x, y);
                }
                else if (cp.GetComponent<BasePiece>().player != player)
                {
                    MovePlateAttackSpawn(x, y);
                }
            }
        }
        else
        {
            PlayAgainstAI sc = controller.GetComponent<PlayAgainstAI>();
            if (sc.positionOnBoard(x, y))
            {
                GameObject cp = sc.getPosition(x, y);

                if (cp == null)
                {
                    MovePlateSpawn(x, y);
                }
                else if (cp.GetComponent<BasePiece>().player != player)
                {
                    MovePlateAttackSpawn(x, y);
                }
            }
        }

    }

    public void PawnMovePlate(int x, int y)
    {
        Scene scene = SceneManager.GetActiveScene();
        string sceneName = scene.name;
        if (sceneName == "Game")
        {
            Game sc = controller.GetComponent<Game>();
            if (sc.positionOnBoard(x, y))
            {
                if (sc.getPosition(x, y) == null)
                {
                    MovePlateSpawn(x, y);
                }

                if (sc.getPosition(x + 1, y) && sc.getPosition(x + 1, y) != null && sc.getPosition(x + 1, y).GetComponent<BasePiece>().player != player)
                {
                    MovePlateAttackSpawn(x + 1, y);
                }

                if (sc.getPosition(x - 1, y) && sc.getPosition(x - 1, y) != null && sc.getPosition(x - 1, y).GetComponent<BasePiece>().player != player)
                {
                    MovePlateAttackSpawn(x - 1, y);
                }
            }
        }
        else
        {
            PlayAgainstAI sc = controller.GetComponent<PlayAgainstAI>();
            if (sc.positionOnBoard(x, y))
            {
                if (sc.getPosition(x, y) == null)
                {
                    MovePlateSpawn(x, y);
                }

                if (sc.getPosition(x + 1, y) && sc.getPosition(x + 1, y) != null && sc.getPosition(x + 1, y).GetComponent<BasePiece>().player != player)
                {
                    MovePlateAttackSpawn(x + 1, y);
                }

                if (sc.getPosition(x - 1, y) && sc.getPosition(x - 1, y) != null && sc.getPosition(x - 1, y).GetComponent<BasePiece>().player != player)
                {
                    MovePlateAttackSpawn(x - 1, y);
                }
            }
        }
    }

     public void MovePlateSpawn(int matrixX, int matrixY)
    {
        
        float x = matrixX;
        float y = matrixY;

        x *= 0.97f; 
        y *= 0.97f; 

        x += -3.45f;
        y += -3.6f;

        //Set actual unity values
        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }

 public void MovePlateAttackSpawn(int matrixX, int matrixY)
    {
        
        float x = matrixX;
        float y = matrixY;

        x *= 0.97f; 
        y *= 0.97f; 

        x += -3.45f;
        y += -3.6f;

        //Set actual unity values
        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.attack = true;
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }
}
