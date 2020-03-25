using BA_Praxis_Library;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    // UI
    public TMP_Text m_PasswordText;
    public TMP_Text m_NameText;

    public Image LoginBanner;
    public TMP_Text LoginBannerText;

    public TMP_Text m_NewIPText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Login()
    {
        // Set IP to the current ip text if not empty
        if(m_NewIPText.text == "")
        {
            return;
        }

        PlayerData.IP = m_NewIPText.text.Remove(m_NewIPText.text.Length - 1);

        ServerResponse sr = UserRequestLibrary.LoginRequest(m_NameText, m_PasswordText);

        // if response is positive, save name and password, and load next scene
        if (sr.ResponseType == EServerResponseType.OK)
        {
            // save name and password
            PlayerData.playerName = m_NameText.text.Remove(m_NameText.text.Length - 1);
            PlayerData.playerPassword = m_PasswordText.text.Remove(m_PasswordText.text.Length - 1);

            LoginBanner.gameObject.SetActive(true);
            LoginBannerText.text = sr.Text;
            StartCoroutine(LoadMainScene());
            return;
        }

        // if response is error, print out error message
        if (sr.ResponseType == EServerResponseType.ERROR)
        {
            LoginBanner.gameObject.SetActive(true);
            LoginBannerText.text = sr.Text;
            Invoke("DisableLoginBanner", 2.0f);
            return;
        }
    }

    void DisableLoginBanner()
    {
        LoginBanner.gameObject.SetActive(false);
        LoginBannerText.text = "";
    }

    IEnumerator LoadMainScene()
    {
        AsyncOperation asyncSceneLoad = SceneManager.LoadSceneAsync(1);

        while (!asyncSceneLoad.isDone)
        {
            yield return null;
        }
    }
}
