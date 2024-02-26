using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float moveStep;

    PathFinder pathFinder;
    EnemyDamage dathParticle;
    Castle castle;
    Vector3 targetPosition;

    void Start()
    {
        castle = FindObjectOfType<Castle>();
        dathParticle = GetComponent<EnemyDamage>();
        pathFinder = FindObjectOfType<PathFinder>();
        var path = pathFinder.GetPath();
        StartCoroutine(EnemyMove(path));
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveStep);
    }

    IEnumerator EnemyMove(List<WayPoint> path)
    {
        foreach (WayPoint wayPoint in path)
        {
            transform.LookAt(wayPoint.transform);
            targetPosition = wayPoint.transform.position;
            yield return new WaitForSeconds(speed);
            
        }
        castle.DamageCastle();
        dathParticle.DestroyEnemy(false);
    }
}
