using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropsHandler : MonoBehaviour
{
    [SerializeField] private HandleLandMaterials _baseLand;
    [SerializeField] private Player _player;
    private SeedBoxObject _selectedSeedBoxObject;
    private CropsObjectSO _cropsObjectSO;

    


     public Vector3 landSizeVectors = new Vector3(0.25f, 2f, 0.25f);


    public List<Transform> _cropsSpawnPointsList = new List<Transform>();
    private void Start()
    {
        _baseLand.OnLandIsPlanted += BaseLand_OnLandIsPlanted;
        _baseLand.OnLandWatered += BaseLand_OnLandWatered;
        
    }

    private void BaseLand_OnLandIsPlanted(object sender, System.EventArgs e)
    {
      
    }

    private void BaseLand_OnLandWatered(object sender, System.EventArgs e)
    {

        Player.Instance.GetSelectedLand().SpawnCropObject();
       
    }

    private void Update()
    {



        HandleInteractionsWithSeedBox();
        

        

    }

    private void HandleInteractionsWithSeedBox()
    {
        Vector3 landSizeVectors = new Vector3(2.5f, 0.1f, 2.5f);
        if (Physics.BoxCast(transform.position, landSizeVectors, transform.up, out RaycastHit hitInfo))
        {
            if (hitInfo.transform.TryGetComponent(out SeedBoxObject seedBoxObject))
            {

                if (seedBoxObject != _selectedSeedBoxObject)
                {
                    SetSelectedSeedObject(seedBoxObject);
                    if ( _selectedSeedBoxObject != null)
                    {
                        SetCropsObjectSO(_selectedSeedBoxObject.GetCurrentCropObjectSO());
                    }
                    else
                    {
                        SetCropsObjectSO(null); 
                    }
                    

                }
            }
           
          
            
        }
       

     




    }

    private void SetSelectedSeedObject(SeedBoxObject seedBoxObject)
    {
        this._selectedSeedBoxObject = seedBoxObject;

       
        

        
    }
    private SeedBoxObject GetSeedBoxObject()
    {
        return  _selectedSeedBoxObject;
    }
       
          
      
        
       
    private void SetCropsObjectSO(CropsObjectSO cropsObjecSO)
    {


        this._cropsObjectSO = cropsObjecSO;
        
       
        
    }
        
        

 

   

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, landSizeVectors);
    }

   

    public CropsObjectSO GetCropsObjectSo()
    {
        return _cropsObjectSO;
    }

    public List<Transform> GetCropSpawnPoints()
    {
        return _cropsSpawnPointsList;
    }

}
