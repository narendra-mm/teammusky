using System;
using UnityEngine;
using UnityEngine.Playables;

[Serializable]
public class LightControlBehaviour : PlayableBehaviour
{
    public Color color = Color.white;
    public float intensity = 1f;
}