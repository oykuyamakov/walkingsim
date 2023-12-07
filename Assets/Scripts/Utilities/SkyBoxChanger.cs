using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using RenderSettings = UnityEngine.RenderSettings;

namespace Utilities
{
    public class SkyBoxChanger : MonoBehaviour
    {
        [SerializeField] 
        private Material m_Skybox;
        
        private bool m_PlayerEntered = false;

        [SerializeField] 
        private GameObject m_LighttoTurnOff;

        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (m_PlayerEntered) 
                    return;

                m_LighttoTurnOff?.SetActive(false);
                RenderSettings.skybox = m_Skybox;
                Camera.main.clearFlags = CameraClearFlags.Skybox;
            }
        }
    }
}
