using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFinish : MonoBehaviour
{

    float screenTop = 0f;
    Vector2 oldPos;

    // Start is called before the first frame update
    void Start()
    {
        float screenZ = -Camera.main.transform.localPosition.z;
        Vector3 upperRight = new Vector3(Screen.width, Screen.height, screenZ);
        Vector3 upperRightWorld = Camera.main.ScreenToWorldPoint(upperRight);
        screenTop = upperRightWorld.y;
        oldPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(RoadMove.isEnd && transform.localPosition.y > screenTop)
        {
            transform.localPosition = new Vector3(0, transform.localPosition.y - 2 , -1);
        }
    }

}
