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
    public Image deleteMenu;
    public Image playMenu;
    public AudioSource myAs;
    public AudioClip minecraft;

    private void Awake()
    {
        MainMenu();
    }

    public void MainMenu()
    {
        playMenu.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);
        tutorialMenu.gameObject.SetActive(false);
        optionsMenu.gameObject.SetActive(false);
        shopMenu.gameObject.SetActive(false);
        deleteMenu.gameObject.SetActive(false);
    }
    public void TutorialMenu()
    {
        playMenu.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(false);
        tutorialMenu.gameObject.SetActive(true);
        optionsMenu.gameObject.SetActive(false);
        shopMenu.gameObject.SetActive(false);
        deleteMenu.gameObject.SetActive(false);
    }
    public void OptionsMenu()
    {
        playMenu.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(false);
        tutorialMenu.gameObject.SetActive(false);
        optionsMenu.gameObject.SetActive(true);
        shopMenu.gameObject.SetActive(false);
        deleteMenu.gameObject.SetActive(false);
    }
    public void ShopMenu()
    {
        playMenu.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(false);
        tutorialMenu.gameObject.SetActive(false);
        optionsMenu.gameObject.SetActive(false);
        shopMenu.gameObject.SetActive(true);
        deleteMenu.gameObject.SetActive(false);
    }
    public void DeleteMenu()
    {
        playMenu.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(false);
        tutorialMenu.gameObject.SetActive(false);
        optionsMenu.gameObject.SetActive(false);
        shopMenu.gameObject.SetActive(false);
        deleteMenu.gameObject.SetActive(true);
    }
    public void PlayMenu()
    {
        playMenu.gameObject.SetActive(true);
        mainMenu.gameObject.SetActive(false);
        tutorialMenu.gameObject.SetActive(false);
        optionsMenu.gameObject.SetActive(false);
        shopMenu.gameObject.SetActive(false);
        deleteMenu.gameObject.SetActive(false);
    }
    public void PlayAudio()
    {
        myAs.PlayOneShot(minecraft);
    }
}
