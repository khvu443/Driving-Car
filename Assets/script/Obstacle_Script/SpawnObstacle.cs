using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class SpawnObstacle : MonoBehaviour
{

    public static SpawnObstacle Instance { get; private set; }
    public GameObject obstaclePrefab;
    public Transform obstacles;
    public Sprite[] sprites;


    public static float timeSpawn = 5f;//Qua màn mới có thể tăng spawn time 

    GameObject ob;
    private bool isSpawnObstacle = false;
    float randomX;
    Vector3 spawnPos;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this);

    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(timeSpawn);
        StartCoroutine(DelayCreateCar());
    }

    // Update is called once per frame
    void Update()
    {
        if (ob != null)
        {
            MovingAndDestroyObstacles();
            turnOnLightObstacle(GameManager.globalLight.GetComponent<Light2D>().intensity > 0.3);
        }
    }


    void createCar()
    {

        if (!RoadMove.isEnd)
        {
            if (isSpawnObstacle)
            {
                return;
            }

            int i = Random.Range(0, sprites.Length);

            randomX = Random.Range(-4f, 4f);

            spawnPos = new Vector3(
                        randomX, //random x
                        Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y, //Lấy đc giới hạn y trên của camera và cho xe nó xuất hiện tại vị trí đó
                        -1
                    ); //Vị trí obstacle xh



            ob = Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);
            ob.tag = "Obstacle";// set tag cho obstacle
            ob.GetComponent<SpriteRenderer>().sprite = sprites[i];

            turnOnLightObstacle(GameManager.globalLight.GetComponent<Light2D>().intensity > 0.3);

            isSpawnObstacle = true; //Khi tạo ra 1 xe thì đổi qua true

            //Khi xe xuất hiện sẽ không bị vướng edge collider khi đi xuống
            Physics2D.IgnoreCollision(ob.GetComponent<BoxCollider2D>(), Camera.main.GetComponent<EdgeCollider2D>());

            ob.transform.SetParent(this.obstacles);
        }
    }

    IEnumerator DelayCreateCar()
    {
        while (true) //Để chạy vĩnh viễn trong hàm Start
        {

            yield return new WaitForSeconds(timeSpawn);
            isSpawnObstacle = false; // Set flag true to create again
            createCar();

        }
    }

    void MovingAndDestroyObstacles()
    {
        float speed = Random.Range(5, 10);// Random speed của obstacle
        ob.GetComponent<Rigidbody2D>().velocity = Vector2.down * speed;

        if (obstacles.transform.GetChild(0).gameObject.transform.position.y < -Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y * 2)
        {
            Destroy(obstacles.transform.GetChild(0).gameObject);
        }//Nếu obstacle mà ra khỏi tầm nhìn camera thì sẽ destroy nó
    }

    void turnOnLightObstacle(bool check)
    {
        ob.transform.GetChild(0).GetComponent<Light2D>().enabled = !check;
        ob.transform.GetChild(1).GetComponent<Light2D>().enabled = !check;
    }


}