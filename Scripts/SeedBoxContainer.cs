using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedBoxContainer : MonoBehaviour,ISpawnBox
{
    [SerializeField] private SeedBoxSO _seedBoxSO;
    [SerializeField] private Transform _containerTopPoint;

    private SeedBoxObject _boxObject;

    private void Awake()
    {
        SeedBoxObject.SpawnBoxObject(_seedBoxSO, this);
    }

    public void Interact(Player player)
    {
        if (HasBoxObject())
        {
            if (player.HasBoxObject())
            {
                Debug.Log("Cannot place that there mylord");
            }
            else
            {
                GetBoxObject().SetBoxObjectParent(player);
            }
        }
        else
        {
            if (player.HasBoxObject())
            {
                player.GetBoxObject().SetBoxObjectParent(this);
            }
        }
    }





    public Transform GetBoxObjectFollowTransform()
    {
        return _containerTopPoint;
    }

    public void SetBoxObject(SeedBoxObject boxObject)
    {
        this._boxObject = boxObject;
    }

    public SeedBoxObject GetBoxObject()
    {
        return _boxObject;
    }

    public void ClearBoxObject()
    {
        _boxObject = null;
    }

    public bool HasBoxObject()
    {
        return _boxObject != null;
    }
}
