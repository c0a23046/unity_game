using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyListManager : MonoBehaviour
{
    public List<Transform> EnemyList = new List<Transform>();
    // Start is called before the first frame update
    
    // Update is called once per frame
    void Update()
    {
        //リスト内で重複しないようにする
        for(int i=0; i<EnemyList.Count; i++)
        {
            //次のやつから比較する
            for(int k=i+1; k<EnemyList.Count; k++)
            {
                //重複していたら削除
                if(EnemyList[i] == EnemyList[k])
                {
                    EnemyList.RemoveAt(k);
                }
            }

            //敵が削除済ならリストからも削除
            if(!EnemyList[i])
            {
                EnemyList.RemoveAt(i);
            }
        }    
    }

    void OnTriggerEnter(Collider collider) 
    {
        //当たってきたオブジェクトをリストに追加
        if(collider.tag == "Enemy")
        {
            EnemyList.Add(collider.gameObject.transform); 
        }
    }

    void OnTriggerExit()
    {
        if(GetComponent<Collider>().tag == "Enemy")
        {
            for(int i=0; i<EnemyList.Count; i++)
            {
                //リストから同じ敵をみつけて削除する
                if(EnemyList[i] == GetComponent<Collider>().gameObject.transform)
                {
                    EnemyList.RemoveAt(i);
                }
            }
        }
    }
}
