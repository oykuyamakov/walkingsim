using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MenuUI : MonoBehaviour
    {
        private Canvas m_Canvas => GetComponent<Canvas>();

        [SerializeField]
        private Button m_ButtonQuit;
        [SerializeField]
        private Button m_ButtonResume;

        private bool m_MenuEnabled = false;

        private void Awake()
        {
            m_ButtonQuit.onClick.RemoveAllListeners();
            m_ButtonResume.onClick.RemoveAllListeners();
            
            m_ButtonQuit.onClick.AddListener(Quit);
            m_ButtonResume.onClick.AddListener(Resume);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                m_MenuEnabled = !m_MenuEnabled;
                
                ToggleMenu();

            }        
            
            
        }

        public void Resume()
        {
            m_Canvas.enabled = false;
            m_MenuEnabled = false;
        }

        public void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
	Application.Quit();
#endif
        }
        
        private void ToggleMenu()
        {
           
                m_Canvas.enabled = m_MenuEnabled;
        }
    }
}
