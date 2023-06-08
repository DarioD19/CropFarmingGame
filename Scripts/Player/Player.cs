using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour,ISpawnTool,ISpawnBox
{
    private static Player _instance;
    public static Player Instance { 
      get
        {
            if (_instance == null)
            {
                SetupInstance();
            }
            return _instance;
        }
    
    }

    public event EventHandler OnLandDigingInteraction;
    public event EventHandler OnLandPlantingInteraction;
    public event EventHandler OnLandWateringInteraction;
    

   
    public event EventHandler<OnSelectedLandChangedArgs> OnSelectedLandChanged;
    public class OnSelectedLandChangedArgs : EventArgs
    {
        public HandleLandMaterials SelectedLand;
    }

    public event EventHandler<OnSelectedToolContainerChangedArgs> OnSelectedToolContainerChanged;
    public class OnSelectedToolContainerChangedArgs : EventArgs
    {
        public ToolContainer SelectedToolContainer;
    }
    public event EventHandler<OnSelectedSeedBoxContainerChangedArgs> OnSelectedSeedBoxChanged;
    public class OnSelectedSeedBoxContainerChangedArgs : EventArgs
    {
        public SeedBoxContainer SelectedSeedBoxContainer;
    }
    [SerializeField] private GameInput _gameInput;
    
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private LayerMask _groundLayerMask, _pickaxeLayerMask,_cabbageSeedBoxLayer,_waterCanLayerMask;
    [SerializeField] private LayerMask _tomatoBoxLayerMask;
    [SerializeField] private Transform _toolObjectHoldPoint;
    [SerializeField] private Transform _boxObjectHoldPoint;

    

    private HandleLandMaterials _selectedLand;
    private ToolObject _toolObject;
    private ToolContainer _selectedToolContainer;
    private SeedBoxContainer _selectedSeedBoxContainer;
    private SeedBoxObject _seedBoxObject;
   

    private bool _isDiging;
    private bool _isWalking;
    private bool _isPlantingTheLand;
    private bool _isWateringTheLand;
    private Vector3 _moveDirection;
    


    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        
    }

    private static void SetupInstance()
    {
        _instance = FindObjectOfType<Player>();
        if (_instance == null)
        {
            GameObject gameObj = new GameObject();
            gameObj.name = "Player";
            _instance = gameObj.AddComponent<Player>();
            DontDestroyOnLoad(gameObj);
        }
    }
    
    private void Update()
    {
        HandleMovement();
        HandleInteractionsWithLand();
        TakeOrDropTool();
        TakeOrDropBox();
        Dig();
        if (_selectedLand != null)
        {
            Plant();
        }
       
        WaterTheLand();

    }
    private void HandleInteractionsWithLand()
    {
        Ray ray;
        
        ray = new Ray(transform.position, Vector3.down * 1.7f);
        if (Physics.Raycast(ray,out RaycastHit hitInfo))
        {
            if (hitInfo.transform.TryGetComponent(out HandleLandMaterials selectedLand))
            {
                if (selectedLand != _selectedLand)
                {
                    SetSelectedLand(selectedLand);

                }
            }
                   
            else
            {
                SetSelectedLand(null);
               
            }
            if (hitInfo.transform.TryGetComponent(out ToolContainer selectedToolContainer))
            {
                if (selectedToolContainer != _selectedToolContainer)
                {
                    SetSelectedToolContainer(selectedToolContainer);
                }
            }
            else
            {
                SetSelectedToolContainer(null);
            }
            if (hitInfo.transform.TryGetComponent(out SeedBoxContainer selectedBoxContainer))
            {
                if (selectedBoxContainer != _selectedSeedBoxContainer)
                {
                    SetSelectedBoxContainer(selectedBoxContainer);
                }
            }
            else
            {
                SetSelectedBoxContainer(null);
            }
                
        }
      
        else
        {
            SetSelectedLand(null);
            SetSelectedToolContainer(null);
            SetSelectedBoxContainer(null);
            
        }
            
             
            

        Debug.Log(_selectedLand);
    }
    private void HandleMovement()
    {
        Vector2 inputVector = _gameInput.GetMovementVectorNormalized();
        Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);
        Vector3 offset = new Vector3(0f, 0.3f, 0f);


        //Checking if player is in front of something
        float playerRadius = 0.3f;
        float moveDistance = _moveSpeed * Time.deltaTime;
        float playerHeight = 1f;
        bool canMove = !Physics.CapsuleCast(transform.position + offset, transform.position + Vector3.up * playerHeight, playerRadius, moveDirection, moveDistance);
        if (!canMove)
        {//Cannot move towards moveDirection
            //Attempt movement on x Axis
            Vector3 moveDirectionX = new Vector3(moveDirection.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionX, moveDistance);
            if (canMove)
            {
                moveDirection = moveDirectionX;
            }
            else
            {
                Vector3 moveDirectionZ = new Vector3(0f, 0f, moveDirection.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionZ, moveDistance);
                if (canMove)
                {
                    moveDirection = moveDirectionZ;
                }
                else
                {

                }
            }

        }
        if (canMove)
        {
            transform.position += moveDirection * moveDistance;
        }
       
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * _rotationSpeed);
        _isWalking = moveDirection != Vector3.zero;
        _moveDirection = moveDirection;

    }

    public bool IsWalking()
    {
        return _isWalking;
    }
    public bool IsDiging()
    {
        return _isDiging;
    }

    public bool IsPlanting()
    {
        return _isPlantingTheLand;
    }

    public bool IsWateringTheLand()
    {
        return _isWateringTheLand;
    }

    private void Dig()
    {

        //Get number of hits to land
        _isDiging = Input.GetKeyDown(KeyCode.E) && !IsWalking() && GetSelectedLand() != null && GetSelectedLand().GetCurrentLandStatus() == GetSelectedLand().GetLandStatusThatCanBeDigged() && HasToolObject() && GetToolObject().GetToolObjectSO().ToolLayer == _pickaxeLayerMask;
        
        if (_isDiging)
        {
            
                OnLandDigingInteraction?.Invoke(this, EventArgs.Empty);

        }
          

    }

    private void Plant()
    {
        _isPlantingTheLand = Input.GetKeyDown(KeyCode.E) && !IsWalking() && GetSelectedLand() != null && GetSelectedLand().GetCurrentLandStatus() == HandleLandMaterials.LandStatus.Tiled && HasBoxObject() && !HasToolObject();
       
        if (_isPlantingTheLand)
        {
          
            OnLandPlantingInteraction?.Invoke(this, EventArgs.Empty);
        }
    }

    private void WaterTheLand()
    {
        _isWateringTheLand = Input.GetKeyDown(KeyCode.E) && !IsWalking() && GetSelectedLand() != null && GetSelectedLand().GetCurrentLandStatus() == HandleLandMaterials.LandStatus.Planted && HasToolObject() && !HasBoxObject() && GetToolObject().GetToolObjectSO().ToolLayer == _waterCanLayerMask;
        if (_isWateringTheLand)
        {
            OnLandWateringInteraction.Invoke(this, EventArgs.Empty);
        }
    }

          
    private void SetSelectedLand(HandleLandMaterials selectedLand)
    {
        this. _selectedLand = selectedLand;
        OnSelectedLandChanged?.Invoke(this, new OnSelectedLandChangedArgs
        {
            SelectedLand = selectedLand
              

        }) ;
    }
    private void SetSelectedToolContainer(ToolContainer selectedToolContainer)
    {
        this._selectedToolContainer = selectedToolContainer;
        OnSelectedToolContainerChanged?.Invoke(this, new OnSelectedToolContainerChangedArgs
        {
            SelectedToolContainer = selectedToolContainer
        }) ;
    }

    private void SetSelectedBoxContainer(SeedBoxContainer selectedBoxContainer)
    {
        this._selectedSeedBoxContainer = selectedBoxContainer;
        OnSelectedSeedBoxChanged?.Invoke(this, new OnSelectedSeedBoxContainerChangedArgs
        {
            SelectedSeedBoxContainer = selectedBoxContainer
        }) ;
    }
        
       
    public void TakeOrDropTool()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (_selectedToolContainer != null)
            {
                _selectedToolContainer.Interact(this);
            }
          
            
        }
    }

    public void TakeOrDropBox()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (_selectedSeedBoxContainer != null)
            {
                _selectedSeedBoxContainer.Interact(this);
            }
        }
    }

      

    public HandleLandMaterials GetSelectedLand()
    {
        return  _selectedLand;
    }

    public Transform GetToolObjectFollowTransform()
    {
        return _toolObjectHoldPoint;
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

    public Transform GetBoxObjectFollowTransform()
    {
        return _boxObjectHoldPoint;
    }

    public void SetBoxObject(SeedBoxObject seedBoxObject)
    {
        this._seedBoxObject = seedBoxObject;
    }

    public SeedBoxObject GetBoxObject()
    {
        return _seedBoxObject;
    }

    public void ClearBoxObject()
    {
        _seedBoxObject = null;
    }

    public bool HasBoxObject()
    {
        return _seedBoxObject != null;
    }

    public Vector3 GetrMovmentDirection()
    {
        return _moveDirection;
    }

   
                      
   
    
  
    



    

       
        
        

        
       
                    

        
                
              
               
                
                    
                        
                    
                    
       


        
        
     

   

































}
