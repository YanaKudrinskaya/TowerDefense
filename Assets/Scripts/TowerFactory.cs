using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] int towersLimit = 4;
    [SerializeField] Tower towerPrefab;

    Queue<Tower> towerQueue = new Queue<Tower>();

    public void AddTower(WayPoint baseWayPoint)
    {
        int towerQuantity = towerQueue.Count;
        if (towerQuantity < towersLimit)
        {
            InstantiateNewTower(baseWayPoint);
        }
        else
        {
            MoveTowerToNewPosition(baseWayPoint);
        }
    }
    private void InstantiateNewTower(WayPoint baseWayPoint)
    {
        var newTower = Instantiate(towerPrefab, baseWayPoint.transform.position, Quaternion.identity);
        newTower.transform.parent = transform;
        baseWayPoint.isPlaceable = false;
        newTower.baseWayPoint = baseWayPoint;
        towerQueue.Enqueue(newTower);
    }
    private void MoveTowerToNewPosition(WayPoint newbaseWayPoint)
    {
        Tower oldTower = towerQueue.Dequeue();

        oldTower.transform.position = newbaseWayPoint.transform.position;
        oldTower.baseWayPoint.isPlaceable = true;
        newbaseWayPoint.isPlaceable = false;
        oldTower.baseWayPoint = newbaseWayPoint;

        towerQueue.Enqueue(oldTower);
    }
}
