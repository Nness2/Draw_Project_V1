using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnsTriggerEnter(Collision Info) {
      //if (other.GameObject.tag == "line"){
        //other.GameObject.SetActive(false);
        Debug.Log(Info.GetComponent<Collider>());
      //}
    }
}
