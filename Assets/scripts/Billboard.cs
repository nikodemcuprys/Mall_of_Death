using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Billboard : MonoBehaviour
{
    public GameObject cam;

    private void Start() {
        if (cam == null)
        {
            cam = GameObject.FindWithTag("MainCamera");
        }
    }
    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(transform.position + cam.transform.forward);
    }
}
