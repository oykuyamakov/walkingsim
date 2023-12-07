using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Utilities
{
    public class BackgroundMover : MonoBehaviour
    {
        [SerializeField] 
        private Transform m_Background;

        [SerializeField] 
        private Material m_WoppyMaterial;
        
        private bool m_PlayerEntered = false;

        private Transform m_PlayerTransform;

        private float m_Diff;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (m_PlayerEntered) 
                    return;

                m_Diff = Mathf.Abs(m_Background.transform.position.z - other.transform.position.z);
                m_PlayerEntered = true;
                
                m_PlayerTransform = other.transform;
            }
        }

        private void Update()
        {
            if (m_PlayerEntered)
            {
                m_Background.transform.position = new Vector3(m_Background.transform.position.x,
                    m_Background.transform.position.y, m_PlayerTransform.position.z + m_Diff);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (m_PlayerEntered)
                {
                    //m_Background.parent = null;
                    m_PlayerEntered = false;

                    StartCoroutine(ChangeBackground());
                }
            }
        }

        private IEnumerator ChangeBackground()
        {
            yield return new WaitForSeconds(2);

            m_Background.GetComponent<SpriteRenderer>().material = m_WoppyMaterial;
        }
    }
}
