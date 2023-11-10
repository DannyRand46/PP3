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
            cameraLerpRatio += Time.deltaTime;
            transform.position = new Vector3(Mathf.Lerp(lastCameraPosition.x, positions[(int)targetPosition].transform.position.x, ParametricGrowthCurve(cameraLerpRatio)),
                Mathf.Lerp(lastCameraPosition.y, positions[(int)targetPosition].transform.position.y, ParametricGrowthCurve(cameraLerpRatio)),
                Mathf.Lerp(lastCameraPosition.z, positions[(int)targetPosition].transform.position.z, ParametricGrowthCurve(cameraLerpRatio)));
            if (cameraLerpRatio >= 1)
            {
                targetPosition = TargetPosition.STILL;
            }
        }
    }

    void SetTargetMainMenu()
    {
        if (targetPosition == TargetPosition.STILL)
        {
            targetPosition = TargetPosition.POS_MAIN_MENU;
            cameraLerpRatio = 0;
        }
    }
    void SetTargetCredits()
    {
        if (targetPosition == TargetPosition.STILL)
        {
            targetPosition = TargetPosition.POS_CREDITS;
            cameraLerpRatio = 0;
        }
    }
    void SetTargetSettings()
    {
        if (targetPosition == TargetPosition.STILL)
        {
            targetPosition = TargetPosition.POS_SETTINGS;
            cameraLerpRatio = 0;
        }
    }
    void SetTargetControls()
    {
        if (targetPosition == TargetPosition.STILL)
        {
            targetPosition = TargetPosition.POS_CONTROLS;
            cameraLerpRatio = 0;
        }
    }
    void SetTargetMinotaur()
    {
        if (targetPosition == TargetPosition.STILL)
        {
            targetPosition = TargetPosition.POS_CONTROLS;
            cameraLerpRatio = 0;
        }
    }

    //Ease in / Ease out animation curve
    float ParametricGrowthCurve(float t)
    {
        t /= 2;
        float sqt = t * t;
        return sqt / (2.0f * (sqt - t) + 1.0f);
    }
}
