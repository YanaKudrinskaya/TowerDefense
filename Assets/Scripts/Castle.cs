using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Castle : MonoBehaviour
{
    [SerializeField] int playerLife = 10;
    [SerializeField] int damageCount = 1;
    [SerializeField] Text textLife;
    [SerializeField] AudioClip castleDamageSound;
    [SerializeField] AudioClip castleDeath;
    [SerializeField] ParticleSystem castleDamage;

    AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        textLife.text = playerLife.ToString();
    }
    public void DamageCastle()
    {
        audioSource.PlayOneShot(castleDamageSound);
        playerLife -= damageCount;
        textLife.text = playerLife.ToString();
        if (playerLife == 0) 
        {
            castleDamage.Play();
            audioSource.PlayOneShot(castleDeath);
            Invoke("gameOver", 2f);
        }
    }

    private void gameOver()
    {
        SceneManager.LoadScene(0);
    }
}
