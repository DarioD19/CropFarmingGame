using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ToolObjectSO : ScriptableObject
{
    public string Name;
    public Transform Prefab;
    public Sprite Sprite;
    public LayerMask ToolLayer;
}
