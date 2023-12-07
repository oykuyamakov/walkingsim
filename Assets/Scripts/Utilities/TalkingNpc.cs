using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UI;
using UnityEngine;
using Color = System.Drawing.Color;

namespace Utilities
{
    [RequireComponent(typeof(AudioSource))]
    public class TalkingNpc : MonoBehaviour
    {
        
        [SerializeField]
        private List<string> m_Lines;

        [SerializeField] private List<AudioClip> m_audioClips;

        private List<UnityEngine.Color> m_Pero = new List<UnityEngine.Color>()
        {
            UnityEngine.Color.black,
            UnityEngine.Color.grey,
            UnityEngine.Color.black,
            UnityEngine.Color.grey, 
            UnityEngine.Color.black,
            UnityEngine.Color.grey,
            UnityEngine.Color.black,
            UnityEngine.Color.grey,
            UnityEngine.Color.black,
            UnityEngine.Color.grey,
        };

        private FirstPersonController m_Player;

        private AudioSource m_Source => GetComponent<AudioSource>();
        
        private bool m_PlayerEntered = false;

        [SerializeField] private Renderer m_renderer;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (m_PlayerEntered) 
                    return;

                m_Player = other.GetComponent<FirstPersonController>();


                m_Player.MoveSpeed = 0;
                m_PlayerEntered = true;
                
                StartCoroutine(SendTexts());
            }
        }

       

        private IEnumerator SendTexts()
        {
            while (!m_renderer.isVisible)
            {
                yield return null;
            }
            
            for (int i = 0; i < m_Lines.Count; i++)
            {
                m_Source.PlayOneShot(m_audioClips[i]);
                
                SpeechUI.Instance.AddSpeech(m_Lines[i], m_Pero[i], 0.08f);
                
                yield return new WaitForSeconds((m_Lines[i].Length + 3) * 0.10f);

                while (!m_renderer.isVisible)
                {
                    yield return null;
                }
            }
            
        }
   
    }
}
