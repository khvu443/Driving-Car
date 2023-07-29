using Assets.script;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;


public class RoadMove : MonoBehaviour
{
    public GameObject background;
    [SerializeField] float speed;
    [SerializeField] GameObject swap;

    Vector2 respawn;
    Vector2 offset;
    float changeTime = 0f;//thoi gian doi

    bool isScroll = false; //da doi dia hinh chua
    public static bool isEnd = false; // ket thuc game
    int swapNumber = 0; //so lan da doi

    // Start is called before the first frame update
    void Start()
    {
        respawn = swap.transform.position;
        Debug.Log(isScroll + " " + isEnd + " " + swapNumber);
        isEnd = false; 
    }

    // Update is called once per frame
    void Update()
    {

        changeTime += Time.deltaTime;

        if(!isEnd) //Khi chua ket thuc game
        {


            // Neu <30s va chua doi dia hinh -> duong se ngung di chuyen thay vao do la dia hinh se thay doi
            if (changeTime < 30f && !isScroll)
            {

                offset = new Vector2(0, Time.time * speed);
                GetComponent<Renderer>().material.mainTextureOffset = offset;
            }
            else if(swapNumber> 0) //khi da doi vi tri qua lan thu 2
            {
                isEnd = true; //set flag end game
                swapNumber = 0; //set lai so lan doi
                return;
            }
            else
            {
                isScroll = true; //set flag da doi
                offset = Vector2.zero;// set lai offset
                changeTime = 0f; //set lai time


                transform.Translate(Vector2.down * 5f * Time.deltaTime);// lam cho 2 dia hinh truot xuong

                if (background != null)
                {
                    background.transform.Translate(Vector2.down * 5f * Time.deltaTime);
                }

                if (transform.localPosition.y < -9.911392 && gameObject.tag == "bridge")
                {

                    Destroy(gameObject);
                    Destroy(background);

                }

                if (transform.localPosition.y < 0.01 && gameObject.tag == "highway") // khi dia hinh thu 2 nam o ngang tam cua dia hinh 1
                {
                    transform.position = respawn; //set cho dia hinh 2 do vi tri dia hinh 1

                    if (background.transform.localPosition.y < 0.01)
                    {
                        isScroll = false; //set lai flag doi
                        swapNumber++; // tang gia tri swap len
                    }
                }
            }
        }
    }

}

