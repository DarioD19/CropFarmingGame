using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class SeedBoxSO : ScriptableObject
{
    public string Name;
    public Transform BoxWithSeedsPrefab;
    public Sprite CropImage;
    public LayerMask SeedBoxLayer;
}
