using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseButtons : MonoBehaviour
{
    [SerializeField]
    float transitionDuration = 0.5f;
    float transitionTimer;
    bool transitionActive = false;
    int scene = 0;
    [SerializeField]
    Animator transitionAnim;
    [SerializeField] AudioSource aS;
    [SerializeField] AudioClip closeSound;

     void Update()
    {
        TransitionTimer();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Task1()
    {
        scene = 0;
        TriggerTransition();
    }

    public void Task2()
    {
        scene = 1;
        TriggerTransition();
    }

    void TriggerTransition()
    {
        transitionTimer = transitionDuration;
        transitionActive = true;
        transitionAnim.Play("close");
        aS.clip = closeSound;
        aS.Play();
    }

    void TransitionTimer()
    {
        if (transitionActive)
        {
            if (transitionTimer > 0) transitionTimer -= Time.unscaledDeltaTime;
            else
            {
                transitionActive = false;
                SceneManager.LoadScene(scene);

            }
        }
    }

}
