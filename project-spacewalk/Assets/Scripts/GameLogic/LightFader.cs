using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFader : MonoBehaviour
{
    private Light light;

    public float changeDuration = 1f;
    public float targetIntensity;

    private float lastIntensity;
    private float lastIntensityChange;

    public void SetTargetIntensity(float intensity) {
        Debug.Log("Setting intensity " + intensity);
        lastIntensity = light.intensity;
        lastIntensityChange = Time.time;
        targetIntensity = intensity;
    }

    void Awake()
    {
        light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        float deltaTime = Time.time - lastIntensityChange;
        light.intensity = Mathf.SmoothStep(lastIntensity, targetIntensity, deltaTime / changeDuration);
    }
}
