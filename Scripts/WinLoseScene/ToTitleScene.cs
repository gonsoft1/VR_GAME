using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToTitleScene : MonoBehaviour {

    public void ToTitleSceneButton()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void Start()
    {
        Invoke("ToTitleSceneButton", 9f);
    }
}
