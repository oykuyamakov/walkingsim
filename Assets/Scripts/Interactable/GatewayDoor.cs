using System;
using SceneManagement;
using Unity.VisualScripting;
using UnityEngine;

namespace Interactable
{
    public class GatewayDoor : MonoBehaviour
    {
        private SceneManager m_SceneManager => SceneManager.Instance;
        
        private bool m_PlayerEntered = false;
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log(other.tag);
            if (other.gameObject.CompareTag ("Player"))
            {
                Debug.Log("AMK");

                if (!m_PlayerEntered)
                {
                    m_PlayerEntered = true;

                    StartCoroutine(m_SceneManager.LoadScene(SceneName.TrippyScene));
                }
            }
            
        }
    }
}
