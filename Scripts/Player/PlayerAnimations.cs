using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";
    private const string IS_DIGING = "IsDiging";
    private const string IS_PLANTING = "IsPlanting";
    private const string IS_WATERING = "IsWatering";
    
    private Animator _animator;
    [SerializeField] private Player _player;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
   

    private void Update()
    {
        HandleAnimations();
    }

    private void HandleAnimations()
    {
        _animator.SetBool(IS_WALKING,_player.IsWalking());
        _animator.SetBool(IS_DIGING, _player.IsDiging());
        _animator.SetBool(IS_PLANTING, _player.IsPlanting());
        _animator.SetBool(IS_WATERING, _player.IsWateringTheLand());

    }

  
    
     
        
    
}
