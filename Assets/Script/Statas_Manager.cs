using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Statas_Manager : MonoBehaviour
{
    public GameObject Main;
    public int HP;
    public int Max_HP;
    public int Score;
    public float ResetTime = 0; 
    public Image HPGage;
    public GameObject Effect;
    public AudioSource audioSource;
    public AudioClip HitSE;
    public string TagName;

    public Collider mycollider;

    private bool isDead = false;
    //private bool isTouchingEnemy = false; // 敵と接触しているかどうかのフラグ

    void Start()
    {
        mycollider = GetComponent<Collider>();
    }

    private void Update() 
    {
        if(isDead) return;
        

        if(HP <= 0)
        {
            HP = 0;
            var effect =  Instantiate(Effect);
            effect.transform.position = transform.position;
            GameObject.Find("GameSystem").GetComponent<GameSysteManager>().Score += Score;
            Destroy(effect, 5);
            Destroy(Main);
            return;
        }

        float percent = (float)HP / Max_HP;
        HPGage.fillAmount = percent;
    }
    
    private void OnTriggerEnter(Collider other) 
    {
        // 自分がプレイヤーの場合 → 敵に触れたらダメージ
        if (CompareTag("Player") && other.CompareTag("Enemy"))
        {
            Damage();
            mycollider.enabled = false;
            Invoke("ColliderReSet", ResetTime);
        }

        // 自分が敵の場合 → 武器に触れたらダメージ
        else if (CompareTag("Enemy") && other.CompareTag("Weapon"))
        {
            Damage();
        }

    }


    void Damage()
    {
        audioSource.PlayOneShot(HitSE);
        HP--;
    }

    void ColliderReSet()
    {
        mycollider.enabled = true; //BoxColliderをオンにする
    }
}
