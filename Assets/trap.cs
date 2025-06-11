using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class trap : MonoBehaviour
{
    Vector2 Start_Position;//初期位置
    Vector2 End_Position;//移動後の位置
    Vector3 move_vec;
    bool move = false;
    float speed = 0.5f;
    int move_width = 10;

    void Start()
    {
        Start_Position = transform.position;
        End_Position = new Vector2(Start_Position.x - move_width, Start_Position.y);
        move_vec = new Vector2(speed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (move && transform.position.x > End_Position.x)//フラグが立っている＆移動が完了していない
        {
            transform.position += move_vec*Time.deltaTime;
        }
        else if(transform.position.x <= End_Position.x)//移動が完了した
        {

        }
        if (!move && transform.position.x < Start_Position.x)//フラグが立っていない＆初期位置に戻っていない
        {
            transform.position += move_vec * Time.deltaTime;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            move = true;
            Debug.Log("trap start!!");
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            move = false;
        }
    }
}
