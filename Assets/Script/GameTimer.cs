using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public float Timer = 60; //残り時間。60秒に設定
    public Text TimerText; //UIテキスト.今回はTimerで設定した値を画面に表示させる
    public GameObject ClearWindow; //時間切れ時の表示するクリア画面のＵＩオブジェクト
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //マイフレームTimer(60秒に設定)残り時間から経過時間を引いている。カウントダウン
        Timer -= Time.deltaTime;
        TimerText.text = ((int)Timer).ToString(); //Timerを整数型に変換

        if(Timer <= 0)
        {
            //クリア画面表示
            ClearWindow.SetActive(true);
            //ゲームの時間を停止
            Time.timeScale = 0;
            //スクリプト自体無効化してUpdate処理を止める
            enabled = false;
        }
    }
}
