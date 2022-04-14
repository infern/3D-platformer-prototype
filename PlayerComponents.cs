using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComponents : MonoBehaviour
{
    public PlayerStatus status;
    public PlayerRun run;
    public PlayerJump jump;
    public PlayerShoot shoot;
    public ReadPlayerInput input;

    public Rigidbody rb;
    public AudioSource aS1;
    public AudioSource aS2;

    public BoxCollider boxCollider;


    public void PlaySound(int source, AudioClip clip)
    {
        if (source == 0)
        {
            aS1.clip = clip;
            aS1.Play();
        }
        else
        {
            aS2.clip = clip;
            aS2.Play();
        }

    }


}
