using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField]
    AudioSource sfxSource = null;
    [SerializeField]
    AudioSource musicSource = null;
    [SerializeField]
    AudioSource deathSource = null;

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
     */
    AudioClip[] sfx = null;

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
    }

    public void PlayUnpauseSFX()
    {
        sfxSource.PlayOneShot(sfx[5]);
        musicSource.UnPause();
        deathSource.UnPause();
    }

    public void PlayGameOverSFX()
    {
        sfxSource.PlayOneShot(sfx[6]);
        musicSource.Pause();
        deathSource.Pause();
    }

    public void PlayJumpSFX()
    {
        sfxSource.PlayOneShot(sfx[7]);
    }

    public void UpdateWarningSFX(float value)
    {
        deathSource.pitch = (value / 1.0f) * -3.0f;
    }

    public void ResetWarningSFX()
    {
        deathSource.pitch = 0.0f;
    }
}
