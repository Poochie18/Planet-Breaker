using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour
{
    //Loading the main scene
    public void Restart(){ SceneManager.LoadScene("MainScene"); }
}
