using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    private GameObject[] squares = new GameObject[64];
    private Texture2D texB, texW;
    private Sprite mySprite_B, mySprite_W;

    // Start is called before the first frame update
    void Start()
    {
        texW = Resources.Load<Texture2D>("Sprites/White200x200");
        mySprite_W = Sprite.Create(texW, new Rect(0.0f, 0.0f, texW.width, texW.height), new Vector2(0.5f, 0.5f), 100.0f);
        texB = Resources.Load<Texture2D>("Sprites/Black200x200");
        mySprite_B = Sprite.Create(texB, new Rect(0.0f, 0.0f, texB.width, texB.height), new Vector2(0.5f, 0.5f), 100.0f);
        int j = -1;
        bool first = true, BxW = false;
        Vector3 start_pos = new Vector3(-7.39f, 7.34f, 0f);
        for (var i = 0; i < 64; i++)
        {
            squares[i] = new GameObject();
            squares[i].name = "Sq_" + i;
            j++;
            squares[i].AddComponent<Move>();
            squares[i].AddComponent<SpriteRenderer>();
            if(BxW == false)
            {                                
                squares[i].GetComponent<SpriteRenderer>().sprite = mySprite_W;
                BxW = true;
            }
            else
            {
                squares[i].GetComponent<SpriteRenderer>().sprite = mySprite_B;
                BxW = false;
            }
            if(j == 7)
            {
                if(BxW == false){
                    BxW = true;
                } else{
                    BxW = false;
                }               
            }

            if (j == 8)
            {
                start_pos.y = start_pos.y - 2f;
                start_pos.x = start_pos.x - (2f * 7f);
                squares[i].transform.position = new Vector3(start_pos.x, start_pos.y, 0f);                
                j = 0;                
            }
            else
            {
                if (first == false) 
                { 
                    start_pos.x = start_pos.x + 2f;
                }                
                squares[i].transform.position = new Vector3(start_pos.x, start_pos.y, 0f);
                first = false;
            }
            squares[i].AddComponent<BoxCollider2D>().size = new Vector2(2f, 2f);
            squares[i].GetComponent<BoxCollider2D>().isTrigger = true;
            //squares[i].AddComponent<MassEquivalenceScript>();
        }
    }
}
