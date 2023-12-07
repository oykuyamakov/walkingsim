using System;
using System.Collections;
using System.Collections.Generic;
using SceneManagement;
using StarterAssets;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using SceneManager = SceneManagement.SceneManager;

namespace Utilities
{
    public class InteractiveNPC : MonoBehaviour
    {
        [SerializeField]
        private List<string> m_Lines;

        [SerializeField] 
        private AudioClip m_Sound;

        [SerializeField] 
        private bool m_HasSound;
        

        [SerializeField] 
        private bool m_DestroyColliders;

        [SerializeField] 
        private bool m_ChangeSkybox;
        
        [SerializeField] 
        private Material m_Skybox;

        [SerializeField] 
        private GameObject m_Ground;

        [SerializeField] 
        private TerrainCollider m_terrainCollider;

        [SerializeField] 
        private AudioSource m_LSDSource;
        [SerializeField] 
        private AudioSource m_TransitionSource;
        
        private bool m_PlayerEntered = false;
        private IEnumerator m_SkyBoxRoutine;


        [SerializeField]
        private bool m_Uninterruptable = false;

        [SerializeField] private bool m_Crazy = false;

        private FirstPersonController m_Pl;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {

                if (m_PlayerEntered) 
                    return;


                m_PlayerEntered = true;

                if (m_DestroyColliders || m_Crazy)
                {
                    m_Pl = other.GetComponent<FirstPersonController>();
                    m_Pl.MoveSpeed = 0;
                }

                if (m_HasSound && m_Sound != null && !m_Crazy)
                {
                    if (this.gameObject.TryGetComponent<AudioSource>(out var source))
                    {
                        if (!source.isPlaying)
                        {
                            
                            source.PlayOneShot(m_Sound);
                        }
                    }
                    else
                    {
                        var sourceC = this.gameObject.AddComponent<AudioSource>();
                        sourceC.PlayOneShot(m_Sound);
                    
                    }
                }
               

                
                StartCoroutine(SendTexts());
            }
        }

        private int m_SkyCounter = 0;

        private IEnumerator SwitchBox()
        {
            while (SceneManager.Instance.SkyBoxSwithicing)
            {
                
                if(!SceneManager.Instance.SkyBoxSwithicing)
                    yield break;
                
                RenderSettings.skybox = m_SkyCounter++ % 2 == 0 ? null : m_Skybox ;

                yield return new WaitForSeconds(1);
            }
        }

        private void OnDestroy()
        {
            if(m_SkyBoxRoutine == null)
                return;
            
            StopCoroutine(m_SkyBoxRoutine);
        }

        private IEnumerator SendTexts()
        {
            if (m_Uninterruptable)
            {
                SpeechUI.Instance.InterruptAllSpeech(m_Lines);
            }
            else
            {
                for (int i = 0; i < m_Lines.Count; i++)
                {
                    var color = SceneManager.Instance.CurrentScene == SceneName.CuteScene && m_DestroyColliders ? 
                        Color.magenta : Color.white;
                    SpeechUI.Instance.AddSpeech(m_Lines[i], color, 0.08f);
                }
            }
            
            var allLenght = 0;
            foreach (var line in m_Lines)
            {
                allLenght += line.Length;
            }
            yield return new WaitForSeconds((allLenght +3) * 0.08f);

            if (m_Crazy)
            {
                m_Pl.MoveSpeed = 4;
            }
            
            if (m_ChangeSkybox && m_Skybox != null)
            {
                
                
                RenderSettings.skybox = m_Skybox;
                Camera.main.clearFlags = CameraClearFlags.Skybox;

                if (m_Crazy && !SceneManager.Instance.SkyBoxSwithicing)
                {
                    SceneManager.Instance.SkyBoxSwithicing = true;
                    
                    m_SkyBoxRoutine =  SwitchBox();
                    StartCoroutine(m_SkyBoxRoutine);


                }

            }
            
            if (m_DestroyColliders)
            {   
                SceneManager.Instance.SkyBoxSwithicing = false;
                
                m_LSDSource.Play();
                
                yield return new WaitForSeconds(2);

                if (SceneManager.Instance.CurrentScene == SceneName.CuteScene)
                {
                    m_Pl.MoveSpeed = 9;
                }
                else
                {
                    m_Pl.MoveSpeed = 4;
                }
               
                m_Ground.SetActive(false);
                
                if (m_TransitionSource != null)
                {
                    m_TransitionSource.Play();
                }
                
                if (m_terrainCollider != null)
                {
                    m_terrainCollider.enabled = false;
                }
            }
            else
            {
                m_PlayerEntered = false;
            }

           
        }
    }
}
