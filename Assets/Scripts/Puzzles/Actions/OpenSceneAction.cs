using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenSceneAction : Action
{
    [SerializeField] int sceneIndex = -1;
    public override void ExecuteAction(GameObject obejct)
    {
        Debug.Log("Change Scene");
        if (sceneIndex >= 0)SceneManager.LoadScene(sceneIndex);
    }
}
