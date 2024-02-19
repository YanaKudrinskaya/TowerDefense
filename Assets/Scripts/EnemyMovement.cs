using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    PathFinder pathFinder;
    void Start()
    {
        pathFinder = FindObjectOfType<PathFinder>();
        var path = pathFinder.GetPath();
        StartCoroutine(EnemyMove(path));
    }

    IEnumerator EnemyMove(List<WayPoint> path)
    {
        foreach (WayPoint cube in path)
        {
            transform.position = cube.transform.position;
            yield return new WaitForSeconds(1f);
        }
    }
}
