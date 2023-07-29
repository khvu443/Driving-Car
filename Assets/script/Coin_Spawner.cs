//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Net.Http;
//using UnityEngine;


//public class Coin_Spawner : MonoBehaviour
//{
//    public GameObject coinPrefab;
//    public float speed = 4f;
//    private bool isCoinSpawned = false;
//    public Transform holder;


//    private Camera mainCamera;


//    // Start is called before the first frame update
//    void Start()
//    {
//        mainCamera = Camera.main;
//        StartCoroutine(CoinSpawner());


//    }

//    // Update is called once per frame
//    void Update()
//    {
//        transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
//    }

//    //void CoinSpawn()
//    //{

//    //    float randomX = Random.Range(mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x, mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x);
//    //    Vector3 spawnPosition = new Vector3(randomX, mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y, transform.position.z);
//    //    Instantiate(coinPrefab, spawnPosition, Quaternion.Euler(0, 0, 90));
//    //}
//    private void CoinSpawn()
//    {
//        if (isCoinSpawned)
//        {
//            return;
//        }

//        float randomX = UnityEngine.Random.Range(-5f, 5f);
//        Vector3 spawnPosition = new Vector3(randomX, mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y, transform.position.z);
//        GameObject coint = Instantiate(coinPrefab, spawnPosition, Quaternion.Euler(0, 0, 90));
//        isCoinSpawned = true;
//        coint.transform.SetParent(this.holder);


//    }

//    IEnumerator CoinSpawner()
//    {
//        while (true)
//        {
//            yield return new WaitForSeconds(1f);
//            CoinSpawn();
//        }
//    }
//}
////private void ontriggerenter2d(collider2d collision)
////{
////    debug.log("collision occurred !!!");
////}
using System;
using System.Collections;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class Coin_Spawner : MonoBehaviour
{

    public GameObject coinPrefab;

    public static float speed = 3f; //Qua màn mới tăng speed 
    public static float timeSpawn = 3f;//Qua màn mới có thể tăng spawn time 
    private bool isCoinSpawned = false;

    public Transform holder;
    private Camera mainCamera;
    GameObject coin;

    static public float randomX;


    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        StartCoroutine(CoinSpawner());


    }

    // Update is called once per frame
    void Update()
    {
        if(coin != null)
        {
            MovingAndDestroyCoin();
        }

    }

    private void CoinSpawn()
    {
        if(!RoadMove.isEnd)
        {
            if (isCoinSpawned)
            {
                return;
            }

            randomX = UnityEngine.Random.Range(-4f, 4f);
            Vector3 spawnPosition = new Vector3(randomX, mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y, -1);
            coin = Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
            coin.tag = "coin";
            isCoinSpawned = true;
            coin.transform.SetParent(this.holder);
        }

    }

    IEnumerator CoinSpawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeSpawn);
            CoinSpawn();
            isCoinSpawned = false; // Đặt lại cờ để tạo đồng xu tiếp theo
        }
    }

    void MovingAndDestroyCoin()
    {
        transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
        if (holder.transform.GetChild(0).gameObject.transform.position.y < -mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y * 2)
        {
            Destroy(holder.transform.GetChild(0).gameObject);
        }; //Nếu coin mà ra khỏi tầm nhìn camera thì sẽ destroy nó
    }
}
