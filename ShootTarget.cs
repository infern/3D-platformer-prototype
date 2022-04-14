using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTarget : MonoBehaviour
{
    #region Variables


    [Header("Settings")]    /********/
    [SerializeField]
    [Range(0.1f, 3f)]
    float disappearDuration = 1f;


    [Header("Data")]    /********/
    bool targetHit = false;


    [Header("Components")]    /********/
    [SerializeField]
    MeshCollider meshCollider;


    #endregion

    #region Base Methods

    #endregion

    #region Unique Methods

    public void Destroyed()
    {
        if (!targetHit)
        {
            targetHit = true;
            StartCoroutine(ScaleOverTime(disappearDuration));
            EventManager.ObjectDestroyed();
            meshCollider.enabled = false;
        }
    }


    IEnumerator ScaleOverTime(float time)
    {
        Vector3 originalScale = transform.localScale;
        float currentTime = 0.0f;
        do
        {
            transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, currentTime / time);
            currentTime += Time.deltaTime;
            yield return null;
        } while (currentTime <= time);

        Destroy(gameObject);
    }



    #endregion

}
