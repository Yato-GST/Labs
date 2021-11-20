using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    public int setF = 0;

    private void OnMouseDown() {
        setF = 1;
    }
    private void OnMouseUp() {
        if(setF == 1){

        }
    }
    private void OnTriggerStay2D(Collider2D other) {

    }
}
