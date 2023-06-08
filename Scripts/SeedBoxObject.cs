using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedBoxObject : MonoBehaviour
{
    [SerializeField] SeedBoxSO _seedBoxSO;

    [SerializeField] private CropsObjectSO _cropObjectSO;
    [SerializeField] private CropsObject _cropObject;

    public LayerMask BoxLayer;

    public SeedBoxSO GetSeedBoxSO()
    {
        return _seedBoxSO;
    }

    private ISpawnBox _boxObjectParent;

    public void SetBoxObjectParent(ISpawnBox boxObjectParent)
    {
        if (this._boxObjectParent != null)
        {
            this._boxObjectParent.ClearBoxObject();
        }
        this._boxObjectParent = boxObjectParent;

        boxObjectParent.SetBoxObject(this);
        transform.parent = boxObjectParent.GetBoxObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public ISpawnBox GetBoxObjectParent()
    {
        return _boxObjectParent;
    }

    public static SeedBoxObject SpawnBoxObject(SeedBoxSO boxSO,ISpawnBox boxObjectParent)
    {
        Transform boxObjectTransform = Instantiate(boxSO.BoxWithSeedsPrefab);
        SeedBoxObject boxObject = boxObjectTransform.GetComponent<SeedBoxObject>();
        boxObject.SetBoxObjectParent(boxObjectParent);

        return boxObject;
    }

    public LayerMask GetBoxObjectSOLayerMask()
    {
        return this.BoxLayer;
    }

    public CropsObjectSO GetCurrentCropObjectSO()
    {
        return _cropObjectSO;
    }

    public CropsObject GetCurrentCropObject()
    {
        return _cropObject;
    }
   
}
