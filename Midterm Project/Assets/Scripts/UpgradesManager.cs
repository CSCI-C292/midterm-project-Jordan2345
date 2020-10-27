using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    [SerializeField] RuntimeData _runtimeData;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        Debug.Log(gameObject.name);
        if(!_runtimeData._upgradesCollected.Contains(gameObject.name))
        {
            _runtimeData._upgradesCollected.Add(gameObject.name);
            gameObject.SetActive(false);
        }
    }
}
