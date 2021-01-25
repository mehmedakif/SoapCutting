using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskDestroyer : MonoBehaviour
{
    void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag != "Knife")
            Destroy(other);
    }
}
