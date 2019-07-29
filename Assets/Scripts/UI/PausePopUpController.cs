using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePopUpController : MonoBehaviour {

    private const float SoundChangeStep = 0.2f;


    public void OnPlusSoundClicked()
    {
        AudioListener.volume += SoundChangeStep;
    }

    public void OnMinuseSoundClicked()
    {
        AudioListener.volume -= SoundChangeStep;
    }
}
