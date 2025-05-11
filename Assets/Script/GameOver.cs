using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject Player;
    public GameObject GameOverCanvas;
    // Update is called once per frame
    void Update()
    {
        if(!Player) //もしプレイヤーがいなくなったらゲームオーバーを表示させる
        {
            GameOverCanvas.SetActive(true);
        }
    }

    public void GameReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //今開いているシーンの名前を読み込んでロードする
    }
}
