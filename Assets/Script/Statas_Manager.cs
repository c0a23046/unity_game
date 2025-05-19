using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Statas_Manager : MonoBehaviour
{
    public GameObject Main;
    public int HP; //体力
    public int Max_HP; //最大体力
    public int Score; //enemyを倒したときに加算するスコア
    public float ResetTime = 0; //ダメージを受けた後に当たり判定（Collider）を再有効化するまでの待ち時間
    public Image HPGage; //体力ゲージのUIイメージ
    public GameObject Effect; //ダメージ時または死亡時に表示するエフェクトPrefab
    public AudioSource audioSource; //効果音を鳴らすためのAudioSourceコンポーネント
    public AudioClip HitSE; //ダメージを受けた時に再生する効果音
    public string TagName; 

    public Collider mycollider; //当たり判定用のCollider（BoxColliderやCapsuleColliderなど）を保持

    private bool isDead = false;
    //private bool isTouchingEnemy = false; // 敵と接触しているかどうかのフラグ

    void Start()
    {
        //自分のColliderを取得しmycolliderにセット
        mycollider = GetComponent<Collider>();
    }

    private void Update() 
    {
        //死亡済みなら以降の処理をスキップ
        if (isDead) return;
        

        if(HP <= 0) //HPが0になったら
        {
            //エフェクトを自分の位置に生成（死亡エフェクト）
            HP = 0;
            var effect =  Instantiate(Effect);
            effect.transform.position = transform.position;
            //ゲーム管理オブジェクトのスコアにこの敵のScoreを加算
            GameObject.Find("GameSystem").GetComponent<GameSysteManager>().Score += Score;
            //エフェクトは5秒後に消える
            Destroy(effect, 5);
            //自分（Mainオブジェクト）を破壊（消滅）
            Destroy(Main);
            return;
        }

        //HPを最大HPで割った割合を計算し、HPゲージのfillAmountに設定（UIゲージの表示更新）
        float percent = (float)HP / Max_HP;
        HPGage.fillAmount = percent;
    }
    
    private void OnTriggerEnter(Collider other) 
    {
        // 自分がプレイヤーの場合 → 敵に触れたらダメージ
        if (CompareTag("Player") && other.CompareTag("Enemy"))
        {
            Damage(); //ダメージ関数を呼び出す
            mycollider.enabled = false; //自分の当たり判定を一時的に無効にする
            Invoke("ColliderReSet", ResetTime); //Resettime秒後にColliderを有効に戻す
        }

        // 自分が敵の場合 → 武器に触れたらダメージ
        else if (CompareTag("Enemy") && other.CompareTag("Weapon"))
        {
            Damage();
        }

    }


    void Damage()
    {
        //hit音のSEを呼び出す
        audioSource.PlayOneShot(HitSE);
        HP--;
    }

    void ColliderReSet()
    {
        mycollider.enabled = true; //BoxColliderをオンにする
    }
}
