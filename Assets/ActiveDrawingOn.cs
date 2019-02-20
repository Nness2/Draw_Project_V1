using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveDrawingOn : MonoBehaviour
{

    public GameObject obj;
    // Start is called before the first frame update
    public void SetTest()
    {
        //obj.SetActive(true);
        if (obj.activeSelf == true) {
            obj.SetActive(false);
            Debug.Log ("false");
        }

        else if (obj.activeSelf == false){
            obj.SetActive(true);
            Debug.Log ("true");
        }
        //obj.GetComponent(PinchDraw).enabled = false;
        //obj.enabled = !obj.enabled;
        //PinchDrawing.SetActive(false);
    }

}
