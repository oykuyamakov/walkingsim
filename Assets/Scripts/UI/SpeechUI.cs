using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Color = System.Drawing.Color;

namespace UI
{
    public class SpeechUI : MonoBehaviour
    {
        public static SpeechUI Instance { get; private set; }
        
        #region Singleton

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);

        }

        #endregion

        [SerializeField] 
        private TextMeshProUGUI m_SpeechTextUI;

        private Queue<string> m_CurrentSpeechs = new Queue<string>();

        private bool m_Animating;

        private IEnumerator m_CurrCouritne;

        private bool m_Uninterruptable = false;
        

        [SerializeField]
        private float m_TextSpeed = 0.12f;
        
        private UnityEngine.Color Color = UnityEngine.Color.white;
        
        public void AddSpeech(string text, UnityEngine.Color cl, float speed = 0.12f)
        {
            if(m_Uninterruptable)
                return;
            
            m_CurrentSpeechs.Enqueue(text);
            
            if(m_Animating)
                return;
            
            m_CurrCouritne = AnimateSpeech(speed,cl);
            StartCoroutine(m_CurrCouritne);
        }

        public void InterruptAllSpeech(List<string> texts)
        {
            m_Uninterruptable = true;
            
            StopCoroutine(m_CurrCouritne);
            m_CurrentSpeechs.Clear();

            for (int i = 0; i < texts.Count; i++)
            {
                m_CurrentSpeechs.Enqueue(texts[i]);
            }
            
            StartCoroutine(m_CurrCouritne);
        }
        
        
        private IEnumerator AnimateSpeech(float speed, UnityEngine.Color cl)
        {
            m_Animating = true;

            while (m_CurrentSpeechs.Count > 0)
            {
                var text = m_CurrentSpeechs.Dequeue();

                var tempT = "";
                m_SpeechTextUI.color = cl;
                for (int i = 0; i < text.Length; i++)
                {
                    if (tempT.Length > 86)
                    {
                        yield return new WaitForSeconds(speed * 2);
                        tempT = "";
                    }
                    tempT += text[i];
                    m_SpeechTextUI.text = tempT;
                        
                    yield return new WaitForSeconds(speed);

                }
                
                yield return new WaitForSeconds(2);

                m_Animating = false;
                m_Uninterruptable = false;
                m_SpeechTextUI.text = "";
            }

           

        }
    }
}
