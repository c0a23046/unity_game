using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Controller : MonoBehaviour
{
    float Timer;
    public float ChangeTime; //ランダムに向きを変えるまでの時間
    public float EnemySpeed; //敵の動くスピード

    GameObject Target; //プレイヤーのgameObjectを保持する変数
    void Start()
    {
        //Destroy(this.gameObject, time);
    }

    void Update()
    {
        var speed = Vector3.zero;
        speed.z = EnemySpeed; //敵の進行方向をz方向に設定。その方向にEnemySpeedで動く
        var rot = transform.eulerAngles; //敵の視覚(向き）方向

        if(Target) //プレイヤーを見つけた時
        {
            transform.LookAt(Target.transform);  //プレイヤーの方向を見る
            rot = transform.eulerAngles; //敵の向く方向を更新
        }
        else //プレイヤーを見つけていない時         
        {
            Timer += Time.deltaTime; //Timerを進めて
            if(ChangeTime <= Timer) //もしchangetimeを超えたら
            {
                float rand = Random.Range(0, 360);  //向く方向をランダムに変える
                rot.y = rand;
                Timer = 0; //タイマーリセット
            }

        }

        //x軸,ｚ軸の回転は０にして傾いたり変な挙動をしないように調整
        rot.x = 0;
        rot.z = 0;
        transform.eulerAngles = rot;

        this.transform.Translate(speed); //敵をｚ方向にEnemySpeedの速度で進める
    }

    void OnTriggerEnter(Collider other)
    {
        //敵のトリガーにプレイヤーが入るとtargetにプレイヤーセット
        if (other.tag == "Player")
        {
            Target = other.gameObject;
        } 
    }

    void OnTriggerExit(Collider other) 
    {
        //プレイヤーが敵のトリガーの範囲から離れたらTargetをnullにし、見失った状態に戻る。
        if (other.tag == "Player")
        {
            Target = null;
        }   
    }
}


