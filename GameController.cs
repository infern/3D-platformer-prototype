using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{





    [Header("Settings")]    /********/
    [SerializeField]
    [Range(0f, 2f)]
    float respawnDuration = 0.5f;
    [SerializeField] AudioClip pauseOnSound;
    [SerializeField] AudioClip pauseOffSound;
    [SerializeField] AudioClip deathSound;



    [Header("Data")]    /********/
    bool respawn = false;
    float respawnTimer;
    bool paused = false;
    bool floorPanelActive = false;


    [Header("Components")]    /********/
    [SerializeField] PlayerComponents player;
    [SerializeField] Transform checkpointPosition;
    [SerializeField] GameObject pausePanel;
    [SerializeField] AudioSource aS;
    [SerializeField] Animator anim;

    #region Base Methods

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void OnAfterSceneLoadRuntimeMethod()
    {
        PlayerPrefs.SetInt("destroyed", 0);
        PlayerPrefs.SetInt("jump", 0);
    }

    void OnEnable()
    {
        EventManager.PauseGameTrigger += PauseToggle;
        EventManager.PlayerDiedTrigger += StartRespawnTimer;
        EventManager.FloorPanelTrigger += ToggleLiftBool;


    }

    void OnDisable()
    {
        EventManager.PauseGameTrigger -= PauseToggle;
        EventManager.PlayerDiedTrigger -= StartRespawnTimer;
        EventManager.FloorPanelTrigger -= ToggleLiftBool;



    }

    void Start()
    {
        Cursor.visible = false;
#if UNITY_EDITOR_64
        Cursor.visible = true;
#endif
        Time.timeScale = 1;

    }

    void Update()
    {
        RespawnTimer();
    }

    #endregion


    #region Unique Methods

    void StartRespawnTimer()
    {
        respawnTimer = respawnDuration;
        respawn = true;
        anim.Play("closeOpen");
        aS.clip = deathSound;
        aS.Play();
    }
    void RespawnTimer()
    {
        if (respawn)
        {
            if (respawnTimer > 0) respawnTimer -= Time.deltaTime;
            else
            {
                respawn = false;
                respawnTimer = 0f;
                player.gameObject.transform.position = checkpointPosition.position;
            }
        }
    }


    void PauseToggle()
    {
        if (!floorPanelActive)
        {
            if (paused) PauseOff();
            else PauseOn();
            paused = !paused;
        }
    }

    void PauseOn()
    {
        aS.clip = pauseOnSound;
        aS.Play();
        pausePanel.SetActive(true);
        Cursor.visible = true;
        Time.timeScale = 0f;
    }

    void PauseOff()
    {
        aS.clip = pauseOffSound;
        aS.Play();
        Cursor.visible = false;
#if UNITY_EDITOR_64
        Cursor.visible = true;
#endif
        pausePanel.SetActive(false);
        Time.timeScale = 1;

    }

    void ToggleLiftBool()
    {
        floorPanelActive = !floorPanelActive;
        if (floorPanelActive) Time.timeScale = 0f;
        else Time.timeScale = 1f;
    }

    #endregion
}
