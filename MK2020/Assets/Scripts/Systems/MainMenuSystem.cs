using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSystem : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    DataSystem dataSystem = null;
    [SerializeField]
    ToggleSeedUI uiData = null;

    void Awake()
    {
        dataSystem = GameObject.FindGameObjectWithTag("Data").GetComponent<DataSystem>();
        dataSystem.Reset();
    }

    public void Play()
    {
        // Assign seed to singleton if needed
        if(uiData.GetUseRandomSeed())
        {
            dataSystem.SetSeed(uiData.GetSeed());
        }

        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
