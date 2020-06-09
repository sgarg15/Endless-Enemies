using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

  public static bool gameIsPaused = false;


  public GameObject pauseMenuUI;
  public Slider[] volumeSliders;

  void Start(){
    volumeSliders [0].value = AudioManager.instance.masterVolumePercent;
    volumeSliders [1].value = AudioManager.instance.musicVolumePercent;
    volumeSliders [2].value = AudioManager.instance.sfxVolumePercent;
  }

  // Update is called once per frame
  void Update() {
    if(Input.GetKeyDown(KeyCode.Escape)){
      if(gameIsPaused){
        Resume();
      } else {
        Pause();
      }
    }
  }

  public void Resume(){
    pauseMenuUI.SetActive(false);
    Time.timeScale = 1f;
    gameIsPaused = false;
  }

  public void Pause(){
    pauseMenuUI.SetActive(true);
    Time.timeScale = 0f;
    gameIsPaused = true;
  }

  public void QuitGame(){
    Application.Quit();
  }

  public void SetMasterVolume(float value){
    AudioManager.instance.SetVolume (value, AudioManager.AudioChannel.Master);
  }

  public void SetMusicVolume(float value){
    AudioManager.instance.SetVolume (value, AudioManager.AudioChannel.Music);
  }

  public void SetSfxVolume(float value){
    AudioManager.instance.SetVolume (value, AudioManager.AudioChannel.Sfx);
  }
}
