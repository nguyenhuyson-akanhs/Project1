using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum GameStatus
{
    Continues,
    Reseted,
    Stopped
}
public class GameControllerScript : MonoBehaviour
{
    [Header("GameStatus")]
    public static GameStatus gameStatus { get; private set; } = GameStatus.Continues;

    [Header("ProcessBar")]
    public int ProcessAmount;
    public Slider ProcessSlider;
    public int MaxProcessAmount = 100;
    public static bool FullProcess = false;

    [Header("LoadLevel")]
    public Transform PlayerTransform;
    public List<GameObject> Levels;
    public int CurrentLevel;

    [Header("GameOver")]
    public GameObject GameOverScreen;
    public GameObject GameResetButton;
    public static event Action GameHasReset;
    private void Start()
    {
        ProcessAmount = 0;
        ProcessSlider.value = 0;
        ProcessSlider.maxValue = MaxProcessAmount;
    }
    private void ProcessBar(int amount)
    {
        ProcessAmount += amount;
        ProcessSlider.value = ProcessAmount;
        if (ProcessAmount >= MaxProcessAmount)
        {
            //CameraShaking.CallShakeCamera();
            ObjectShaker.CallObjectShake("Slider", 10f, 0.1f);
            FullProcess = true;
        }
    }
    private void LoadNextLevel()
    {
        int NextLevel = (CurrentLevel == Levels.Count - 1) ? 0 : CurrentLevel + 1;
        if (gameStatus == GameStatus.Reseted)
        {
            NextLevel = 0;
            gameStatus = GameStatus.Continues;
        }

        Levels[CurrentLevel].SetActive(false);
        CurrentLevel = NextLevel;
        GameProcessReset();

        Levels[NextLevel].SetActive(true);


        PlayerTransform.position = Vector3.zero;
    }

    private void EnableGameOverScreen()
    {
        GameOverScreen.SetActive(true);
        GameResetButton.SetActive(true);

        Time.timeScale = 0;
    }

    public void GameReset()
    {
        GameHasReset.Invoke();

        gameStatus = GameStatus.Reseted;
        LoadNextLevel();

        GameOverScreen.SetActive(false);
        GameResetButton.SetActive(false);

        Time.timeScale = 1;
    }
    private void GameProcessReset()
    {
        ProcessAmount = 0;
        ProcessSlider.value = 0;
        FullProcess = false;
    }
    private void OnEnable()
    {
        Gems.OnGemsCollect += ProcessBar;
        HoldToLoadLevel.OnHold += LoadNextLevel;
        PlayerHealth.OnDeath += EnableGameOverScreen;
    }
    private void OnDisable()
    {
        Gems.OnGemsCollect -= ProcessBar;
        HoldToLoadLevel.OnHold -= LoadNextLevel;
        PlayerHealth.OnDeath -= EnableGameOverScreen;
    }
}
