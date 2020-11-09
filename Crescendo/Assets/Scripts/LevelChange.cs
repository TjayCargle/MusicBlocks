using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelChange : MonoBehaviour
{
    public string sceneName = "";

    public void LoadScene()
    {
        PlayerPrefs.SetInt("mode", 0);
        SceneManager.LoadScene(sceneName);
    }

    public void LoadScene(string scene)
    {
        PlayerPrefs.SetInt("mode", 0);
        SceneManager.LoadScene(scene);
    }

    public void SetupData(ValueHolder holder)
    {

        PlayerPrefs.SetInt("gridWidth", holder.wid);
        PlayerPrefs.SetInt("gridHeight", holder.hei);
        PlayerPrefs.SetInt("song", holder.song);
        PlayerPrefs.SetInt("mode", 1);
        SceneManager.LoadScene("New Scene");
    }

    public void SetupDataFromLevel(ValueHolder holder)
    {

        PlayerPrefs.SetInt("gridWidth", holder.wid);
        PlayerPrefs.SetInt("gridHeight", holder.hei);
        PlayerPrefs.SetInt("song", holder.song);

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
    }
}
