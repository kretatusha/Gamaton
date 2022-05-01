using UnityEngine;
using UnityEngine.SceneManagement;

public class Da : MonoBehaviour
{
    private void Awake()
    {
        var Da = FindObjectOfType<Da>();
        if (Da && Da != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += (arg0, mode) =>
            GetComponent<AudioSource>().mute = arg0.buildIndex == 5 || arg0.buildIndex == 0;
    }
}