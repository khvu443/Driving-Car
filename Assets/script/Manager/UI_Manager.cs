using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager Instance { get; private set; }
    [SerializeField] GameObject UIpanelStart;
    [SerializeField] GameObject UIpanelOver;
    [SerializeField] GameObject UIpanelGameOver;
    [SerializeField] GameObject UIpanelPause;
    [SerializeField] GameObject UIpanelSetting;
    [SerializeField] TMP_Text textLevel;
    public int isFinish = 0;




    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        else
        {
            Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
        textLevel.text = "LEVEL " + SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        if (UIpanelStart.activeInHierarchy)
        {
            StartCurrLevel();
        }

        switch (isFinish)
        {
            case 1: UIpanelOver.SetActive(true); break;
            case 2: UIpanelGameOver.SetActive(true); break;
            case 3: UIpanelPause.SetActive(true); break;
        }
    }



    public void StartCurrLevel()
    {
        if (Input.anyKey)
        {
            GameManager.isStartGame = true;
            Time.timeScale = 1f;
            UIpanelStart.SetActive(false);
        }
    }

    public void StartNextLevel()
    {
        SceneGameManager.instance.StartNewGame();
        SpawnObstacle.timeSpawn--;

    }

    public void RestartGame()
    {
        SceneGameManager.instance.RestartScene();
    }

    public void QuitGame()
    {
        SceneGameManager.instance.QuitGame();
    }

    public void ResumeGame()
    {
        GameManager.isStartGame = true;
        Time.timeScale = 1f;
        UIpanelPause.SetActive(false);
        isFinish = 0;
    }

    public void DisplayeSetting()
    {
        UIpanelSetting.SetActive(!UIpanelSetting.activeInHierarchy);
    }

}
