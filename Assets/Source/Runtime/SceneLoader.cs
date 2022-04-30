using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private int _nextSceneIndex;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.GetComponent<BotCompositionRoot>())
            LoadNextScene();
    }

    public void LoadNextScene() => SceneManager.LoadSceneAsync(_nextSceneIndex);

    public void Exit() => Application.Quit();
}
