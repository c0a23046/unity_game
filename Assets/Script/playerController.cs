using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    //プレイヤーが操作するカメラのTransform。プレイヤーの視点を決定する。
    public Transform Camera;
    //PlayerSpeed：プレイヤーの移動速度を制御するための変数。
    public float PlayerSpeed;
    //RotationSpeed：プレイヤーの回転速度を制御するための変数。
    public float RotationSpeed;
    //speed Vectr3.zero : (0, 0, 0)を意味する。ベクトルの初期化
    Vector3 speed = Vector3.zero;
    //rot Vector3.zero : (0, 0, 0)を意味する。回転に関するベクトル
    Vector3 rot = Vector3.zero;
    //プレイヤーアニメーションの制御
    public Animator PlayerAnimator;
    bool isRun;
    public Collider WeaponCollider;

    bool canMove = true;
    public AudioSource audioSource;

    //攻撃時にhit音
    public AudioClip AttackSE;

    //プレイヤーの範囲内に入った敵のリスト作成
    public EnemyListManager enemy_List_Manager;

    //ターゲットとなる敵の変数
    public Transform Target;

    //ターゲット選択の変数
    int TargetCount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move(); //プレイヤーを移動させる処理
        Rotation(); //プレイヤーカメラの回転
        Attack(); //攻撃関係

        TargetLook(); //ターゲットを選んでカメラ方向の操作
        Camera.transform.position = transform.position; //カメラをプレイヤーの位置に追従
    }

    void Move()
    {   
        if(canMove == false)
        {
            return;
        }

        speed = Vector3.zero;
        rot = Vector3.zero;
        isRun = false;

        //プレイヤーの操作　上下左右
        if(Input.GetKey(KeyCode.W))
        {
            rot.y = 0;
            MoveSet();
        }

        if(Input.GetKey(KeyCode.S))
        {
            rot.y = 180;
            MoveSet();
        }

        if(Input.GetKey(KeyCode.D))
        {
            rot.y = 90;
            MoveSet();
        }

        if(Input.GetKey(KeyCode.A))
        {
            rot.y = -90;
            MoveSet();
        }
        
        transform.Translate(speed);
        PlayerAnimator.SetBool("run", isRun); //移動の際に走るアニメーションの状態を"run"にする
    }

    void MoveSet()
    {
        //z方向への移動速度を設定、speedはvector3にしているのでspeed.zつまりvectorのz成分にPlayerspeedを設定
        speed.z = PlayerSpeed;

        //プレイヤーの向きをカメラの向き + 入力方向に回転。Camera.transform.eulerAngles は現在のカメラの回転角（ベクトル
        //rot は入力キー（WASD）に応じて設定されたY軸の回転角
        transform.eulerAngles = Camera.transform.eulerAngles + rot;
        isRun = true; //isRun=trueにし、走るアニメーションにする
    }

    void Rotation()
    {
        //(0, 0, 0) の3次元ベクトルを speed に代入（ローカル変数）
        var speed = Vector3.zero;

        //←が押されたら左回転するための角度（負のY値）を speed.y に代入
        if(Input.GetKey(KeyCode.LeftArrow)) 
        {
            speed.y = -RotationSpeed;
        }

        //→キーが押されている場合、右回転するための角度（正のY値）を speed.y に代入
        if(Input.GetKey(KeyCode.RightArrow))
        {
            speed.y = RotationSpeed;
        }

        //プレイヤーのカメラの角度に上で取得したspeedを加算＝プレイヤーカメラの角度が変わる
        Camera.transform.eulerAngles += speed;
    }

    void Attack()
    {
        //スペースキー押されたとき攻撃アニメーション"Attack"をtrueにして動作
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            PlayerAnimator.SetBool("Attack", true);
            //canMoveをfalseにして攻撃中は移動できないようにする
            canMove = false;
            //設定されたAttackSEを一度だけ再生
            audioSource.PlayOneShot(AttackSE);
        }
    }


    //WeaponON() と WeaponOFF() メソッドは、プレイヤーが攻撃する際の武器（Weapon）の当たり判定（Collider）
    //を有効・無効に切り替えるためのメソッド
    void WeaponON()
    {
        //WeaponCollider は、プレイヤーの武器（例：剣）の Collideが
        //trueでOnTriggerEnter()などのイベントが発生
        WeaponCollider.enabled = true;
    }

    void WeaponOFF()
    {
        //プレイヤーが武器を振っていないときは攻撃判定が発生しない
        WeaponCollider.enabled = false;
        //アニメーターの「Attack」フラグを false に戻すことで、攻撃アニメーションから通常状態に戻るトリガー
        PlayerAnimator.SetBool("Attack", false);
    }

    void CanMove()
    {
        canMove = true;
    }

    void TargetLook()
    {
        //ターゲットをセットする
        if(Input.GetKeyDown(KeyCode.E))
        {
            //リストが０なら止める
            if(enemy_List_Manager.EnemyList.Count == 0)
            {
                return;
            }

            //リストの数をカウントが超えたら０にリセット
            if(enemy_List_Manager.EnemyList.Count <= TargetCount)
            {
                TargetCount = 0;
            }

            //ターゲットをリストからセットする
            Target = enemy_List_Manager.EnemyList[TargetCount];
            //カウントを進める
            TargetCount++;
        }

        //セットを解除する
        if(Input.GetKeyDown(KeyCode.C))
        {
            Target = null;
        }

        //ターゲットがセットされていたら
        if(Target)
        {
            //ターゲットの座標を補完
            var pos = Vector3.zero;
            pos = Target.position;
            //カメラが上下しないように、高さはカメラ基準にする
            pos.y = Camera.transform.position.y;

            //ターゲットを見る
            Camera.transform.LookAt(pos);
        }
    }
}
