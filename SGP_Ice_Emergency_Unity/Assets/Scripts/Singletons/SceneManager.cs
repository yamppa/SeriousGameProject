#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance { get; private set; }
#if UNITY_EDITOR
    [SerializeField] private SceneAsset sceneToLoad;
#endif

    string sceneName;


#if UNITY_EDITOR
    private void OnValidate()
    {
        if (sceneToLoad != null)
        {
            sceneName = sceneToLoad.name;
        }
    }
#endif

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void LoadScene(string scene)
    {
        if (scene != null)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
        }
        else
        {
            Debug.LogError("Scene asset is null. Please assign a valid scene.");
        }
    }

    public void RestartScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }


}
