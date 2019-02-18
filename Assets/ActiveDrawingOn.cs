using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveDrawingOn : MonoBehaviour
{


    // Start is called before the first frame update
    public void SetTest()
    {
        GameObject obj = GameObject.Find("PinchDrawing")as GameObject;
        if (obj.SetActive(true)) {
            obj.SetActive(false);
        }

        if (obj.SetActive(false))
        {
            obj.SetActive(true);
        }

        //PinchDrawing.SetActive(false);
    }

}
