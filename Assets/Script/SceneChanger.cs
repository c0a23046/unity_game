using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    //ロードするシーンの変数
    public string SceneName;
    public void SceneChange()
    {
        //ScenNameで選んだものをロードする
        SceneManager.LoadScene(SceneName);
    }
}
