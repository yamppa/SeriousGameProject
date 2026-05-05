using UnityEngine;
using UnityEditor;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private SceneAsset sceneToLoad;



    public void LoadScene()
    {
        if (sceneToLoad != null)
        {
            SceneManager.Instance.LoadScene(sceneToLoad);
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
