using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayScene(string SceneName) {
        SceneManager.LoadScene(SceneName);
        Time.timeScale = 1;
    }

    public void ExitGame() {
        Application.Quit();
    }
}
