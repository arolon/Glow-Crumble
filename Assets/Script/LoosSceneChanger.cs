using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoosSceneChanger : MonoBehaviour
{
    public void ChangeSceneToStart()
    {

        SceneManager.LoadScene("StartScene");
    }
}
