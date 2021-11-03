using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    [SerializeField] Button startGame;
    [SerializeField] Button continueGame;
    [SerializeField] Button exitGame;
    [SerializeField] Button pauseGame;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] Canvas mainCanvas;
    [SerializeField] Canvas menuCanvas;

    private void Awake()
    {
        menuCanvas.gameObject.SetActive(true);
        mainCanvas.gameObject.SetActive(false);
        Time.timeScale = 0;
    }

    private void Update()
    {
        SetLevelCount();
    }

    public void StartGame()
    {
        menuCanvas.gameObject.SetActive(false);
        mainCanvas.gameObject.SetActive(true);
        Time.timeScale = 1;
    }

    public void PauseGame()
    {
        menuCanvas.gameObject.SetActive(true);
        mainCanvas.gameObject.SetActive(false);
        startGame.gameObject.SetActive(false);
        continueGame.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
    
    public void ContinueGame()
    {
        menuCanvas.gameObject.SetActive(false);
        mainCanvas.gameObject.SetActive(true);
        Time.timeScale = 1;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void SetLevelCount()
    {
        levelText.text = "Level " + gameManager.GetCurrentLevel();
    }
}
