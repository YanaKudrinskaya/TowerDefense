using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[SelectionBase]
public class EnemyDamage : MonoBehaviour
{
    [SerializeField] int hitPoints = 10;
    [SerializeField] ParticleSystem hitParticles;
    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] AudioClip hipSource;
    [SerializeField] AudioClip enemySoundDeath;

    Text score;
    int currentScore;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        score = GameObject.Find("Score").GetComponent<Text>();
    }
    private void OnParticleCollision(GameObject other)
    {
        if (hitPoints <= 0)
        {
            DestroyEnemy(true);
        }
        Hit();
    }
    public void DestroyEnemy(bool addScore)
    {
        if(addScore)
        {
            currentScore = int.Parse(score.text);
            currentScore++;
            score.text = currentScore.ToString();
        }
        var destroyFX = Instantiate(deathParticles, transform.position, Quaternion.identity);
        destroyFX.Play();
        float destroyFX_duration = destroyFX.main.duration;

        AudioSource.PlayClipAtPoint(enemySoundDeath, Camera.main.gameObject.transform.position);

        Destroy(destroyFX.gameObject, destroyFX_duration);
        Destroy(gameObject);
    }
    private void Hit()
    {
        audioSource.PlayOneShot(hipSource);
        hitParticles.Play();
        hitPoints --;
    }
}
