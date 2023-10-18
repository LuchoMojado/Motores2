using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Image mainMenu;
    public Image tutorialMenu;
    public Image optionsMenu;
    public Image shopMenu;

    private void Awake()
    {
        MainMenu();
    }

    public void MainMenu()
    {
        mainMenu.gameObject.SetActive(true);
        tutorialMenu.gameObject.SetActive(false);
        optionsMenu.gameObject.SetActive(false);
        shopMenu.gameObject.SetActive(false);
    }
    public void TutorialMenu()
    {
        mainMenu.gameObject.SetActive(false);
        tutorialMenu.gameObject.SetActive(true);
        optionsMenu.gameObject.SetActive(false);
        shopMenu.gameObject.SetActive(false);
    }
    public void OptionsMenu()
    {
        mainMenu.gameObject.SetActive(false);
        tutorialMenu.gameObject.SetActive(false);
        optionsMenu.gameObject.SetActive(true);
        shopMenu.gameObject.SetActive(false);
    }
    public void ShopMenu()
    {
        mainMenu.gameObject.SetActive(false);
        tutorialMenu.gameObject.SetActive(false);
        optionsMenu.gameObject.SetActive(false);
        shopMenu.gameObject.SetActive(true);
    }
}
