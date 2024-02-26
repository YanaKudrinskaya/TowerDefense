using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SplashScreen : MonoBehaviour
{
    private void Awake()
    {
        int numMusic = FindObjectsOfType<SplashScreen>().Length;
        if (numMusic > 1 )
            Destroy(gameObject);
        else 
        DontDestroyOnLoad(gameObject);
    }
}
