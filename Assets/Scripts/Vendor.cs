using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vendor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")) GameManager.instance.ShowText("Press F to interact", 30, Color.white, transform.position, Vector3.zero, 5f);
    }
}
