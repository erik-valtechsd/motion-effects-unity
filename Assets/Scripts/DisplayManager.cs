using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using extOSC.Examples;

public class DisplayManager : MonoBehaviour
{

    OscController oscCtrl;

    [SerializeField]
    public GameObject vfxInstance;

    [SerializeField]
    private float defaultTargetTime = 5.0f;
    private float targetTime = 5.0f;
    public bool idleMode = false;

    void Awake()
    {
        oscCtrl = vfxInstance.GetComponent<OscController>();

    }
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        targetTime -= Time.deltaTime;
        if (targetTime <= 0.0f)
        {
            idleMode = true;
        }

        if (idleMode)
        {
            float posX = Mathf.PerlinNoise(Time.time*0.2f, 0.0f);
            float posY = Mathf.PerlinNoise(0.0f, Time.time*0.2f);
            float posZ = Mathf.PerlinNoise(Time.time*0.2f, Time.time*0.2f);
            oscCtrl.SetX(ExtensionMethods.Remap(posX, 0, 1, -180, 180));
            oscCtrl.SetY(ExtensionMethods.Remap(posY, 0, 1, -180, 180));
            oscCtrl.SetZ(ExtensionMethods.Remap(posY, 0, 1, -180, 180));

        }
    }

    public void ResetIdleMode()
    {
        idleMode = false;
        targetTime = defaultTargetTime;
    }

}
public static class ExtensionMethods
{

    public static float Remap(this float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

}

