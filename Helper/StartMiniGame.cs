using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StartMiniGame : Singleton<StartMiniGame>
{
    [Header("UI Variables")]
    public Image m_PreGameScreen;
    public TMP_Text m_PreGameText;

    public Button m_StartGameButton;

    public GameObject m_IngameHUD;
    public TMP_Text m_CountdownText;

    public GameObject m_EndGameScreen;
    public TMP_Text m_EndGameText;

    [HideInInspector]
    public bool m_StartGame;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartMiniGame_Button()
    {
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        // Deactivate Start Game Button
        m_StartGameButton.gameObject.SetActive(false);

        // Set text in middle of screen
        m_PreGameText.rectTransform.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);

        // countdown
        m_PreGameText.text = "3";
        yield return new WaitForSeconds(1.0f);

        m_PreGameText.text = "2";
        yield return new WaitForSeconds(1.0f);

        m_PreGameText.text = "1";
        yield return new WaitForSeconds(1.0f);

        m_PreGameScreen.gameObject.SetActive(false);
        m_StartGame = true;
        m_IngameHUD.SetActive(true);

        yield break;
    }

    public void LoadMainScene()
    {
        StartCoroutine(MainScene());
    }

    IEnumerator MainScene()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(1);

        while(!asyncOperation.isDone)
        {
            yield return null;
        }
    }
}
