using System;
using System.Collections.Generic;
using StarterAssets;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Video;

namespace Utilities
{
    [RequireComponent(typeof(VideoPlayer))]
    public class VideoWall : MonoBehaviour
    {
        [SerializeField] private List<VideoClip> m_Videos;
        private VideoPlayer m_VideoPlayer => GetComponent<VideoPlayer>();

        private int m_VideoIndex;
        private void Update()
        {
            //dunno wtf is happenin here but it is workin 
            m_VideoPlayer.playbackSpeed = math.remap(0,4f , 0f, 1f,FirstPersonController.Speed);

            // if (Math.Abs(m_VideoPlayer.length - m_VideoPlayer.time) > 0.1f) 
            //     return;
            //
            // m_VideoPlayer.clip = m_Videos[++m_VideoIndex% m_Videos.Count];
            

        }
    }
}
