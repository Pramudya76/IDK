using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Slider Enemyslider;
    public Slider Playerslider;
    public EnemyMovement EM;
    public PlayerMovement PM;
    public GameObject AudioSetting;
    // Start is called before the first frame update
    void Start()
    {
        AudioSetting.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       enemyHealth();
       playerHealth();

        if(Input.GetKeyDown(KeyCode.Escape)) {
            AudioSetting.gameObject.SetActive(true);
            Time.timeScale = 0;
        }

    }

    

    public void enemyHealth() {
        Enemyslider.value = EM.health;
    }

    public void playerHealth() {
        Playerslider.value = PM.healthPlayer;
    }
    

    public void RestartGame(string SceneName) {
        SceneManager.LoadScene(SceneName);
        Time.timeScale = 1;
    }

    public void MainMenu(string SceneName) {
        SceneManager.LoadScene(SceneName);
    }

    public void BackInGameAfterAudio() {
        AudioSetting.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

}
