using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlate : MonoBehaviour
{
    public GameObject controller;
    GameObject reference = null;
    int matrixX;
    int matrixY;
    public bool attack = false;

    public void Start()
    {
        if(attack)
        {
            // change to red
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }
    }
    public void OnMouseUp()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        if(attack)
        {
            GameObject piece = controller.GetComponent<Game>().getPosition(matrixX, matrixY);

            if(piece.name == "whiteKing") controller.GetComponent<Game>().Winner('B');
            if(piece.name == "blackKing") controller.GetComponent<Game>().Winner('W');

            Destroy(piece); 
        }
        
        controller.GetComponent<Game>().setPositionEmpty(reference.GetComponent<BasePiece>().GetXboard(), reference.GetComponent<BasePiece>().GetYBoard());
        reference.GetComponent<BasePiece>().SetXBoard(matrixX);
        reference.GetComponent<BasePiece>().SetYBoard(matrixY);
        reference.GetComponent<BasePiece>().SetCoords();

        controller.GetComponent<Game>().setPosition(reference);

        controller.GetComponent<Game>().NextTurn();

        reference.GetComponent<BasePiece>().DestroyMovePlates();
    }

    public void SetCoords(int x, int y)
    {
        matrixX = x;
        matrixY = y;
    }

    public void SetReference(GameObject obj)
    {
        reference = obj;
    }

    public GameObject GetReference()
    {
        return reference;
    }
}
