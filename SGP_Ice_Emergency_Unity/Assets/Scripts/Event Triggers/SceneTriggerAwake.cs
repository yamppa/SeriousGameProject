using UnityEngine;

public class SceneTriggerAwake : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;

    private void Awake()
    {
        if (sceneLoader != null)
        {
            sceneLoader.LoadSceneInTime(1);
        }
        
    }
}
