using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BA_Praxis_Library;

public class MaterialSpawner : Singleton<MaterialSpawner>
{
    [Header("Material Information")]
    public GameObject[] m_AllMaterials;
    private GameObject m_materialToSpawn;

    [Header("Spawnpoints")]
    public Transform[] m_TopSpawnpoint;
    public Transform[] m_BottomSpawnpoint;
    public Transform[] m_LeftSpawnpoint;
    public Transform[] m_RightSpawnpoint;
    private List<Transform[]> m_SpawnpointList = new List<Transform[]>();

    [Header("Velocities")]
    public Vector2 m_TopVelocity;
    public Vector2 m_BottomVelocity;
    public Vector2 m_LeftVelocity;
    public Vector2 m_RightVelocity;
    private List<Vector2> m_VelocityList = new List<Vector2>();

    [Header("Game Variables")]
    public float m_GameTime = 30.0f;
    public float m_SpawnInterval = 1.0f;

    [HideInInspector]
    private bool m_gameOver;
    private float m_currentSpawnInterval = 0.0f;
    private System.Random m_randomIndexGen;

    // Start is called before the first frame update
    void Start()
    {
        // Top references
        m_SpawnpointList.Add(m_TopSpawnpoint);
        m_VelocityList.Add(m_TopVelocity);

        // Bottom references
        m_SpawnpointList.Add(m_BottomSpawnpoint);
        m_VelocityList.Add(m_BottomVelocity);

        // Left references
        m_SpawnpointList.Add(m_LeftSpawnpoint);
        m_VelocityList.Add(m_LeftVelocity);

        // Right references
        m_SpawnpointList.Add(m_RightSpawnpoint);
        m_VelocityList.Add(m_RightVelocity);

        // set material to spawn to correct one
        m_materialToSpawn = m_AllMaterials[PlayerData.currentSelectedCharacter];
    }

    // Update is called once per frame
    void Update()
    {
        // if player started game
        if (StartMiniGame.get.m_StartGame)
        {
            // still has playtime, spawn an obstacle every m_SpawnInterval
            if (m_currentSpawnInterval >= m_SpawnInterval && m_GameTime >= 0)
            {
                HandleMiniGame();
                m_currentSpawnInterval = 0;
            }

            m_GameTime -= Time.deltaTime;
        }

        m_currentSpawnInterval += Time.deltaTime;

        // if game is over
        if (m_GameTime < 0 && !m_gameOver)
        {
            StartMiniGame.get.m_IngameHUD.SetActive(false);
            StartMiniGame.get.m_EndGameScreen.SetActive(true);

            if (MaterialPlayerScript.get.m_MaterialsGathered >= 25)
            {
                StartMiniGame.get.m_EndGameText.text = $"Congrats! You caught {MaterialPlayerScript.get.m_MaterialsGathered} Items!\nYou received 2 Materials, that have been added to your inventory!";

                UpdateMaterial.UpdateResourcesForCurrentSelectedCharacter(2);
            }
            else if (MaterialPlayerScript.get.m_MaterialsGathered >= 15)
            {
                StartMiniGame.get.m_EndGameText.text = $"Congrats! You caught {MaterialPlayerScript.get.m_MaterialsGathered} Items!\nYou received 1 Material, that has been added to your inventory!";

                UpdateMaterial.UpdateResourcesForCurrentSelectedCharacter(1);
            }
            else
            {
                StartMiniGame.get.m_EndGameText.text = $"You didn't catch enough Items!\nTry again next time.";
            }

            m_gameOver = true;

        }
        else
        {
            StartMiniGame.get.m_CountdownText.text = ((int)m_GameTime).ToString();
        }

    }

    private void HandleMiniGame()
    {
        // create new system random for int value
        m_randomIndexGen = new System.Random();

        // index for obstacle array
        int obstacleArrayIndex = m_randomIndexGen.Next(0, 4);

        // get a random spawnpoint in that array
        int obstacleIndex = m_randomIndexGen.Next(0, m_SpawnpointList[obstacleArrayIndex].Length);

        // spawn the gameobject
        GameObject go = Instantiate(m_materialToSpawn, m_SpawnpointList[obstacleArrayIndex][obstacleIndex]);

        // get a random velocity between the right values
        float m_velocityRandom = Random.Range(m_VelocityList[obstacleArrayIndex].x, m_VelocityList[obstacleArrayIndex].y);

        // add the velocity to the object
        go.GetComponent<Rigidbody2D>().AddForce(go.transform.up * m_velocityRandom);
    }
}
