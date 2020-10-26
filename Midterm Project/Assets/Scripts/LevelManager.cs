using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LevelManager : MonoBehaviour
{
    [SerializeField] RuntimeData _runtimeData;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        int currLevel = _runtimeData._currentLevel;
        //Debug.Log(gameObject.name);
        string levelName = "Level" + currLevel + " End";
        if (gameObject.name.Equals(levelName))
            _runtimeData._currentLevel++;
        else
            _runtimeData._currentLevel--;
        CameraManager.CameraInstance.ChangeCamera(currLevel, _runtimeData._currentLevel);
    }
}
