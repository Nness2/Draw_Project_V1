﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.DetectionExamples;

public class ActiveDrawingOn : MonoBehaviour
{

    public GameObject obj;
    
    public void activeTrail()
    {
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

    public void delete (){
        obj.GetComponent<PinchDraw>().State = 4;
        Debug.Log (obj.GetComponent<PinchDraw>().State);
    }

    public void drag (){
        obj.GetComponent<PinchDraw>().State = 5;
        Debug.Log (obj.GetComponent<PinchDraw>().State);
    }
    
    public void geoShaders (){
        obj.GetComponent<PinchDraw>().State = 6;
        Debug.Log (obj.GetComponent<PinchDraw>().State);
    }
    
    public void holoShaders (){
        obj.GetComponent<PinchDraw>().State = 7;
        Debug.Log (obj.GetComponent<PinchDraw>().State);
    }
    
    public void contourShaders (){
        obj.GetComponent<PinchDraw>().State = 8;
        Debug.Log (obj.GetComponent<PinchDraw>().State);
    }
    
    public void blurShaders (){
        obj.GetComponent<PinchDraw>().State = 9;
        Debug.Log (obj.GetComponent<PinchDraw>().State);
    }

}
