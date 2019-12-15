using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleSeedUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    Image checkImage = null;
    [SerializeField]
    InputField seedInput = null;
    [SerializeField]
    string currentSeed = "";
    bool useSeed = true;

    void Start()
    {
        seedInput.onEndEdit.AddListener(delegate { UpdateSeed(seedInput); });
    }

    private void UpdateSeed(InputField seedInput)
    {
        currentSeed = seedInput.text.ToString();
    }

    public void UseSeedToggle()
    {
        if(useSeed)
        {
            checkImage.gameObject.SetActive(false);
            seedInput.gameObject.SetActive(true);

        }
        else
        {
            checkImage.gameObject.SetActive(true);
            seedInput.gameObject.SetActive(false);
            currentSeed = "";
        }

        useSeed = !useSeed;
    }

    public bool GetUseSeed()
    {
        return useSeed;
    }

    public string GetSeed()
    {
        return currentSeed;
    }
}
