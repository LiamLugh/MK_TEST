using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseSystem : MonoBehaviour
{
    [SerializeField]
    Player p = null;
    [SerializeField]
    MapController m = null;
    [SerializeField]
    bool toggle = true;

    // UI elements
    [SerializeField]
    Image pauseScreen;

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
