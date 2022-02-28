using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardHighlight : MonoBehaviour
{

    public static BoardHighlight Instance { set; get; }
    public GameObject highlightPrefab;
    private List<GameObject> highlights;

    private void Start()
    {
        Instance = this;
        highlights = new List<GameObject>();
    }

    private GameObject GetHighlight(){
        GameObject var = highlights.Find(g => !g.activeSelf);

        if( var == null){
            var = Instantiate(highlightPrefab);
            highlights.Add(var);
        }
        return var;
    }

    public void HighlightAllowed(bool[,] moves){
        for(int i = 0; i < 8; i++){
            for(int j = 0; j < 8; j++){
                if(moves[i,j]){
                    GameObject var = GetHighlight();
                    var.SetActive (true);
                    var.transform.position = new Vector3(i+0.5f, 0, j+0.5f);
                }
            }
        }
    }

    public void HideHighlight(){
        foreach(GameObject var in highlights)
            var.SetActive(false);
    }
}
