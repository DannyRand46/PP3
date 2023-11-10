using System.Collections.Generic;
using UnityEngine;

public class DynamicCamera : MonoBehaviour
{

    enum TargetPosition
    {
        POS_MAIN_MENU,
        POS_CREDITS,
        POS_SETTINGS,
        POS_CONTROLS,
        POS_MINOTAUR,

        STILL,
    }

    [SerializeField] List<GameObject> positions;
    float cameraLerpRatio;
    Vector3 lastCameraPosition;
    TargetPosition targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        cameraLerpRatio = 0;
        targetPosition = TargetPosition.STILL;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetPosition != TargetPosition.STILL)
        {
            //dictates transition time
            cameraLerpRatio += (Time.deltaTime/3);
            transform.position = new Vector3(Mathf.Lerp(lastCameraPosition.x, positions[(int)targetPosition].transform.position.x, ParametricGrowthCurve(cameraLerpRatio)),
                Mathf.Lerp(lastCameraPosition.y, positions[(int)targetPosition].transform.position.y, ParametricGrowthCurve(cameraLerpRatio)),
                Mathf.Lerp(lastCameraPosition.z, positions[(int)targetPosition].transform.position.z, ParametricGrowthCurve(cameraLerpRatio)));
            if (cameraLerpRatio >= 1)
            {
                targetPosition = TargetPosition.STILL;
            }
        }
    }

    public void SetTargetMainMenu()
    {
        if (targetPosition == TargetPosition.STILL)
        {
            lastCameraPosition = transform.position;
            targetPosition = TargetPosition.POS_MAIN_MENU;
            cameraLerpRatio = 0;
        }
    }
    public void SetTargetCredits()
    {
        if (targetPosition == TargetPosition.STILL)
        {
            lastCameraPosition = transform.position;
            targetPosition = TargetPosition.POS_CREDITS;
            cameraLerpRatio = 0;
        }
    }
    public void SetTargetSettings()
    {
        if (targetPosition == TargetPosition.STILL)
        {
            lastCameraPosition = transform.position;
            targetPosition = TargetPosition.POS_SETTINGS;
            cameraLerpRatio = 0;
        }
    }
    public void SetTargetControls()
    {
        if (targetPosition == TargetPosition.STILL)
        {
            lastCameraPosition = transform.position;
            targetPosition = TargetPosition.POS_CONTROLS;
            cameraLerpRatio = 0;
        }
    }
    public void SetTargetMinotaur()
    {
        if (targetPosition == TargetPosition.STILL)
        {
            lastCameraPosition = transform.position;
            targetPosition = TargetPosition.POS_CONTROLS;
            cameraLerpRatio = 0;
        }
    }

    //Ease in / Ease out animation curve
    float ParametricGrowthCurve(float t)
    {
        float sqt = t * t;
        return sqt / (2.0f * (sqt - t) + 1.0f);
    }
}
