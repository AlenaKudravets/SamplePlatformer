using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlatformerGameManager : MonoBehaviour
{
    private PlatformerCharacterController m_character;
    private Vector3 m_charSpawnPoint;
    public Vector3 CharacterSpawnPoint
    {
        get { return m_charSpawnPoint; }
        set { m_charSpawnPoint = value; }
    }
    private static PlatformerGameManager m_instance;
    public static PlatformerGameManager Instance
    {
        get
        {
            if(m_instance == null)
            {
                var instance = Instantiate(Resources.Load("Prefabs/PlatformerGameManager")) as GameObject;
                m_instance = instance.GetComponent<PlatformerGameManager>();
            }
            return m_instance;
        }
        //get { return m_instance ?? (m_instance = CreateInstanceOnScene()); }//EQUALS
        //get { return _instance != null ? _instance : (_instance = CreateInstanceOnScene()); }
    }

    void Awake ()
    {
        if (m_instance == null)
        {
            m_instance = this; 
        }
        else
        {
            if (m_instance != this)
            {
                Destroy(gameObject);
            }
        }

        SpawnCharacter();
	}

    void OnDestroy()
    {
        m_instance = null;
    }

    private void SpawnCharacter()
    {
        m_charSpawnPoint = new Vector3(-3, 0, 0);
        Instantiate(Resources.Load("Prefabs/Character"), m_charSpawnPoint, transform.rotation);
    }

    public void RespawnCharacter()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);//TODO: Show loose UI
        PlatformerStats.CurrentScore = 0;
    }
}
