using Assets.script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    public GameObject light_l, light_r;
    static public GameObject globalLight;
    GameObject lightLamp_r, lightLamp_l, glomEffectOcean, lightOcean;

    public GameObject[] roads;
    public GameObject[] background;
    GameObject road;
    GameObject bg;
    private bool isNight;
    public static bool isStartGame;
    float delay = 0;

    // Start is called before the first frame update
    void Start()
    {
        isStartGame = false;
        globalLight = GameObject.Find("Global Light 2D");
        for (int i = 0; i < 2; i++)
        {
            createBackground(i);
            createRoad(i);
        }

        //lightLamp_r.GetComponent<Light2D>().enabled = true;
        //lightLamp_l.GetComponent<Light2D>().enabled = true;
        //lightOcean.GetComponent<Light2D>().enabled = true;
        //glomEffectOcean.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        if(isStartGame)
        {
            modifyLightEffect();
            PauseGame();
            changeDayTime();
        }
    }


    //spawn road
    void createRoad(int i)
    {
        road = Instantiate(roads[i], roads[i].transform.localPosition, Quaternion.identity);
        road.GetComponent<RoadMove>().background = bg;
    }

    void createBackground(int i)
    {

        bg = Instantiate(background[i], roads[i].transform.localPosition, Quaternion.identity);
        if (roads[i].tag == "highway")
        {
            if(bg.tag == "city")
                bg.transform.localPosition += new Vector3(0, 2, 0);
        }


        switch (bg.tag)
        {
            case "ocean":
                lightOcean = bg.transform.GetChild(1).gameObject;
                glomEffectOcean = bg.transform.GetChild(2).gameObject;
                bg.GetComponent<AudioSource>().clip = Sound_Manager.Instance.oceanSfx;
                bg.GetComponent<AudioSource>().Play();
                break;
            case "city":
                lightLamp_r = bg.transform.GetChild(3).gameObject.transform.GetChild(0).gameObject;
                lightLamp_l = bg.transform.GetChild(3).gameObject.transform.GetChild(1).gameObject;
                break;
        }
    }



    //Turn on and off light when dark
    void modifyLightEffect()
    {
        if (globalLight.GetComponent<Light2D>().intensity <= 1 && globalLight.GetComponent<Light2D>().intensity >= 0.5)
        {

            if(light_l != null && light_r != null)
            {
                light_l.SetActive(false);
                light_r.SetActive(false);
            }


            if (lightLamp_r != null) lightLamp_r.GetComponent<Light2D>().enabled = false;
            if (lightLamp_l != null) lightLamp_l.GetComponent<Light2D>().enabled = false;
             
            if (lightOcean != null) lightOcean.GetComponent<Light2D>().enabled = false;
            if (glomEffectOcean != null) glomEffectOcean.SetActive(false);

        }
        else if (globalLight.GetComponent<Light2D>().intensity <= 0.3)
        {
            if (light_l != null && light_r != null)
            {
                light_l.SetActive(true);
                light_r.SetActive(true);
            }

            if (lightLamp_r != null) lightLamp_r.GetComponent<Light2D>().enabled = true;
            if (lightLamp_l != null) lightLamp_l.GetComponent<Light2D>().enabled = true;

            if (lightOcean != null) lightOcean.GetComponent<Light2D>().enabled = true;
            if (glomEffectOcean != null) glomEffectOcean.SetActive(true);
        }
    }

    void PauseGame()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            isStartGame = false;
            Time.timeScale = 0f;
            UI_Manager.Instance.isFinish = 3;
        }
    }

    void changeDayTime()
    {
        if (!isNight)
        {
            globalLight.GetComponent<Light2D>().intensity -= 0.0001f;
            if (globalLight.GetComponent<Light2D>().intensity <= 0.3f)
            {
                DelayChangeDayTime();
            }
        }
        else if (isNight)
        {
            globalLight.GetComponent<Light2D>().intensity += 0.0001f;
            if (globalLight.GetComponent<Light2D>().intensity >= 1f)
            {
                DelayChangeDayTime();
            }
        }
    }

    void DelayChangeDayTime()
    {
        delay += Time.deltaTime;
        if (delay >=3f)
        {
            isNight = !isNight;
        }

    }

}
