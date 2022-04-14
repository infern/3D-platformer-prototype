using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    #region Variables


    [Header("Settings")]    /********/
    [SerializeField]
    [Range(1f, 7f)]
    float disappearDuration = 7f;
    [SerializeField]
    [Range(1f, 7f)]
    float particleEffectDuration = 7f;

    [Header("Data")]    /********/
    float disappearTimer;
    bool targetHit = false;

    [Header("Components")]    /********/
    [SerializeField]
    GameObject model;
    [SerializeField]
    GameObject projectile;
    [SerializeField]
    AudioSource aS;
    [SerializeField]
    AudioClip spawnSound;
    [SerializeField]
    List<AudioClip> explosionSounds = new List<AudioClip>();
    [SerializeField]
    ParticleSystem ps;
    [SerializeField]
    MeshRenderer mesh;
    [SerializeField]
    Rigidbody rb;


    #endregion


    #region Base Methods

     void OnEnable()
    {
        disappearTimer = disappearDuration;
        aS.clip = spawnSound;
        rb.velocity = Vector3.zero;
        targetHit = false;
        aS.Play();
        model.SetActive(true);
        projectile.SetActive(false);

    }

    void Update()
    {
        DisappearTimer();
    }



    #endregion

    #region Unique Methods

    void DisappearTimer()
    {
        if (disappearTimer > 0) disappearTimer -= Time.deltaTime;
        else
        {
            gameObject.SetActive(false);
        }
    }

     void OnCollisionEnter(Collision collision)
    {
        if (!targetHit)
        {
            rb.velocity = Vector3.zero;
            targetHit = true;
            ParticleSystem.MainModule gm = ps.main;
            gm.startColor = mesh.material.color;
            model.SetActive(false);
            projectile.SetActive(true);
            disappearTimer = particleEffectDuration;
            int randomSound = Random.Range(0, explosionSounds.Capacity);
            aS.clip = explosionSounds[randomSound];
            aS.Play();
            ShootTarget target = collision.gameObject.GetComponent<ShootTarget>();
            if(target!=null) target.Destroyed();


        }



    }

    #endregion
}
