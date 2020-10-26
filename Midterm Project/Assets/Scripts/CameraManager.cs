using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager CameraInstance;
    private void Awake()
    {
        CameraInstance = this;
    }
    public void ChangeCamera(int prevCam, int currCam)
    {
        Transform oldC = gameObject.transform.Find("Level" + prevCam + " Camera");
        Transform newC = gameObject.transform.Find("Level" + currCam + " Camera");
        Camera oldCam = oldC.GetComponent<Camera>();
        Camera newCam = newC.GetComponent<Camera>();

        oldCam.enabled = false;
        newCam.enabled = true;
    }
}
