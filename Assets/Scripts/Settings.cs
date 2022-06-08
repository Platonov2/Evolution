using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    public Slider mainSlider;

    public void VolumeSliderSetting()
    {
        Debug.Log(mainSlider.value);
        AudioListener.volume = mainSlider.value;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape key was pressed");
            SceneManager.LoadScene(2);
        }
    }
}
