using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCollision : MonoBehaviour
{
    [SerializeField] GameObject otherPortal;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
           other.transform.position = new Vector3(otherPortal.transform.position.x, other.transform.position.y, other.transform.position.z) ;
        }

    }
}
