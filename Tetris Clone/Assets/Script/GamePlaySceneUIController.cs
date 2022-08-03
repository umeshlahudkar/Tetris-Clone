using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlaySceneUIController : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreenPanel;

    public void onPauseButtonClick()
    {
        Time.timeScale = 0;
        SoundManager.Instance.playSfxSound(SoundManager.soundName.buttonClick);
        SoundManager.Instance.pauseSound();
        pauseScreenPanel.SetActive(true);
    }

    public void onResumeButtonClick()
    {
        Time.timeScale = 1;
        SoundManager.Instance.playSfxSound(SoundManager.soundName.buttonClick);
        SoundManager.Instance.unPauseSound();
        pauseScreenPanel.SetActive(false);
    }

    public void onMenuButtonClick()
    {
        Time.timeScale = 1;
        SoundManager.Instance.unPauseSound();
        SceneManager.LoadScene(0);
        SoundManager.Instance.playSfxSound(SoundManager.soundName.buttonClick);
        SoundManager.Instance.playBgSound(SoundManager.soundName.lobbyScene);
    }

    public void onReplayButtonClick()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(activeScene.buildIndex);
        SoundManager.Instance.playSfxSound(SoundManager.soundName.buttonClick);
        SoundManager.Instance.playBgSound(SoundManager.soundName.gamePlay);
    }
}
