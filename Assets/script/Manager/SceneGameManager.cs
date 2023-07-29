using Assets.script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneGameManager : MonoBehaviour
{

    public static SceneGameManager instance { get; private set;}
    public static int score = 0;
    [SerializeField] GameObject SettingPanel;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }


    public void StartNewGame()
    {
        int currScene = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(currScene + 1);
        string nameNextScene = SceneManager.GetSceneByBuildIndex(currScene + 1).name;
        SceneManager.LoadScene(currScene + 1);

        if(SceneManager.GetSceneByBuildIndex(currScene).name != "StartGame")
        {
            Sound_Manager.Instance.PlayMusic(Sound_Manager.Instance.background);
        }

        if (nameNextScene != "Credit")
        {
            score = Car.carObj.score;
        }

        if(SceneManager.GetSceneByBuildIndex(currScene).name == "Credit")
        {
            Destroy(GameObject.FindGameObjectWithTag("Obstacles"));
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartScene()
    {
        int currScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currScene);

    }

    public void ShowSettingPanel()
    {
        SettingPanel.SetActive(!SettingPanel.activeInHierarchy);
    }
}
