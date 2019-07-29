using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSceneUIController : MonoBehaviour {

    [SerializeField] private GameObject PausePopUp;

    private void Awake()
    {
        PausePopUp.SetActive(false);
    }

    public void OnPauseButtonClicker()
    {
        Time.timeScale = 0;
        PausePopUp.SetActive(true);
    }

    public void OnResumedButtonClicker()
    {
        Time.timeScale = 1;
        PausePopUp.SetActive(false);
    }
}
