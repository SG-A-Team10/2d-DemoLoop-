using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void OnClickNewGame()
    {
        Debug.Log("게임 시작.");
        // 메인 씬을 로드
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
        // 추가 씬을 로드
        SceneManager.LoadScene("Essential", LoadSceneMode.Additive);
        //SceneManager.LoadScene("Start");
    }

    public void OnClickLoad()
    {
        Debug.Log("불러오기 클릭됨");
        SceneManager.LoadScene("Load Screen");
    }

    public void OnClickOptions()
    {
        Debug.Log("옵션 클릭됨");
        SceneManager.LoadScene("Options Screen");
    }

    public void OnClickQuit()
    {
        // if UNITY_EDITOR
        // UnityEditor.EditorApplication.isPlaying = false; // 에디터 에서 실행해줬을때
        Application.Quit(); //빌드한 앱에서 실행 했을때
    }
}
