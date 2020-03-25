using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using BA_Praxis_Library;

public class ResourcesWindow : Singleton<ResourcesWindow>
{
    [Header("UI Elements")]
    public TMP_Text m_AncientMaterialText;
    public TMP_Text m_ChaosMaterialText;
    public TMP_Text m_DarkMaterialText;
    public TMP_Text m_NatureMaterialText;
    public TMP_Text m_NeutralMaterialText;
    public TMP_Text m_StepCounterText;    

    // Start is called before the first frame update
    void Start()
    {
        UpdateResources();
    }

    // Update is called once per frame
    void Update()
    {
        // update the ui
        m_StepCounterText.text = ((int)GPSTracking.get.m_currentStepTracker).ToString();
    }

    // get resources from database
    public void UpdateResources()
    {
        ServerResponse sr = UserRequestLibrary.GetResourcesRequest();

        m_AncientMaterialText.text = sr.Resources.AncientMaterial.ToString();
        m_ChaosMaterialText.text = sr.Resources.ChaosMaterial.ToString();
        m_DarkMaterialText.text = sr.Resources.DarkMaterial.ToString();
        m_NatureMaterialText.text = sr.Resources.NatureMaterial.ToString();
        m_NeutralMaterialText.text = sr.Resources.NeutralMaterial.ToString();
    }
}
