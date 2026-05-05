using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SceneLoader : MonoBehaviour
{
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


    public void LoadScene()
    {
        if (sceneName != null)
        {
            SceneManager.Instance.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Scene asset is null. Please assign a valid scene.");
        }
    }

    public void LoadSceneInTime(float time)
    {
        Invoke(nameof(LoadScene), time);
    }
}
