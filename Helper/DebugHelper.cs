using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DebugHelper : MonoBehaviour
{
    public Image m_Checkmark;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoBackToMainScene()
    {
        SceneManager.LoadScene(1);
    }

    public void DisableDatabaseSaving()
    {
        GPSTracking.get.m_EnableDatabaseSaving = !(GPSTracking.get.m_EnableDatabaseSaving);

        m_Checkmark.gameObject.SetActive(GPSTracking.get.m_EnableDatabaseSaving);
    }
}
