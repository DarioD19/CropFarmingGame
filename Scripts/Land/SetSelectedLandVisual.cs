using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSelectedLandVisual : MonoBehaviour
{
    [SerializeField] private HandleLandMaterials _baseLand;
    [SerializeField] private GameObject _visualGameObject;
    private void Start()
    {
        Player.Instance.OnSelectedLandChanged += Player_OnSelectedLandChanged;
    }

    private void Player_OnSelectedLandChanged(object sender, Player.OnSelectedLandChangedArgs e)
    {
        if (e.SelectedLand == _baseLand)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
    private void Show()
    {
        _visualGameObject.SetActive(true);
    }
    private void Hide()
    {
        _visualGameObject.SetActive(false);
    }
}
