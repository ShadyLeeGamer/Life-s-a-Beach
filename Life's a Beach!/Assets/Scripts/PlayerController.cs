using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] WaterBlaster[] waterBlasters;

    [SerializeField] FlyboardController flyboard;
    float flyboardDefaultRotation;
    [SerializeField] TargetRotator[] flyboardRotators;
    float[] flyboardRotatorDefaultTargets;

    Camera gameCam;

    private void Awake()
    {
        gameCam = Camera.main;

        flyboardDefaultRotation = flyboard.transform.eulerAngles.z;
    }

    private void Start()
    {
        flyboardRotatorDefaultTargets = new float[flyboardRotators.Length];
        for (int i = 0; i < flyboardRotatorDefaultTargets.Length; i++)
        {
            flyboardRotatorDefaultTargets[i] = flyboardRotators[i].Target;
        }
    }

    private void Update()
    {
        UpdateWaterBlasters();

        //SetFreeBodyRotatorTargets(flyboard.transform.eulerAngles.z);
    }

    void UpdateWaterBlasters()
    {
        Vector2 aimTarget = gameCam.ScreenToWorldPoint(Input.mousePosition);
        bool shootInput = Input.GetMouseButton(0);
        foreach (var waterBlaster in waterBlasters)
        {
            waterBlaster.ProcessInputs(aimTarget, shootInput);
        }
    }

    void SetFreeBodyRotatorTargets(float target)
    {
        for (int i = 0; i < flyboardRotators.Length; i++)
        {
            flyboardRotators[i].Target = flyboardRotatorDefaultTargets[i] - (target - flyboardDefaultRotation);
        }
    }
}
