using Assets.script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UIElements;

public class Car : MonoBehaviour
{
    public static CarObj carObj = new CarObj()
    {
        force = 700f,
        score = 0,
    };

    bool isHitCoin = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        carMoving();
    }

    void carMoving()
    {
        if (Input.GetKey("w"))
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, carObj.force * Time.deltaTime));

        if (Input.GetKey("a"))
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-carObj.force * Time.deltaTime, 0));

        if (Input.GetKey("d"))
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(carObj.force * Time.deltaTime, 0));

        if (Input.GetKey("s"))
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -carObj.force * Time.deltaTime));

        if (Input.GetKey(KeyCode.Space))
        {
            Sound_Manager.Instance.PlaySFX(Sound_Manager.Instance.hornCarSFX);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Finish")
        {
            StartCoroutine(stopCar());
            GetComponent<Car>().enabled = false;
            UI_Manager.Instance.isFinish = 1;
            carObj.score = ScoreManager.Score;

            Sound_Manager.Instance.PlaySFX(Sound_Manager.Instance.finishSFX);
        }

        if (collision.tag == "coin")
        {
            Sound_Manager.Instance.PlaySFX(Sound_Manager.Instance.coinHitSfx);

            ScoreManager.instance.AddPoint();
            Animator ani = collision.GetComponent<Animator>(); //Get animator
            ani.SetBool("isBlink", true); //Gắn giá trị boolean là true để thỏa mãn đk transition
            Destroy(collision.gameObject, ani.GetCurrentAnimatorStateInfo(0).length + 0.1f);
            isHitCoin = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        isHitCoin = !isHitCoin;
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Obstacle")
        {
            Sound_Manager.Instance.PlaySFX(Sound_Manager.Instance.carCrashSfx);

            gameObject.GetComponent<Rigidbody2D>().freezeRotation = false;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 1.0f;
            Physics2D.IgnoreCollision(gameObject.GetComponent<PolygonCollider2D>(), Camera.main.GetComponent<EdgeCollider2D>());

            collision.collider.GetComponent<Rigidbody2D>().freezeRotation = false;
            collision.collider.GetComponent<Rigidbody2D>().gravityScale = 1.0f;

            RoadMove.isEnd = true;
            UI_Manager.Instance.isFinish = 2;
            Destroy(collision.gameObject, 0.1f);


        }
    }

    IEnumerator stopCar()
    {
        yield return new WaitForSeconds(1);
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
    }
}
