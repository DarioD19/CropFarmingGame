using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawnBox 
{
    public Transform GetBoxObjectFollowTransform();

    public void SetBoxObject(SeedBoxObject seedBoxObject);

    public SeedBoxObject GetBoxObject();

    public void ClearBoxObject();

    public bool HasBoxObject();
}
