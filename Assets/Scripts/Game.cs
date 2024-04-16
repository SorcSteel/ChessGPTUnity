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

    private SignalRConnection signalRConnection;

    // Start is called before the first frame update
    void Start()
    {
        signalRConnection = new SignalRConnection();
        signalRConnection.OnMessageReceived += HandleOpponentMove;

        SetupChessboard();
    }

    public void SetupChessboard()
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
        signalRConnection.Initialize();
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

    public async void NextTurn()
    {
            // Create an instance of SignalRConnection
            SignalRConnection signalRConnection = new SignalRConnection();

            // Call Initialize to set up the connection (assuming this needs to be done before sending a message)
            signalRConnection.Initialize();

            // Await the SendMessage call
            await signalRConnection.SendMessage(GetFEN());
        if(currentPlayer == 'W')
        {
            currentPlayer = 'B';
        }
        else
        {
            currentPlayer = 'W';
        }
    }

    void HandleOpponentMove(string fen)
{
    // Parse the FEN string to update the game state
    ParseFEN(fen);

    // Switch player turn after handling the opponent's move
}

void ParseFEN(string fen)
{
    // Split the FEN string to extract the board configuration and current player
    string[] parts = fen.Split(' ');
    string boardConfig = parts[0];
    char player = parts[1][0];

    // Reset the board positions
    for (int xIterator = 0; xIterator < 8; xIterator++)
    {
        for (int yIterator = 0; yIterator < 8; yIterator++)
        {
            positions[xIterator, yIterator] = null;
        }
    }

    // Parse the board configuration to set the positions of the chess pieces
    int x = 0, y = 7; // Start at the bottom-left corner of the board
    foreach (char c in boardConfig)
    {
        if (char.IsDigit(c))
        {
            // Empty squares
            int emptyCount = int.Parse(c.ToString());
            x += emptyCount;
        }
        else if (c == '/')
        {
            // Move to the next row
            x = 0;
            y--;
        }
        else
        {
            // Chess piece
            GameObject piece = CreatePieceFromFEN(c, x, y);
            setPosition(piece);
            x++;
        }
    }
}

GameObject CreatePieceFromFEN(char fenChar, int x, int y)
{
    // Determine the piece color based on the case of the FEN character
    bool isWhite = char.IsUpper(fenChar);

    // Convert the FEN character to the piece name
    string pieceName = "";
    switch (char.ToLower(fenChar))
    {
        case 'p':
            pieceName = isWhite ? "whitePawn" : "blackPawn";
            break;
        case 'r':
            pieceName = isWhite ? "whiteRook" : "blackRook";
            break;
        case 'n':
            pieceName = isWhite ? "whiteKnight" : "blackKnight";
            break;
        case 'b':
            pieceName = isWhite ? "whiteBishop" : "blackBishop";
            break;
        case 'q':
            pieceName = isWhite ? "whiteQueen" : "blackQueen";
            break;
        case 'k':
            pieceName = isWhite ? "whiteKing" : "blackKing";
            break;
    }

    // Create and return the corresponding GameObject
    return Create(pieceName, x, y);
}

    public string GetFEN()
{
    string fen = "";
    int emptySquareCount = 0;

    for (int y = 7; y >= 0; y--)
    {
        for (int x = 0; x < 8; x++)
        {
            GameObject piece = getPosition(x, y);
            if (piece != null)
            {
                char pieceFEN = GetFENRepresentation(piece.GetComponent<BasePiece>().name);
                if (emptySquareCount > 0)
                {
                    fen += emptySquareCount.ToString();
                    emptySquareCount = 0;
                }
                fen += pieceFEN;
            }
            else
            {
                emptySquareCount++;
            }
        }

        if (emptySquareCount > 0)
        {
            fen += emptySquareCount.ToString();
            emptySquareCount = 0;
        }

        if (y > 0)
        {
            fen += "/";
        }
    }

    fen += " ";
    fen += currentPlayer == 'W' ? "w" : "b";

    return fen;
}

private char GetFENRepresentation(string pieceName)
{
    switch (pieceName)
    {
        case "whitePawn":
            return 'P';
        case "whiteRook":
            return 'R';
        case "whiteKnight":
            return 'N';
        case "whiteBishop":
            return 'B';
        case "whiteQueen":
            return 'Q';
        case "whiteKing":
            return 'K';
        case "blackPawn":
            return 'p';
        case "blackRook":
            return 'r';
        case "blackKnight":
            return 'n';
        case "blackBishop":
            return 'b';
        case "blackQueen":
            return 'q';
        case "blackKing":
            return 'k';
        default:
            return ' ';
    }
}

    public void Update()
    {
        if(gameOver && Input.GetMouseButtonDown(0))
        {
            gameOver = false;

            SceneManager.LoadScene(1);
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
