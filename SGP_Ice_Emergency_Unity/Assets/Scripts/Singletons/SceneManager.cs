using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance { get; private set; }

    [SerializeField] private SceneAsset sceneToLoad;

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

    public void LoadScene(SceneAsset scene)
    {
        if (scene != null)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(scene.name);
        }
        else
        {
            Debug.LogError("Scene asset is null. Please assign a valid scene.");
        }
    }

    public void RestartScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToLoad.name);
    }


}
