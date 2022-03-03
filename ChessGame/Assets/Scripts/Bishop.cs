using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : ChessP
{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];
        ChessP c;
        //White team move
        if (isWhite)
        {
            //Diagonal Left
            if (CurrentX != 0)
            {
                c = BoardManager.Instance.ChessPs[CurrentX - 1, CurrentY + 1];
                if (c != null)
                    r[CurrentX - 1, CurrentY + 1] = true;
            }
            //Diagonal Right
            if (CurrentX != 7 && CurrentY != 7)
            {
                c = BoardManager.Instance.ChessPs[CurrentX + 1, CurrentY + 1];
                if (c != null)
                    r[CurrentX + 1, CurrentY + 1] = true;
            }
        }
        else //black team move
        {
            //Diagonal Left
            if (CurrentX != 0)
            {
                c = BoardManager.Instance.ChessPs[CurrentX - 1, CurrentY -1];
                if (c != null && c.isWhite)
                    r[CurrentX - 1, CurrentY - 1] = true;
            }
            //Diagonal Right
            if (CurrentX != 7 && CurrentY != 0)
            {
                c = BoardManager.Instance.ChessPs[CurrentX + 1, CurrentY - 1];
                if (c != null && c.isWhite)
                    r[CurrentX + 1, CurrentY - 1] = true;
            }

        }
        return r;
    }
}
