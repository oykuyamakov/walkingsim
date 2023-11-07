using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneManagement
{
    public class SceneManager : MonoBehaviour
    {
        public static SceneManager Instance { get; private set; }
        
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

            m_LastScene = SceneName.FirstScene;
        }

        #endregion
        
        private SceneName m_LastScene = SceneName.Null;

        public IEnumerator LoadScene(SceneName sceneName)
        {
            var op1 = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName.ToString(), LoadSceneMode.Additive);

            while (!op1.isDone)
            {
                yield return null;
            }

            yield return new WaitForSeconds(1);
            
            UnLoadPrevScene(m_LastScene);

        }

        private void UnLoadPrevScene(SceneName sceneName)
        {
            if (m_LastScene != SceneName.Null)
            {
                UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(m_LastScene.ToString());
            }
        }

    }

    public enum SceneName
    {
        Null,
        IntroScene,
        FirstScene,
        TrippyScene
    }
}
