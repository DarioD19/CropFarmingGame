using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSelectedSeedBoxVisual : MonoBehaviour
{
    [SerializeField] private SeedBoxContainer _seedBoxContainer;
    [SerializeField] private GameObject _visualGameObject;
    // Start is called before the first frame update
    void Start()
    {
        Player.Instance.OnSelectedSeedBoxChanged += Instance_OnSelectedSeedBoxChanged;
    }

    private void Instance_OnSelectedSeedBoxChanged(object sender, Player.OnSelectedSeedBoxContainerChangedArgs e)
    {
        if (e.SelectedSeedBoxContainer == _seedBoxContainer)
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
