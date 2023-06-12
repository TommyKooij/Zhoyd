using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorActivator : MonoBehaviour
{
    #region VARIABLES
    public GameObject elevator;
    private ElevatorController elevators;

    private bool destroyElevator, spawnElevator;
    #endregion

    // Update is called once per frame
    void Update()
    {
        if (destroyElevator == true)
        {
            elevator.SetActive(false);

            if (!elevator.activeInHierarchy)
            {
                destroyElevator = false;
            }
        }

        if (spawnElevator == true)
        {
            if (!elevator.activeInHierarchy)
            {
                elevator.SetActive(true);
                spawnElevator = false;
            }
        }
    }

    public void DestroyElevator()
    {
        destroyElevator = true;
    }

    public void SpawnElevator()
    {
        spawnElevator = true;
    }
}
