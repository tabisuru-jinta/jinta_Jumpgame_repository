using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    GameObject MainCamera;
    GameObject Player;
    Rigidbody2D rb;
    Vector2 CurrentVelocity; //速度を格納
    public const float Slide_Force = 3.0f;//移動する力
    public const float Jump_Speed = 10f;//ジャンプする速さ
    const double MaxVelocity = 7; //速度の上限
    public bool IsGrounded = false; //着地フラグ
    const int Camera_position_y = 5;
    const int Reset_Height = -5; //落下判定する高さ
    Vector2 Start_Position = new Vector2(0, 0); //初期位置

    void Start()
    {
        MainCamera = GameObject.Find("Main Camera");
        Player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;

        transform.position = Start_Position; //playerを初期位置にリセット

        Debug.Log("game start!!");
    }

    
    
    
    // Update is called once per frame
    void Update()
    {
        if(transform.position.y > Camera_position_y) //playerが一定以上高い位置にいたらカメラはy軸も追従
        {
            MainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -50);
        }
        else //それ以外はx,yともに追従
        {
            MainCamera.transform.position = new Vector3(transform.position.x, Camera_position_y, -50);
        }

            CurrentVelocity = rb.velocity;

        if (Input.GetKey(KeyCode.D) && CurrentVelocity.x < MaxVelocity)
        {
            if(IsGrounded)
            {
                rb.AddForce(new Vector2(Slide_Force, 0), ForceMode2D.Force);
            }
            else
            {
                rb.AddForce(new Vector2(Slide_Force*0.1f, 0), ForceMode2D.Force);
            }
            
        }
        if (Input.GetKey(KeyCode.A) && CurrentVelocity.x > -MaxVelocity)
        {
            if (IsGrounded)
            {
                rb.AddForce(new Vector2(-Slide_Force, 0), ForceMode2D.Force);
            }
            else
            {
                rb.AddForce(new Vector2(-Slide_Force * 0.1f, 0), ForceMode2D.Force);
            }

        }
        if (Input.GetKey(KeyCode.Space) && IsGrounded)
        {
            //Debug.Log("jump");
            rb.velocity = new Vector2(CurrentVelocity.x, Jump_Speed);
            

        }

        if(transform.position.y < Reset_Height) //落ちた時、初期位置へ戻す
        {
            transform.position = Start_Position;
            Debug.Log("落ちた！！");
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            
            //Debug.Log("壁にぶつかった！！");
            foreach(ContactPoint2D contact in collision.contacts)
            {
                Vector2 normal = contact.normal;
                if (normal == Vector2.up) IsGrounded = true;
                //if (normal == Vector2.down) Debug.Log("上から");
            }
        }
        else
        {
            //Debug.Log("何かにぶつかった！！");
        }

        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        IsGrounded = false; ;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Goal")
        {
            Debug.Log("クリアl!!!");
            Camera.main.backgroundColor = new Color(0.7f,0.35f,0.35f);
        }
    }
}
