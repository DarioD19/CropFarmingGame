using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolContainer : MonoBehaviour,ISpawnTool
{
    [SerializeField] private ToolObjectSO _toolObjectSO;


    [SerializeField] private Transform _containerTopPoint;

    private ToolObject _toolObject;

    private void Awake()
    {
        ToolObject.SpawnToolObject(_toolObjectSO, this);
    }

    public void Interact(Player player)
    {
        if (HasToolObject())
        {
            if (player.HasToolObject())
            {
                Debug.Log("Cannot place that there mylord");
            }
            else
            {
                GetToolObject().SetToolObjectParent(player);
            }
        }
        else
        {
            if (player.HasToolObject())
            {
                player.GetToolObject().SetToolObjectParent(this);
            }
        }


        
       
    }

    public Transform GetToolObjectFollowTransform()
    {
        return _containerTopPoint;
    }

    public void SetToolObject(ToolObject toolObject)
    {

        this._toolObject = toolObject;
    }

    public ToolObject GetToolObject()
    {
        return _toolObject;
    }

    public void ClearToolObject()
    {
        _toolObject = null;
    }

    public bool HasToolObject()
    {
        return _toolObject != null;
    }
}
