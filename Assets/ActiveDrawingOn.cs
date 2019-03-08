using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.DetectionExamples;

public class ActiveDrawingOn : MonoBehaviour
{

    public GameObject obj;
    // Start is called before the first frame update
    public void activeTrail()
    {
/*        if (obj.activeSelf == true) {
            obj.SetActive(false);
            Debug.Log ("false");
        }

        else if (obj.activeSelf == false){
            obj.SetActive(true);
            Debug.Log ("true");
        }*/
        obj.GetComponent<PinchDraw>().State = 1;
        Debug.Log (obj.GetComponent<PinchDraw>().State);
    }

    public void activeCube(){
        obj.GetComponent<PinchDraw>().State = 2;
        Debug.Log (obj.GetComponent<PinchDraw>().State);
    }

    public void activeSphere(){
        obj.GetComponent<PinchDraw>().State = 3;
        Debug.Log (obj.GetComponent<PinchDraw>().State);
    }

}
