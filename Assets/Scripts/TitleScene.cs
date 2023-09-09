using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScene : MonoBehaviour
{
    public Button startbutton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartButton()
    {
       // startbutton.onClick.AddListener(() => { UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene"); });
       UnityEngine.SceneManagement.SceneManager.LoadScene("Lobby");
    }

}
