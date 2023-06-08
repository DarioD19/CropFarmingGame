using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropsObject : MonoBehaviour
{
    [SerializeField] private CropsObjectSO _cropsObjectSO;
    private float _timeDuration = 4 * 40;
    private float _timer;
    private float _growTime = 20f;

    private bool _cropIsFullGrown;



    Vector3 startScale = new Vector3(0.01f, 0.22f, 0.01f);

   

   
    private void Start()
    {
        ResetTimer();
        transform.localScale = startScale;
    }

    private void Update()
    {

        
        Debug.Log(_timer);
        if (!_cropIsFullGrown)
        {
            HandleCropGrowing();
        }
      


    }

    private ISpawnCrop _cropObjectParent;

    private void ResetTimer()
    {
        _timer = _timeDuration;
    }
    private void TimerCountdown()
    {
        _timer -= Time.deltaTime;
    }

    public void SetCropObjectParent(ISpawnCrop cropObjectParent)
    {
        if (this._cropObjectParent != null)
        {
            this._cropObjectParent.ClearCropsObject();
        }
        this._cropObjectParent = cropObjectParent;

        cropObjectParent.SetSetCropObject(this);
        transform.parent = cropObjectParent.GetCropObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public  CropsObject SpawnCropObject(CropsObjectSO cropObjectSO, ISpawnCrop cropObjectParent)
    {
        Transform cropObjectTransform = Instantiate(cropObjectSO.CropPrefab);
        CropsObject cropsObject = cropObjectTransform.GetComponent<CropsObject>();
        cropsObject.SetCropObjectParent(cropObjectParent);

        return cropsObject;
    }



    private void HandleCropGrowing()
    {
       
        Vector3 maxCropScaleGrow = new Vector3(0.14f, 0.88f, 0.17f);
        _cropIsFullGrown = _timer <= 30f;


        if (_timer <= 5)
        {
            _timer = 0f;
        }
        else
        {
            TimerCountdown();


            if (_timer < 120 && _timer > 90f)
            {
                transform.localScale = Vector3.Lerp(startScale, maxCropScaleGrow / 3, _timer / _growTime);

            }
            if (_timer < 90f && _timer > 60f)
            {
                transform.localScale = Vector3.Lerp(startScale, maxCropScaleGrow / 2, _timer / _growTime);
            }
            if (_cropIsFullGrown)
            {
                transform.localScale = Vector3.Lerp(startScale, maxCropScaleGrow, _timer / _growTime);
            }

        }
      
        

     

        


    }


}
