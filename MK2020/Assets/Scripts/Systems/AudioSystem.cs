using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSystem : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField]
    AudioSource sfxSource = null;
    [SerializeField]
    AudioSource musicSource = null;
    [SerializeField]
    AudioSource deathSource = null;
    [SerializeField]
    AudioSource jumpChargeSource = null;

    [Header("Sound Files")]
    [SerializeField]
    /*
     *      0 - GOOD COIN
     *      1 - BAD COIN
     *      2 - LANDING
     *      3 - MENU SELECT
     *      4 - PAUSE
     *      5 - UNPAUSE
     *      6 - GAME OVER
     *      7 - JUMP
     *      8 - EXPLODE
     *      8 - SWITCH
     */
    AudioClip[] sfx = null;

    [Header("UI")]
    [SerializeField]
    Slider sfxSlider;
    [SerializeField]
    Slider musicSlider;

    void Awake()
    {
        // UI classes use unity events, so delegates TODO --- SAVE THESE TO PLAYER PREFS
        sfxSlider.onValueChanged.AddListener((float v) => SetSFXVolume(v));
        musicSlider.onValueChanged.AddListener((float v) => SetMusicVolume(v));
    }

    void SetSFXVolume(float v)
    {
        sfxSource.volume = v;
        deathSource.volume = v;
        jumpChargeSource.volume = v;
    }

    void SetMusicVolume(float v)
    {
        musicSource.volume = v;
    }

    public void PlayGoodCoinSFX()
    {
        sfxSource.PlayOneShot(sfx[0]);
    }

    public void PlayBadCoinSFX()
    {
        sfxSource.PlayOneShot(sfx[1]);
    }

    public void PlayLandingSFX()
    {
        sfxSource.PlayOneShot(sfx[2]);
    }

    public void PlayMenuSelectSFX()
    {
        sfxSource.PlayOneShot(sfx[3]);
    }

    public void PlayPauseSFX()
    {
        sfxSource.PlayOneShot(sfx[4]);
        musicSource.Pause();
        deathSource.Pause();
        jumpChargeSource.Pause();
    }

    public void PlayUnpauseSFX()
    {
        sfxSource.PlayOneShot(sfx[5]);
        musicSource.UnPause();
        deathSource.UnPause();
        jumpChargeSource.UnPause();
    }

    public void PlayGameOverSFX()
    {
        sfxSource.PlayOneShot(sfx[6]);
        musicSource.Pause();
        deathSource.Pause();
        jumpChargeSource.Pause();
    }

    public void PlayJumpSFX()
    {
        sfxSource.PlayOneShot(sfx[7]);
    }

    public void PlayExplodeSFX()
    {
        sfxSource.PlayOneShot(sfx[8]);
    }

    public void PlaySwitchSFX()
    {
        sfxSource.PlayOneShot(sfx[9]);
    }

    public void UpdateWarningSFX(float value)
    {
        deathSource.pitch = (value / 1.0f) * -3.0f;
    }

    public void ResetWarningSFX()
    {
        deathSource.pitch = 0.0f;
    }

    public void UpdateJumpChargeSFX(float value)
    {
        jumpChargeSource.pitch = (value / 1.0f) * 3.0f;
    }

    public void ResetJumpChargeSFX()
    {
        jumpChargeSource.pitch = 0.0f;
    }

    public void MuteSFXToggle()
    {
        sfxSource.mute = !sfxSource.mute;
        deathSource.mute = !deathSource.mute;
        jumpChargeSource.mute = !jumpChargeSource.mute;
    }

    public void MuteMusicToggle()
    {
        musicSource.mute = !musicSource.mute;
    }
}
