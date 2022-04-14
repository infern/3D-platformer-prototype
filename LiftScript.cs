using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftScript : MonoBehaviour
{
    [Header("Settings")]    /********/
    [SerializeField]
    bool active = true;
    [SerializeField]
    [Range(1f, 50f)]
    float speed = 0.2f;
    [SerializeField]
    bool reached = false;
    [Range(0f, 2f)]
    float liftDelayDuration = 0.5f;
    [SerializeField]
    bool multiLevel = false;

    [Header("Data")]    /********/
    Vector3 startPosition;
    Vector3 endPosition;
    Vector3 topOfLift;
    float liftDelayTimer;
    bool liftDelayActive = false;
    bool selectingFloor = false;
    int selectedFloor=-1;



    [Header("Components")]    /********/
    [SerializeField] GameObject singleFloorTower;
    [SerializeField] GameObject multipleFloorTower;
    [SerializeField]
    Transform target;
    [SerializeField]
    AudioSource aS;
    [SerializeField]
    List<Transform> floors = new List<Transform>();
    [SerializeField] GameObject  floorSelectionPanel;

    void Start()
    {

        endPosition = target.position;
        topOfLift = target.position;
        active = false;
        if (multiLevel)
        {
            singleFloorTower.SetActive(false);
            multipleFloorTower.SetActive(true);
            startPosition = floors[0].position;
        }
        else
        {
            singleFloorTower.SetActive(true);
            multipleFloorTower.SetActive(false);
            startPosition = this.transform.position;
        }
    }
    void Update()
    {
        TravelTowardsTarget();
        LiftDelayTimer();
    }

    void TravelTowardsTarget()
    {
        if (active == true)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, endPosition, step);
            bool platformReachedTarget = (Vector3.Distance(transform.position, endPosition) <= 0);
            if (platformReachedTarget)
            {
                Vector3 temp = startPosition;
                startPosition = endPosition;
                endPosition = temp;
                active = false;
                bool liftIsAtTheTop = (Vector3.Distance(transform.position, topOfLift) <= 0);
             if (liftIsAtTheTop && selectedFloor!=0) reached = true;


            }
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        PlayerComponents player = collision.gameObject.GetComponent<PlayerComponents>();
        if (player != null)
        {
            aS.Play();
            if (!multiLevel)
            {
                liftDelayTimer = liftDelayDuration;
                liftDelayActive = true;
                player.transform.SetParent(this.transform);
            }
            else
            {
                if (!selectingFloor)
                {
                    floorSelectionPanel.SetActive(true);
                    player.transform.SetParent(this.transform);
                    Cursor.visible = true;
                    selectingFloor = true;
                    EventManager.FloorPanelToggle();
                }
   
            }
        }
    }

    void OnTriggerExit(Collider collision)
    {
        PlayerComponents player = collision.gameObject.GetComponent<PlayerComponents>();
        if (player != null)
        {
            player.transform.SetParent(null);
            liftDelayActive = false;
            liftDelayTimer = 0f;
        }
    }

    void LiftDelayTimer()
    {
        if (liftDelayActive)
        {
            if (liftDelayTimer > 0) liftDelayTimer -= Time.deltaTime;
            else
            {
                liftDelayActive = false;
                active = true;
            }
        }

    }

    public bool LiftTop()
    {
        if (reached) return true;
        else return false;
    }

    public void Activate()
    {
        active = true;
        reached = false;
        if(multiLevel) endPosition = floors[0].position;


    }

    public void SelectFloor(int number)
    {
        selectedFloor = number;
        endPosition = floors[number].position;
        topOfLift = floors[number].position;
        liftDelayTimer = liftDelayDuration;
        liftDelayActive = true;
        Cursor.visible = false;
        #if UNITY_EDITOR_64
        Cursor.visible = true;
        #endif
        floorSelectionPanel.SetActive(false);
        EventManager.FloorPanelToggle();
        selectingFloor = false;
        aS.Play();
    }

}
