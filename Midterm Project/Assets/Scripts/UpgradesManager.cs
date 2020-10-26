using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    [SerializeField] RuntimeData _runtimeData;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Shark Pog");
        Debug.Log(gameObject.name);
        if(!_runtimeData._upgradesCollected.Contains(gameObject.name))
        {
            _runtimeData._upgradesCollected.Add(gameObject.name);
            gameObject.SetActive(false);
        }
    }
}
