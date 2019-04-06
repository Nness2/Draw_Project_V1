﻿using UnityEngine;
using System.Collections;
using Leap.Unity.DetectionExamples;

public class Collision : MonoBehaviour
{
	public GameObject obj;

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "3dObj" && obj.GetComponent<PinchDraw>().State == 4){
			Destroy(col.gameObject);
		}

		if (col.gameObject.tag == "3dObj" && obj.GetComponent<PinchDraw>().State == 6){
			Material material = new Material(Shader.Find("Hand_Make/GeowireShader"));
			//material.color = Color.green;
			col.GetComponent<Renderer>().material = material;
		}
		
		if (col.gameObject.tag == "3dObj" && obj.GetComponent<PinchDraw>().State == 7){
			Material material = new Material(Shader.Find("Hand_Make/HoloSurfaceShader"));
			//material.color = Color.green;
			col.GetComponent<Renderer>().material = material;
		}
		
		if (col.gameObject.tag == "3dObj" && obj.GetComponent<PinchDraw>().State == 8){
			Material material = new Material(Shader.Find("Hand_Make/Contour"));
			//material.color = Color.white;
			col.GetComponent<Renderer>().material = material;
		}
		
		if (col.gameObject.tag == "3dObj" && obj.GetComponent<PinchDraw>().State == 9){
			Material material = new Material(Shader.Find("Hand_Make/BlurShader"));
			//material.color = Color.grey;
			col.GetComponent<Renderer>().material = material;
		}
/*        if (col.gameObject.tag == "colorPicker"){
	        col.transform.position = this.transform.position;
        }*/

	}
}
    

	        
