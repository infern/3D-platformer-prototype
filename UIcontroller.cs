using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
public class UIcontroller : MonoBehaviour
{
    #region Variables


    [Header("Components")]    /********/
    [SerializeField]
    TextMeshProUGUI jumpTMP;
    [SerializeField]
    TextMeshProUGUI destroyedTMP;
    [SerializeField]
    TextMeshProUGUI timeTMP;
    [SerializeField]
    Animator jumpAnim;
    [SerializeField]
    Animator destroyedAnim;


    #endregion

    void Start()
    {
        
        jumpTMP.text = PlayerPrefs.GetInt("jump", 0).ToString();
        destroyedTMP.text = PlayerPrefs.GetInt("destroyed", 0).ToString();
    }
    void OnEnable()
    {
        EventManager.UpdateJumpCount += UpdateJumpCount;
        EventManager.UpdateDestroyedCount += UpdateDestroyedCount;

    }


    void OnDisable()
    {
        EventManager.UpdateJumpCount -= UpdateJumpCount;
        EventManager.UpdateDestroyedCount -= UpdateDestroyedCount;
    }

     void Update()
    {
        var ts = TimeSpan.FromSeconds(Time.realtimeSinceStartup);
        timeTMP.text = string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);

    }

    void UpdateJumpCount()
    {
        int value = PlayerPrefs.GetInt("jump", 0);
        PlayerPrefs.SetInt("jump", value + 1);
        jumpTMP.text = PlayerPrefs.GetInt("jump", 0).ToString();
        jumpAnim.Play("bounce", -1, -1);
    }

    void UpdateDestroyedCount()
    {
        int value = PlayerPrefs.GetInt("destroyed", 0);
        PlayerPrefs.SetInt("destroyed", value + 1);
        destroyedTMP.text = PlayerPrefs.GetInt("destroyed", 0).ToString();
        destroyedAnim.Play("bounce", -1, -1);

    }

}
