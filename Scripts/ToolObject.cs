using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolObject : MonoBehaviour
{
    [SerializeField] ToolObjectSO _toolObjectSO;

    public LayerMask ToolLayer;
    
    public ToolObjectSO GetToolObjectSO()
    {
        return _toolObjectSO;
    }

    private ISpawnTool _toolObjectParent;

    public void SetToolObjectParent(ISpawnTool toolObjectParent)
    {
        if (this._toolObjectParent != null)
        {
            this._toolObjectParent.ClearToolObject();
        }
        this._toolObjectParent = toolObjectParent;

        toolObjectParent.SetToolObject(this);
       transform.parent = toolObjectParent.GetToolObjectFollowTransform();
        transform.localPosition = Vector3.zero;
        if (ToolLayer == GetToolObbjectSOLayerMask())
        {
            transform.up = toolObjectParent.GetToolObjectFollowTransform().forward;
        }
       
       
        
    }

    public ISpawnTool GetToolObjectParent()
    {
        return _toolObjectParent;
    }

    public static ToolObject SpawnToolObject(ToolObjectSO toolObjectSO,ISpawnTool toolObjectParent)
    {
      
        Transform toolObjectTransform = Instantiate(toolObjectSO.Prefab);
        ToolObject toolObject = toolObjectTransform.GetComponent<ToolObject>();
        toolObject.SetToolObjectParent(toolObjectParent);

        return toolObject;
    }

    private LayerMask GetToolObbjectSOLayerMask()
    {
        return GetToolObjectSO().ToolLayer;
    }

 

  
}
