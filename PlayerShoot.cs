using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    #region Variables


    [Header("Settings")]    /********/
    [SerializeField]
    [Range(1f, 600f)]
    float force = 300f;
    [SerializeField]
    [Range(0f, 1f)]
    float cooldownDuration = 0.5f;
    [SerializeField]
    Vector3 spawnOffset = Vector3.zero;
    [SerializeField] List<Material> materials = new List<Material>();

    [Header("Data")]    /********/
    float cooldownTimer;
    bool buttonDown = false;
    bool shootingEnabled = true;

    [Header("Components")]    /********/
    [SerializeField]
    PlayerComponents player;
    [SerializeField]
    GameObject projectile;


    #endregion


    #region Base Methods

    void OnEnable()
    {
        EventManager.PauseGameTrigger += DisableShoot;
        EventManager.FloorPanelTrigger += DisableShoot;


    }

    void OnDisable()
    {
        EventManager.PauseGameTrigger -= DisableShoot;
        EventManager.FloorPanelTrigger -= DisableShoot;

    }

    void Update()
    {
        CooldownTimer();
        CreateProjectile();
    }



    #endregion

    #region Unique Methods

    public void ButtonDown()
    {
        buttonDown = true;
    }

    public void ButtonUp()
    {
        buttonDown = false;
    }

    void CreateProjectile()
    {
        if (buttonDown && cooldownTimer <= 0 && shootingEnabled)
        {
            cooldownTimer = cooldownDuration;
            GameObject temp = ObjectPool.SharedInstance.GetProjectileFromPool();
            temp.SetActive(true);
            Vector3 spawnPosition = new Vector3(player.transform.position.x + spawnOffset.x, player.transform.position.y + spawnOffset.y, player.transform.position.z + spawnOffset.z);
            temp.transform.position = spawnPosition;
            Material randomMaterial = materials[Random.Range(0, materials.Capacity)];
            temp.transform.GetChild(0).GetComponent<MeshRenderer>().material = randomMaterial;
            temp.transform.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * force, ForceMode.Impulse);
        }
    }

    void CooldownTimer()
    {
        if (cooldownTimer > 0) cooldownTimer -= Time.deltaTime;
    }

    void DisableShoot()
    {
        shootingEnabled = !shootingEnabled;
    }

    #endregion
}
