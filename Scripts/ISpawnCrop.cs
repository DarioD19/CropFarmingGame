using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawnCrop 
{
    public Transform GetCropObjectFollowTransform();

    public void SetSetCropObject(CropsObject cropObject);

    public CropsObject GetCropsObject();

    public void ClearCropsObject();

    public bool HasCropsObject();
}
