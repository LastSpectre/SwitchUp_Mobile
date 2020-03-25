using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResourceMissionWindow : MonoBehaviour
{
    public TMP_Text m_MissingStepsText;

    public Button m_GatherMaterialButton;
    private ButtonAppearance m_gatherMaterialButtonAppearance;

    public Button m_GainExperienceButton;
    private ButtonAppearance m_gainExperienceButtonAppearance;

    [SerializeField]
    private int m_stepRangeUntilNewMission = 150;

    // Start is called before the first frame update
    void Start()
    {
        m_gatherMaterialButtonAppearance = m_GatherMaterialButton.GetComponent<ButtonAppearance>();
        m_gainExperienceButtonAppearance = m_GainExperienceButton.GetComponent<ButtonAppearance>();
    }

    // Update is called once per frame
    void Update()
    {
        // if player walked enough steps
        if (GPSTracking.get.m_currentStepTracker >= m_stepRangeUntilNewMission)
        {
            m_MissingStepsText.text = "Mission ready! Select a game!";

            // enable buttons
            m_gatherMaterialButtonAppearance.ChangeAppearance(false);
            m_gainExperienceButtonAppearance.ChangeAppearance(false);
        }
        // if he didnt
        else
        {
            // update the ui
            m_MissingStepsText.text = "Missing for resource mission: " + ((int)((m_stepRangeUntilNewMission + 1) - GPSTracking.get.m_currentStepTracker)).ToString();

            // disable buttons
            m_gatherMaterialButtonAppearance.ChangeAppearance(true);
            m_gainExperienceButtonAppearance.ChangeAppearance(true);
        }
    }

    // invokes the scene change to experience gain level
    public void LoadExperienceLevel()
    {
        StartCoroutine(LoadSceneAsync(2));
    }

    // invokes the scene change to material gather level
    public void LoadMaterialLevel()
    {
        StartCoroutine(LoadSceneAsync(3));
    }

    // loads the given scene async
    IEnumerator LoadSceneAsync(int _sceneNumber)
    {
        GPSTracking.get.m_currentStepTracker -= m_stepRangeUntilNewMission;
        GPSTracking.get.UpdateStepsInDatabase();

        SingletonReset.Reset();
        AsyncOperation asyncSceneLoad = SceneManager.LoadSceneAsync(_sceneNumber);

        while (!asyncSceneLoad.isDone)
        {
            yield return null;
        }
    }

}
