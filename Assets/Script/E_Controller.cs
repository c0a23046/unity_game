using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Controller : MonoBehaviour
{
    float Timer;
    public float ChangeTime;
    public float EnemySpeed;

    GameObject Target;
    void Start()
    {
        //Destroy(this.gameObject, time);
    }

    void Update()
    {
        var speed = Vector3.zero;
        speed.z = EnemySpeed;
        var rot = transform.eulerAngles; //敵の視覚方向

        if(Target) //プレイヤーを見つけた時
        {
            transform.LookAt(Target.transform);  //プレイヤーの方向を見る
            rot = transform.eulerAngles;
        }
        else //プレイヤーを見つけていない時         
        {
            Timer += Time.deltaTime;
            if(ChangeTime <= Timer)
            {
                float rand = Random.Range(0, 360);  //向く方向を変える
                rot.y = rand;
                Timer = 0;
            }

        }

        
        rot.x = 0;
        rot.z = 0;
        transform.eulerAngles = rot;

        this.transform.Translate(speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Target = other.gameObject;
        } 
    }

    void OnTriggerExit(Collider other) 
    {
        if(other.tag == "Player")
        {
            Target = null;
        }   
    }
}


