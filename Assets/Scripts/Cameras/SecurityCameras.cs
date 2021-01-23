using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCameras : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _securityCameras;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach (GameObject camera in _securityCameras)
                camera.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            foreach (GameObject camera in _securityCameras)
                camera.SetActive(false);
        }
    }
}
