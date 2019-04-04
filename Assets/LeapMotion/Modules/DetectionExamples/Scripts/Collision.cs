using UnityEngine;
using System.Collections;
using Leap.Unity.DetectionExamples;

public class Collision : MonoBehaviour
{
	public GameObject obj;
    void OnTriggerEnter (Collider col)
    {
    	if (col.gameObject.tag == "3dObj" && obj.GetComponent<PinchDraw>().State == 4){
    		Destroy(col.gameObject);
    	}
        
/*        if (col.gameObject.tag == "colorPicker"){
	        col.transform.position = this.transform.position;
        }*/
        
    }
}