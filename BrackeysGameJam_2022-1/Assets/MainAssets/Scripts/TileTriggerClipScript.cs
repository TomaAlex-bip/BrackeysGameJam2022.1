using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileTriggerClipScript : MonoBehaviour
{
    
    
    [SerializeField] private Material seeMaterial;
    [SerializeField] private Material invisibleMaterial;

    private void Start()
    {
        var rend = gameObject.GetComponent<MeshRenderer>();
        rend.material = seeMaterial;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ClipTrigger"))
        {
            var rend = gameObject.GetComponent<MeshRenderer>();
            rend.material = invisibleMaterial;
            
            // print("gigel invisibil");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ClipTrigger"))
        {
            var rend = gameObject.GetComponent<MeshRenderer>();
            rend.material = seeMaterial;
            
            // print("gigel visibil");
        }
    }
}
