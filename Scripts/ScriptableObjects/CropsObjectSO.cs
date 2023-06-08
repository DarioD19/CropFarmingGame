using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CropsObjectSO : ScriptableObject
{
    public string Name;
    public Transform CropPrefab;
    public Sprite CropSprite;
    public LayerMask CropLayer;
}
