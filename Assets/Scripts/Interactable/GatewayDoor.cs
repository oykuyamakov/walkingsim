using System;
using SceneManagement;
using Unity.VisualScripting;
using UnityEngine;

namespace Interactable
{
    public class GatewayDoor : MonoBehaviour
    {
        [SerializeField] private SceneName m_SceneToGo = SceneName.CuteScene;
        
        private SceneManager m_SceneManager => SceneManager.Instance;
        
        private bool m_PlayerEntered = false;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag ("Player"))
            {
                Debug.Log(m_SceneToGo);

                if (!m_PlayerEntered)
                {
                    m_PlayerEntered = true;

                    StartCoroutine(m_SceneManager.LoadScene(m_SceneToGo));
                }
            }
            
        }
    }
}
