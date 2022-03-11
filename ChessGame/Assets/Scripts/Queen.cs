using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : ChessP
{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];
        ChessP c;
        int i,j;

        //right
        i = CurrentX;
        while (true)
        {
            i++;
            if (i >= 8)
                break;
            c = BoardManager.Instance.ChessPs[i, CurrentY];
            if (c == null)
                r[i, CurrentY] = true;
            else
            {
                if (c.isWhite != isWhite)
                    r[i, CurrentY] = true;
                break;
            }
        }

        //left
        i = CurrentX;
        while (true)
        {
            i--;
            if (i < 0)
                break;
            c = BoardManager.Instance.ChessPs[i, CurrentY];
            if (c == null)
                r[i, CurrentY] = true;
            else
            {
                if (c.isWhite != isWhite)
                    r[i, CurrentY] = true;
                break;
            }
        }

        //up
        i = CurrentY;
        while (true)
        {
            i++;
            if (i >= 8)
                break;
            c = BoardManager.Instance.ChessPs[CurrentX, i];
            if (c == null)
                r[CurrentX, i] = true;
            else
            {
                if (c.isWhite != isWhite)
                    r[CurrentX, i] = true;
                break;
            }
        }

        //down
        i = CurrentY;
        while (true)
        {
            i--;
            if (i < 0)
                break;
            c = BoardManager.Instance.ChessPs[CurrentX, i];
            if (c == null)
                r[CurrentX, i] = true;
            else
            {
                if (c.isWhite != isWhite)
                    r[CurrentX, i] = true;
                break;
            }
        }
        
        //TopLeft
        i = CurrentX;
        j = CurrentY;
        while(true){
            i--;
            j++;
            if(i < 0 || j >= 8)
                break;
            c = BoardManager.Instance.ChessPs[i,j];
            if(c == null)
                r [i,j] = true;
            else{
                if(isWhite != c.isWhite)
                    r [i,j] = true;
                break;
            }
        }

        //TopRight
        i = CurrentX;
        j = CurrentY;
        while(true){
            i++;
            j++;
            if(i >= 8 || j >= 8)
                break;
            c = BoardManager.Instance.ChessPs[i,j];
            if(c == null)
                r [i,j] = true;
            else{
                if(isWhite != c.isWhite)
                    r [i,j] = true;
                break;
            }
        }

        //DownLeft
        i = CurrentX;
        j = CurrentY;
        while(true){
            i--;
            j--;
            if(i < 0 || j < 0)
                break;
            c = BoardManager.Instance.ChessPs[i,j];
            if(c == null)
                r [i,j] = true;
            else{
                if(isWhite != c.isWhite)
                    r [i,j] = true;
                break;
            }
        }

        //Down Right
        i = CurrentX;
        j = CurrentY;
        while(true){
            i++;
            j--;
            if(i >= 8 || j < 0)
                break;
            c = BoardManager.Instance.ChessPs[i,j];
            if(c == null)
                r [i,j] = true;
            else{
                if(isWhite != c.isWhite)
                    r [i,j] = true;
                break;
            }
        }
        
        
        return r;
    }
}
