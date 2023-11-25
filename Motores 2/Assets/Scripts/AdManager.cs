using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsListener
{
    string _gameID = "5484290", _adID = "Rewarded_Android";

    [SerializeField] CustomJsonSaveSystem _json;
    [SerializeField] GameObject _addEnergyButton;

    private void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(_gameID);
    }

    private void Update()
    {
        if (Advertisement.IsReady() && _json.saveData._energy < 5)
        {
            _addEnergyButton.SetActive(true);
        }
        else
        {
            _addEnergyButton.SetActive(false);
        }
    }

    public void ShowAd()
    {
        Advertisement.Show(_adID);
    }

    public void OnUnityAdsDidError(string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (placementId == _adID)
        {
            if (showResult == ShowResult.Finished) _json.GainEnergy(2);
            else if (showResult == ShowResult.Skipped) _json.GainEnergy(1);
        }
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsReady(string placementId)
    {
        
    }
}
