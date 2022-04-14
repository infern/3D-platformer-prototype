using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    [Header("Settings")]    /********/
    [SerializeField]
    [Range(1f, 50f)]
    float speed = 0.2f;
    [SerializeField]
    [Range(0f, 3f)]
    float waitDuration = 0.2f;

    [Header("Data")]    /********/
    Vector3 startPosition;
    Vector3 endPosition;
    float waitTimer;





    [Header("Components")]    /********/
    [SerializeField]
     Transform target;

     void Start()
    {
        startPosition = this.transform.position;
        endPosition = target.position;
    }
    void Update()
    {
        TravelTowardsTarget();
        WaitTimer();
    }

    void TravelTowardsTarget()
    {
        if (waitTimer <=0)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, endPosition, step);
            bool platformReachedTarget = (Vector3.Distance(transform.position, endPosition) <= 0);
            if (platformReachedTarget)
            {
                Vector3 temp = startPosition;
                startPosition = endPosition;
                endPosition = temp;
                waitTimer = waitDuration;

            }
        }
    }

      void OnCollisionEnter(Collision collision)
    {
        PlayerComponents player = collision.gameObject.GetComponent<PlayerComponents>();
        if (player != null)
        {
            player.transform.SetParent(this.transform);
        }
    }

     void OnCollisionExit(Collision collision)
    {
        PlayerComponents player = collision.gameObject.GetComponent<PlayerComponents>();
        if (player != null)
        {
            player.transform.SetParent(null);
        }
    }

    void WaitTimer()
    {
        if (waitTimer > 0) waitTimer -= Time.deltaTime;
    }


}
