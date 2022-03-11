using System.Collections.Generic;
using UnityEngine;
using Unity;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance { set; get; }
    public bool[,] allowedMove { set; get; }
    public ChessP[,] ChessPs { set; get; }
    private ChessP selectedChessP;
    private const float TileSize = 1.0f;
    private const float TileOffset = 0.5f;
    bool canMove = false;

    private int selectionX = -1;
    private int selectionY = -1;

    public List<GameObject> chessP;
    private List<GameObject> activeChessP;

    private Quaternion orientation = Quaternion.Euler(0, 90, 0);
    public bool isWhiteTurn = true;
    private void Start()
    {
        Instance = this;
        SpawnAllP();
    }
    private void Update()
    {
        UpdateSelection();
        DrawChessBoard();

        if (Input.GetMouseButtonDown(0))
        {
            if (selectionX >= 0 && selectionY >= 0)
            {
                if (selectedChessP == null)
                {
                    SelectChessP(selectionX, selectionY);
                }
                else
                {
                    MoveChessP(selectionX, selectionY);
                }
            }
        }
    }

    private void SelectChessP(int x, int y)
    {
        if (ChessPs[x, y] == null)
            return;

        if (ChessPs[x, y].isWhite != isWhiteTurn)
            return;
    
        allowedMove = ChessPs[x, y].PossibleMove();
        for (int i = 0; i < 8; i++)
            for (int j = 0; j < 8; j++)
                if (allowedMove[i, j])
                    canMove = true;

        selectedChessP = ChessPs[x, y];
        BoardHighlight.Instance.HighlightAllowed(allowedMove);
    }
    private void MoveChessP(int x, int y)
    {
        if (allowedMove[x, y] == true)
        {
            ChessP c = ChessPs[x, y];
            if (c != null && c.isWhite != isWhiteTurn)
            {
                //kill piece
                activeChessP.Remove(c.gameObject);
                Destroy(c.gameObject);

                //if kill king end game
                if (c.GetType() == typeof(King))
                {
                    EndGame();
                    return;
                }
            }
            ChessPs[selectedChessP.CurrentX, selectedChessP.CurrentY] = null;
            selectedChessP.transform.position = GetTileCentre(x, y);
            selectedChessP.SetPosition(x, y);
            ChessPs[x, y] = selectedChessP;
            isWhiteTurn = !isWhiteTurn;
        }
        BoardHighlight.Instance.HideHighlight();
        selectedChessP = null;
    }

    private void UpdateSelection()
    {
        if (!Camera.main)
            return;
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 20.0f, LayerMask.GetMask("ChessPlane")))
        {
            selectionX = (int)hit.point.x;
            selectionY = (int)hit.point.z;
        }
        else
        {
            selectionX = -1;
            selectionY = -1;
        }
    }
    private void DrawChessBoard()
    {
        Vector3 widthLine = Vector3.right * 8;
        Vector3 heightLine = Vector3.forward * 8;

        for (int i = 0; i <= 8; i++)
        {
            Vector3 boardLw = Vector3.forward * i;
            Debug.DrawLine(boardLw, boardLw + widthLine);
            for (int j = 0; j <= 8; j++)
            {
                Vector3 boardLh = Vector3.right * j;
                Debug.DrawLine(boardLh, boardLh + heightLine);
            }
        }
        //draw selection
        if (selectionX >= 0 && selectionY >= 0)
        {
            Debug.DrawLine(Vector3.forward * selectionY + Vector3.right * selectionX,
                           Vector3.forward * (selectionY + 1) + Vector3.right * (selectionX + 1));
            Debug.DrawLine(Vector3.forward * (selectionY + 1) + Vector3.right * selectionX,
                           Vector3.forward * selectionY + Vector3.right * (selectionX + 1));
        }
    }

    private void SpawnPiece(int index, int x, int y)
    {
        GameObject hg = Instantiate(chessP[index], GetTileCentre(x, y), orientation) as GameObject;
        hg.transform.SetParent(transform);
        ChessPs[x, y] = hg.GetComponent<ChessP>();
        ChessPs[x, y].SetPosition(x, y);
        activeChessP.Add(hg);
    }

    private void SpawnAllP()
    {
        activeChessP = new List<GameObject>();
        ChessPs = new ChessP[8, 8];
        //Spawn white team
        SpawnPiece(0, 4, 0);
        SpawnPiece(1, 3, 0);
        SpawnPiece(2, 5, 0);
        SpawnPiece(2, 2, 0);
        SpawnPiece(3, 6, 0);
        SpawnPiece(3, 1, 0);
        SpawnPiece(4, 0, 0);
        SpawnPiece(4, 7, 0);
        for (int i = 0; i < 8; i++)
        {
            SpawnPiece(5, i, 1);
        }
        //spawn black team
        SpawnPiece(6, 4, 7);
        SpawnPiece(7, 3, 7);
        SpawnPiece(8, 5, 7);
        SpawnPiece(8, 2, 7);
        SpawnPiece(9, 6, 7);
        SpawnPiece(9, 1, 7);
        SpawnPiece(10, 0, 7);
        SpawnPiece(10, 7, 7);
        for (int i = 0; i < 8; i++)
        {
            SpawnPiece(11, i, 6);
        }
    }

    private Vector3 GetTileCentre(int x, int y)
    {
        Vector3 origin = Vector3.zero;
        origin.x += (TileSize * x) + TileOffset;
        origin.z += (TileSize * y) + TileOffset;

        return origin;
    }
    private void EndGame()
    {
        if (isWhiteTurn)
            Debug.Log("White Team wins!");
        else
            Debug.Log("Black Team wins!");
        foreach (GameObject hg in activeChessP)
            Destroy(hg);
        isWhiteTurn = true;
        BoardHighlight.Instance.HideHighlight();
        SpawnAllP();
    }
}
