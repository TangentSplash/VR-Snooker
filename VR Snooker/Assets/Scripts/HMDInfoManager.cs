using UnityEngine;
using UnityEngine.XR;

public class HMDInfoManager : MonoBehaviour
{
    [SerializeField] private GameObject MockHMD;
    void Start()
    {
        Debug.Log("Active " + XRSettings.isDeviceActive);
        Debug.Log(XRSettings.loadedDeviceName);
        if (!XRSettings.isDeviceActive)
        {
            Debug.Log("Using Mock HMD");
            MockHMD.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
