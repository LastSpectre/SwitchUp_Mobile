using System.Collections;
using UnityEngine;
using System.Device.Location;
using TMPro;
using BA_Praxis_Library;

public class GPSTracking : Singleton<GPSTracking>
{
    // Latitude (North / South) Breitengrad
    [HideInInspector]
    public float m_Latitude_Last = 0.0f;
    [HideInInspector]
    public float m_Latitude_Latest = 0.0f;

    // Longitude (West / East) Längengrad
    [HideInInspector]
    public float m_Longitude_Last = 0.0f;
    [HideInInspector]
    public float m_Longitude_Latest = 0.0f;

    private float m_updateLocationTimer = 0.0f;
    private float m_saveLocationTimer = 0.0f;
    private bool m_locationTrackable = false;

    // current steps
    public double m_currentStepTracker;

    // Debug
    [HideInInspector]
    public bool m_EnableDatabaseSaving = true;
    public TMP_Text m_IsGPSEnabled;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        GetSavedSteps();

        StartCoroutine(StartGPSLocationService());
    }

    // Update is called once per frame
    void Update()
    {
        if (m_updateLocationTimer > 1.0f && m_locationTrackable)
        {
            UpdateGPSPos();
        }

        if(m_saveLocationTimer > 10.0f && m_locationTrackable && m_EnableDatabaseSaving)
        {
            UpdateStepsInDatabase();
            m_saveLocationTimer = 0;
        }

        m_updateLocationTimer += Time.deltaTime;
        m_saveLocationTimer += Time.deltaTime;
    }

    // tries to start the gps location system
    private IEnumerator StartGPSLocationService()
    {
        if (!Input.location.isEnabledByUser)
        {
            m_IsGPSEnabled.text = "User has not enabled GPS";
            yield break;
        }

        Input.location.Start();

        int maxWait = 20;

        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait <= 0)
        {
            m_IsGPSEnabled.text =  "Timed out";
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            m_IsGPSEnabled.text = "Unable to get device location";
            yield break;
        }

        m_Latitude_Latest = Input.location.lastData.latitude;
        m_Longitude_Latest = Input.location.lastData.longitude;

        m_locationTrackable = true;

        ServerResponse sr = UserRequestLibrary.GetResourcesRequest();

        m_currentStepTracker = sr.Resources.StepCounter;

        m_IsGPSEnabled.text = "GPS active";
    }

    // renews the gps position and stepcounter data
    private void UpdateGPSPos()
    {
        // save the last spot
        m_Latitude_Last = m_Latitude_Latest;
        m_Longitude_Last = m_Longitude_Latest;

        m_Latitude_Latest = Input.location.lastData.latitude;
        m_Longitude_Latest = Input.location.lastData.longitude;

        // calculate the distance walked since last second
        double distance = GetDistanceBetweenTwoPoints(m_Latitude_Last, m_Longitude_Last,
                                                      m_Latitude_Latest, m_Longitude_Latest);

        // add the distance onto the step counter
        m_currentStepTracker += distance;
        
        // reset the update timer
        m_updateLocationTimer = 0;
    }

    // returns the distance between two points (gps coordinates)
    public double GetDistanceBetweenTwoPoints(double latitude1, double longitude1, double latitude2, double longitude2)
    {
        GeoCoordinate _pointOne = new GeoCoordinate(latitude1, longitude1);
        GeoCoordinate _pointTwo = new GeoCoordinate(latitude2, longitude2);

        return _pointOne.GetDistanceTo(_pointTwo);
    }

    // gets the saved stepdata from database
    public void GetSavedSteps()
    {
        ServerResponse sr = UserRequestLibrary.GetResourcesRequest();

        m_currentStepTracker = sr.Resources.StepCounter;

        ResourcesWindow.get.m_StepCounterText.text = ((int)m_currentStepTracker).ToString();
    }

    // saves the accumulated steps from player in the database
    public void UpdateStepsInDatabase()
    {
        ServerResponse sr = UserRequestLibrary.GetResourcesRequest();

        if (m_locationTrackable)
        {
            BA_Praxis_Library.Resources res = new BA_Praxis_Library.Resources()
            {
                StepCounter = (int)m_currentStepTracker,
                AncientMaterial = sr.Resources.AncientMaterial,
                ChaosMaterial = sr.Resources.ChaosMaterial,
                DarkMaterial = sr.Resources.DarkMaterial,
                NatureMaterial = sr.Resources.NatureMaterial,
                NeutralMaterial = sr.Resources.NeutralMaterial                
                };

            sr = UserRequestLibrary.UpdateResourcesRequest(res);
        }
    }
}