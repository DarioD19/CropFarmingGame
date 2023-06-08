using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HandleLandMaterials : MonoBehaviour
{
    [SerializeField] private CropsHandler _cropsHnadler;

    public event EventHandler OnLandWatered;
    public event EventHandler OnLandIsPlanted;

    private bool _canLandBeDigged;
    private int _hitsOnLand= 0 ;
    int _maxHitsOnLand = 5;


    private bool _canLandBePlanted;
    private int _numberOfCropsPlanted = 0;
    private int _maxNumberOfCropsPlanted = 6;
    private bool _landIsPlanted;

    private bool _canLandBeWatered;
    private int _numberOfWaterShoots = 0 ;
    private int _maxNumberOfWaterShots = 6;

    private float _plantingTimer;
    private float _timeToPlant = 30f;

    






   

    

    public enum LandStatus
    {
        Soil,
        Tiled,
        Planted,
        Watered
    }
    LandStatus landStatus;



    [SerializeField] private Material _soilMat, _tiledMat, _wateredMat;


    private Renderer _renderer;


    private void Awake()
    {
        _renderer = GetComponent<Renderer>();


    }
    private void Start()
    {
       
        _plantingTimer = _timeToPlant;
        SwitchLandStatus(LandStatus.Soil);
        Player.Instance.OnLandDigingInteraction += Player_OnLandDigingInteraction;
        Player.Instance.OnLandPlantingInteraction += Player_OnLandPlantingInteraction;
        Player.Instance.OnLandWateringInteraction += Player_OnLandWateringInteraction;
        _landIsPlanted = false;

    }
    private void Update()
    {
        if (_numberOfCropsPlanted >0 && _plantingTimer > 0)
        {
            _plantingTimer -= Time.deltaTime;
            Debug.Log(_plantingTimer);
            
        }
        else
        {
            _plantingTimer = _timeToPlant;
        }


    }

    private void Player_OnLandWateringInteraction(object sender, EventArgs e)
    {
       
        
            _numberOfWaterShoots++;
            if (_numberOfWaterShoots == _maxNumberOfWaterShots )
            {
               
                Player.Instance.GetSelectedLand().SwitchLandStatus(LandStatus.Watered);
               

                if (landStatus == LandStatus.Watered)
                {
                    OnLandWatered?.Invoke(this, EventArgs.Empty);
                    
                }
                else
                {
                    _numberOfWaterShoots = 0;
                }




            }
           
        
    }

    private void Player_OnLandPlantingInteraction(object sender, EventArgs e)
    {
        _numberOfCropsPlanted++;
        if (CanLandBePlanted())
        {
            if (_numberOfCropsPlanted == _maxNumberOfCropsPlanted && _plantingTimer > 0)
            {
                Player.Instance.GetSelectedLand().SwitchLandStatus(LandStatus.Planted);
                _landIsPlanted = true;
                OnLandIsPlanted?.Invoke(this, EventArgs.Empty);
                
                if (landStatus != LandStatus.Planted)
                {
                    _landIsPlanted = false;
                    _numberOfCropsPlanted = 0;
                }
               
                
               
            }
        }
    }

          
           
      
                
    private void Player_OnLandDigingInteraction(object sender, EventArgs e)
    {
        
        if (CanLandBeDigged())
        {
            _hitsOnLand++;
            if (_hitsOnLand == _maxHitsOnLand)
            {
                
                Player.Instance.GetSelectedLand().SwitchLandStatus(LandStatus.Tiled);
                _hitsOnLand = 0;
               
                
               
            }

        }
        
        
    }
    public void SwitchLandStatus(LandStatus statusToSwitch)
    {
        
        
            landStatus = statusToSwitch;

            Material materialToSwitch = _soilMat;
            switch (statusToSwitch)
            {
                case LandStatus.Soil:
                    materialToSwitch = _soilMat;

                    break;
                case LandStatus.Tiled:
                    materialToSwitch = _tiledMat;
                break;
            case LandStatus.Planted:
                materialToSwitch = _tiledMat;
                    break;
                case LandStatus.Watered:

                    materialToSwitch = _wateredMat;
                    break;

            }

            _renderer.material = materialToSwitch;

    }
   public bool CanLandBeDigged()
    {
      
        _canLandBeDigged = landStatus == LandStatus.Soil && _hitsOnLand < _maxHitsOnLand;
        if (landStatus != LandStatus.Soil)
        {
            return !_canLandBeDigged;    
        }
       
        return _canLandBeDigged;
    }

    public bool CanLandBePlanted()
    {
        _canLandBePlanted = landStatus == LandStatus.Tiled && _numberOfCropsPlanted <= _maxNumberOfCropsPlanted;
      
       
        return _canLandBePlanted;
    }


   public void SpawnCropObject()
    {
        foreach (Transform spawnPoint in _cropsHnadler.GetCropSpawnPoints())
        {
            if ( _cropsHnadler.GetCropsObjectSo() != null)
            {
              Instantiate(_cropsHnadler.GetCropsObjectSo().CropPrefab, spawnPoint);
            }
        }
    }


  

    public HandleLandMaterials GetCurrentLand()
    {
        return Player.Instance.GetSelectedLand();
    }
    public LandStatus GetCurrentLandStatus()
    {
        return landStatus;
    }

    public LandStatus GetLandStatusThatCanBeDigged()
    {
        return LandStatus.Soil;
        
    }

    public LandStatus GetLandStatusThatCanBePlanted()
    {
        return LandStatus.Tiled;
    }

    public bool IsLandPlanted()
    {
        return _landIsPlanted;
    }

  
  

                
             
            
            

                   
               
            
           

               

            
                
                    
                    
                


                
   


        

      
       
       
           
        
        
        
        
        


   

  





}
