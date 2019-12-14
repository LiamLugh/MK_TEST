using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseSystem : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    Player p = null;
    [SerializeField]
    MapController m = null;

    [Header("State")]
    [SerializeField]
    bool toggle = true;

    [Header("UI")]
    [SerializeField]
    Image pauseScreen = null;

    public void PauseToggle()
    {
        if(toggle)
        {
            PauseGame();
            pauseScreen.gameObject.SetActive(true);
        }
        else
        {
            UnpauseGame();
            pauseScreen.gameObject.SetActive(false);
        }

        toggle = !toggle;
    }

    void PauseGame()
    {
        p.Pause();
        m.Pause();
    }

    void UnpauseGame()
    {
        p.Unpause();
        m.Unpause();
    }
}
