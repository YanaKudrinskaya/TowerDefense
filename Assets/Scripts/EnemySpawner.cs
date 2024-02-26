using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] [Range(0.1f, 20f)] float spawnInterval;
    [SerializeField] EnemyMovement enemyPrefab;
    [SerializeField] AudioClip enemySpownSoundFX;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(EnemySpawn());

    }
    IEnumerator EnemySpawn()
    {
        while (true)
        {
            audioSource.PlayOneShot(enemySpownSoundFX);
            var newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            newEnemy.transform.parent = transform;
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
