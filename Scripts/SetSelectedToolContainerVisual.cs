using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSelectedToolContainerVisual : MonoBehaviour
{
    [SerializeField] private ToolContainer _toolContainer;
    [SerializeField] private GameObject _containerVisual;

    private void Start()
    {
        Player.Instance.OnSelectedToolContainerChanged += Player_OnSelectedToolContainerChanged;
    }

    private void Player_OnSelectedToolContainerChanged(object sender, Player.OnSelectedToolContainerChangedArgs e)
    {
        if (e.SelectedToolContainer == _toolContainer)
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
        _containerVisual.gameObject.SetActive(true);
    }

    private void Hide()
    {
        _containerVisual.gameObject.SetActive(false);
    }
}
