using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSelection : MonoBehaviour
{
    [SerializeField] LiftScript lift;

    public void Floor0()
    {
        lift.SelectFloor(0);
    }

    public void Floor1()
    {
        lift.SelectFloor(1);
    }

    public void Floor2()
    {
        lift.SelectFloor(2);
    }
    public void Floor3()
    {
        lift.SelectFloor(3);
    }
    public void Floor4()
    {
        lift.SelectFloor(4);
    }
}
