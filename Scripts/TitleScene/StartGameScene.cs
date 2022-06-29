using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameScene : MonoBehaviour {

    public void GoGameSceneButton()
    {
        Debug.Log("시작 버튼 클릭됨");
        SceneManager.LoadScene("GameScene");        
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GoGameSceneButton();
        }
    }
}
