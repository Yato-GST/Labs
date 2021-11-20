using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public GameObject[] pawns;
    public Pawn  pawnsript;
    Pawn p1 = new Pawn();

    private void Start() {
        if (pawns == null)
            pawns = GameObject.FindGameObjectsWithTag("pawns");
    }
    private void OnMouseUp() {
        foreach (GameObject pawn in pawns)
        {
            if(p1.setF == 1){
                pawn.transform.position = this.transform.position;
            }
        }        
    }
}