using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeHandler : MonoBehaviour
{
    public string sceneName;


    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
