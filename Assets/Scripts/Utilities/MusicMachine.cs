using System;
using UnityEngine;

namespace Utilities
{
    [RequireComponent(typeof(SceneSound))]
    public class MusicMachine : MonoBehaviour
    {
        private bool m_PlayerEntered = false;
        private SceneSound m_SceneSound => GetComponent<SceneSound>();
        

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag ("Player"))
            {
                if (!m_PlayerEntered)
                {
                    m_PlayerEntered = true;
                    m_SceneSound.StartMusic(false);

                }
            }
            
        }
    }
}
