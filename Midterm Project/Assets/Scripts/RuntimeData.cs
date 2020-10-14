using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New RunTimeData")]
public class RuntimeData : ScriptableObject
{
    public List<string> _keysCollected;
}
