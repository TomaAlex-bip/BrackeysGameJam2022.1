using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPowerup : MonoBehaviour
{
    
    [SerializeField] private float initialSphereSize;
    [SerializeField] private float growSpeed = 0.1f;
    [SerializeField] private float shrinkSpeed = 0.3f;
    [SerializeField] private float maxSphereSize = 5f;

    [SerializeField] private float maxPower = 10;

    [SerializeField] private Slider sliderPowerup;
    
    private void Start()
    {
        initialSphereSize = transform.localScale.x;
        sliderPowerup.maxValue = maxPower;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.E) && transform.localScale.x < maxSphereSize && sliderPowerup.value < sliderPowerup.maxValue)
        {
            transform.localScale += Vector3.one * growSpeed * Time.deltaTime;
        }
        else// if (!Input.GetKey(KeyCode.E) && transform.localScale.x > initialSphereSize)
        {
            transform.localScale -= Vector3.one * shrinkSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.E))
        {
            sliderPowerup.value += growSpeed * Time.deltaTime;
        }
        else
        {
            sliderPowerup.value -= growSpeed * Time.deltaTime;
        }

        if (transform.localScale.x < initialSphereSize)
        {
            transform.localScale = Vector3.one * initialSphereSize;
        }

        if (sliderPowerup.value < 0)
        {
            sliderPowerup.value = 0;
        }
    }
    
    
    
}
