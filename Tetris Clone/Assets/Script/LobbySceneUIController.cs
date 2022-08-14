using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LobbySceneUIController : MonoBehaviour
{
    [SerializeField] private GameObject levelSelectScreen;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private GameObject scorePanel;
    [SerializeField] private GameObject audioPanel;
    [SerializeField] private GameObject settingScreenPanel;


    public void onPlayButtonClick()
    {
        SoundManager.Instance.playSfxSound(SoundManager.soundName.buttonClick);
        levelSelectScreen.SetActive(true);
    }

    public void onEnterButtonClick()
    {
        SoundManager.Instance.playSfxSound(SoundManager.soundName.buttonClick);
        SceneManager.LoadScene("GamePlay");
        SoundManager.Instance.playBgSound(SoundManager.soundName.gamePlay);
    }

    public void onQuitButtonClick()
    {
        SoundManager.Instance.playSfxSound(SoundManager.soundName.buttonClick);
        Application.Quit();
    }

    public void onSliderSelect(float value)
    {
        SoundManager.Instance.playSfxSound(SoundManager.soundName.buttonClick);
        levelText.text = value.ToString();
        LevelController.Instance.setLevel((int)value);

    }

    public void onBgSoundMuteClick(bool mute)
    {
        SoundManager.Instance.playSfxSound(SoundManager.soundName.buttonClick);
        SoundManager.Instance.bgSourceMute(mute);
    }

    public void onSFXSoundMuteClick(bool mute)
    {
        SoundManager.Instance.playSfxSound(SoundManager.soundName.buttonClick);
        SoundManager.Instance.sfxSourceMute(mute);
    }

    public void onBgSoundVolumeClick(float value)
    {
        SoundManager.Instance.setBgSourceVolume(value);
    }

    public void onSFXSoundMuteClick(float value)
    {
        SoundManager.Instance.setSfxSourceVolume(value);
    }

    public void onBackButtonClick()
    {
        settingScreenPanel.SetActive(false);
        scorePanel.SetActive(false);
        audioPanel.SetActive(false);
        SoundManager.Instance.playSfxSound(SoundManager.soundName.buttonClick);
    }

    public void onScoreButtonClick()
    {
        scorePanel.SetActive(true);
        SoundManager.Instance.playSfxSound(SoundManager.soundName.buttonClick);
    }

    public void onAudioButtonClick()
    {
        audioPanel.SetActive(true);
        SoundManager.Instance.playSfxSound(SoundManager.soundName.buttonClick);
    }

    public void onSettingButtonClick()
    {
        settingScreenPanel.SetActive(true);
        SoundManager.Instance.playSfxSound(SoundManager.soundName.buttonClick);
    }
}
