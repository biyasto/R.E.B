using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChooseLevel : MonoBehaviour
{
    public void Lv0()
    {
        SceneManager.LoadScene("LevelTutorial");
    }
    public void Lv1()
    {
        SceneManager.LoadScene("Level1");
    }
    public void Lv2()
    {
        SceneManager.LoadScene("Level2");
    }
    public void Lv3()
    {
        SceneManager.LoadScene("Level3");
    }
    public void Lv5()
    {
        SceneManager.LoadScene("Level5");
    }
    public void Lv4()
    {
        SceneManager.LoadScene("Level4");
    }
    public void Lv6()
    {
        SceneManager.LoadScene("Level6");
    }
    public void Lv7()
    {
        SceneManager.LoadScene("Level7");
    }
    public void Lv8()
    {
        SceneManager.LoadScene("Level8");
    }
    public void Back()
    {
        SceneManager.LoadScene("Menu");
    }
}
