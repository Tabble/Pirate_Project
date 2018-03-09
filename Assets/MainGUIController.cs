using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGUIController : MonoBehaviour {

    public Text EnergyText;
    public Text GameOverText;

    private const string ENERGY_TEXT = "Energy: ";
    private const string WIN_TEXT = "GEWONNEN";
    private const string LOSS_TEXT = "VERLOREN";

    private static MainGUIController instance;
    public static MainGUIController Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<MainGUIController>();
            }
            return instance;
        }
    }

	// Use this for initialization
	void Start () {
        GameOverText.gameObject.SetActive(false);
	}

    public void SetEnergy(float energy)
    {
        EnergyText.text = ENERGY_TEXT + energy.ToString();
    }

    public void ShowGameoverText(bool win)
    {
        GameOverText.gameObject.SetActive(true);
        if (win)
        {
            GameOverText.text = WIN_TEXT;
        }
        else
        {
            GameOverText.text = LOSS_TEXT;
        }
    }
	

}
