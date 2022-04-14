using UnityEngine.Events;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static event UnityAction UpdateJumpCount;
    public static void JumpPerformed() => UpdateJumpCount?.Invoke();

    public static event UnityAction UpdateDestroyedCount;
    public static void ObjectDestroyed() => UpdateDestroyedCount?.Invoke();

    public static event UnityAction PauseGameTrigger;
    public static void PauseToggle() => PauseGameTrigger?.Invoke();

    public static event UnityAction PlayerDiedTrigger;
    public static void PlayerDied() => PlayerDiedTrigger?.Invoke();

    public static event UnityAction FloorPanelTrigger;
    public static void FloorPanelToggle() => FloorPanelTrigger?.Invoke();
}
