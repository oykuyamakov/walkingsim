using System;
using UnityEngine;

namespace Interactable
{
    [RequireComponent(typeof(Animator))]
    public class BeKindAnimController : MonoBehaviour
    {
        [SerializeField]
        private string m_AnimName;

        private void Awake()
        {
            GetComponent<Animator>().SetBool(m_AnimName.ToString(),true);
        }
    }
}
