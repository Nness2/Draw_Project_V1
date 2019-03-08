using UnityEngine;
using System.Collections;

public class coll : MonoBehaviour
{
    void OnCollisionEnter (Collision col)
    {
        Destroy(col.gameObject);
    }
}