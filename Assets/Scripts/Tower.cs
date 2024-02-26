using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Tower : MonoBehaviour
{
    [SerializeField] Transform towerTop;
    [SerializeField] Transform targetEnemy;
    [SerializeField] float shootRange;
    [SerializeField] ParticleSystem bulletParticles;

    public WayPoint baseWayPoint;
    private void Update()
    {
        SetTargetEnemy();
        if(targetEnemy)
        {
            towerTop.LookAt(targetEnemy);
            Fire();
        }
        else
        {
            shoot(false);
        }
    }
    private void SetTargetEnemy()
    {
        var sceneEnemies = FindObjectsOfType<EnemyDamage>();
        if (sceneEnemies.Length == 0) { return; }
        Transform closestEnemy = sceneEnemies[0].transform;
        foreach (EnemyDamage test in sceneEnemies)
        {
            closestEnemy = GetClosestEnemy(closestEnemy.transform, test.transform);
        }
        targetEnemy = closestEnemy;
    }
    private Transform GetClosestEnemy(Transform enemyA, Transform enemyB)
    {
        var distToA = Vector3.Distance(enemyA.position, transform.position);
        var distToB = Vector3.Distance(enemyB.position, transform.position);
        if(distToA < distToB)
        {
            return enemyA;
        }
        return enemyB;
    }

    private void Fire()
    {
        float distanceToEnemy = Vector3.Distance(targetEnemy.position, transform.position);
        if(distanceToEnemy <= shootRange)
        {
            shoot(true);
        }
        else
        {
            shoot(false);
        }
    }

    private void shoot(bool isActive)
    {
        var emission = bulletParticles.emission;
        emission.enabled = isActive;
    }
}
