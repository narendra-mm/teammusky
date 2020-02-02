using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class OxygenCounter : MonoBehaviour
{

    public float oxygenCount = 100f;
    private bool _isRunning = false;
    public float oxygenLossPerSecond = .2f;

    public Text text;

    public Action OnOxygenDepleted;

    private void Awake()
    {
    }

    public void SetIsRunning (bool b) {
        _isRunning = b;
    }

    public void AddOxygenLossPerSecond(float amount) {
        oxygenLossPerSecond += amount;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(_isRunning) {
            oxygenCount -= oxygenLossPerSecond * Time.fixedDeltaTime;
            text.text = "" + Mathf.Max(Mathf.RoundToInt(oxygenCount), 0f) + "%";
            if(oxygenCount < 0) {
                OnOxygenDepleted?.Invoke();
            }
        }
    }
}
