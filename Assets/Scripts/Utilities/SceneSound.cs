using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utilities
{
    [RequireComponent(typeof(AudioSource))]
    public class SceneSound : MonoBehaviour
    {
        [SerializeField]
        public AudioSource Source;

        [SerializeField]
        public bool AutoStart = true;

        [SerializeField] 
        private float m_MaxVolume = 1;

        private IEnumerator m_MusicStarterRoutine;

        public void StartMusic(bool autostart)
        {
            if (AutoStart != autostart)
                return;

            m_MusicStarterRoutine = StartMusic();
            StartCoroutine(m_MusicStarterRoutine);
        }

        public IEnumerator StartMusic()
        {
            
            Source.volume = 0;
            Source.Play();
            while (Source.volume < m_MaxVolume)
            {
                Source.volume += 0.2f * Time.deltaTime;

                yield return null;
            }

        }

        private void OnDestroy()
        {
            if (m_MusicStarterRoutine == null)
            {
                return;
            }
            
            StopCoroutine(m_MusicStarterRoutine);
        }

        public void StopMusic()
        {
            
        }
    }
}
