using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

namespace SceneManagement
{
    public class SceneManager : MonoBehaviour
    {
        public static SceneManager Instance { get; private set; }

        [SerializeField]
        private Material m_TrippySky;
        [SerializeField]
        private Material m_CuteSky;
        [SerializeField]
        private Material m_EndSky;
        
        #region Singleton

        public SceneName CurrentScene;

        public bool SkyBoxSwithicing;

        void Awake()
        {
            //Debug.LogError("I am an Error");
            
            //Debug.developerConsoleVisible = true;
            
            
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Debug.Log("SIC");
                Destroy(gameObject);
                Debug.Log("TIN");
                return;
            }

            DontDestroyOnLoad(gameObject);

            StartCoroutine(StartGame());

        }

        #endregion
        
        private SceneName m_LastScene = SceneName.Null;


        private IEnumerator StartGame()
        {
            yield return new WaitForSeconds(6);

            
            StartCoroutine(LoadScene(SceneName.Shared));

            yield return new WaitForSeconds(2);
            
            //Debug.Log("Now will be loading first scene?");
            
            m_LastScene = SceneName.IntroScene;

            StartCoroutine(LoadScene(SceneName.FirstScene));
            


            //StartCoroutine(UnLoadScene(SceneName.IntroScene));
        }
        
        public IEnumerator LoadScene(SceneName sceneName)
        {
            
            //Debug.Log(sceneName + "IS LOADING");
            
            var sceneSounds = FindObjectsOfType<SceneSound>();

            if (sceneName != SceneName.Shared)
            {
                var multiplier = sceneName == SceneName.FirstScene ? 5 : 1;
                
                foreach (var sceneSo in sceneSounds)
                {
                    multiplier = sceneSo.AutoStart ? 1 : 5;
                    Debug.Log(sceneSo.name+ "SoundScene founded, will be MUTED");

                    while (sceneSo.Source.volume > 0)
                    {
                        sceneSo.Source.volume -= 0.1f * Time.deltaTime * multiplier;
                    
                        yield return null;
                    }
                }
            }
            
            var op1 = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName.ToString(), LoadSceneMode.Additive);
            
            UnLoadPrevScene();

            while (!op1.isDone)
            {
                yield return null;
            }
            
            Debug.Log(sceneName + "LOADED");
            
            FirstPersonController.PlayerTransform.position = Vector3.zero;
            
            Debug.Log("player set");


            if (sceneName != SceneName.Shared)
            {
                m_LastScene = sceneName;
            }
            
            Debug.Log("anani sikem");
            
            Debug.Log("Po cant pass this point?");
            
            FirstPersonController.PlayerTransform.position = Vector3.zero;

            CurrentScene = sceneName;
            
            if(sceneName == SceneName.Shared)
                yield break;
            
            Debug.Log("Po or this?");
            
            //yield return new WaitForSeconds(1);
            
            Debug.Log("now will search for scene sounds");


            var newSceneSounds = FindObjectsOfType<SceneSound>();
            
            Debug.Log(newSceneSounds.Length + "SoundScene found");

            foreach (var nSs in newSceneSounds)
            {
                Debug.Log(nSs.name+ "SoundScene founded, will be played");

                nSs.StartMusic(true);
            }
            
            if (sceneName == SceneName.TrippyScene)
            {
                RenderSettings.skybox = m_TrippySky;
                Camera.main.clearFlags = CameraClearFlags.Skybox; 
            }
            else if (sceneName == SceneName.EndScene)
            {
                RenderSettings.skybox = m_EndSky;
                Camera.main.clearFlags = CameraClearFlags.Skybox;
            }
        }

        private void UnLoadPrevScene()
        {
            Debug.Log(m_LastScene.ToString());
            if (m_LastScene != SceneName.Null)
            {
                UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(m_LastScene.ToString());
            }
        } 
        private IEnumerator UnLoadScene(SceneName sceneName)
        {
            yield return new WaitForSeconds(1);
            
            UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(sceneName.ToString());
        }

    }

    public enum SceneName
    {
        Null,
        IntroScene,
        FirstScene,
        TrippyScene,
        CuteScene,
        Shared,
        EndScene,
    }
}
