using System.Collections.Generic;
using UnityEngine;
using Unity;

public class BoradManager : MonoBehaviour
{
    private const float TileSize = 1.0f;
    private const float TileOffset = 0.5f;

    private int selectionX = -1;
    private int selectionY = -1;

    public List<GameObject> chessP;
    private List<GameObject> activeChessP;

    private Quaternion orientation = Quaternion.Euler(0, 180, 0);

    private void Update()
    {
        UpdateSelection();
        DrawChessBoard();
    }
    private void Start()
    {
        SpawnAllP();
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

    private void SpawnPiece(int index, Vector3 position)
    {
        GameObject hg = Instantiate(chessP[index], position, orientation) as GameObject;
        hg.transform.SetParent(transform);
        activeChessP.Add(hg);
    }

    private void SpawnAllP()
    {
        activeChessP = new List<GameObject>();
        //Spawn white team
        SpawnPiece(0, GetTileCentre(3, 0));
        SpawnPiece(1, GetTileCentre(4, 0));
        SpawnPiece(2, GetTileCentre(5, 0));
        SpawnPiece(2, GetTileCentre(2, 0));
        SpawnPiece(3, GetTileCentre(6, 0));
        SpawnPiece(3, GetTileCentre(1, 0));
        SpawnPiece(4, GetTileCentre(0, 0));
        SpawnPiece(4, GetTileCentre(7, 0));
        for (int i = 0; i < 8; i++)
        {
            SpawnPiece(5, GetTileCentre(i, 1));
        }
        //spawn black team
        SpawnPiece(6, GetTileCentre(4, 7));
        SpawnPiece(7, GetTileCentre(3, 7));
        SpawnPiece(8, GetTileCentre(5, 7));
        SpawnPiece(8, GetTileCentre(2, 7));
        SpawnPiece(9, GetTileCentre(6, 7));
        SpawnPiece(9, GetTileCentre(1, 7));
        SpawnPiece(10, GetTileCentre(0, 7));
        SpawnPiece(10, GetTileCentre(7, 7));
        for (int i = 0; i < 8; i++)
        {
            SpawnPiece(11, GetTileCentre(i, 6));
        }
    }

    private Vector3 GetTileCentre(int x, int y)
    {
        Vector3 origin = Vector3.zero;
        origin.x += (TileSize * x) + TileOffset;
        origin.z += (TileSize * y) + TileOffset;

        return origin;
    }
}
