using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    //Variables
    Movement movementScript;
    AudioSource audioSource;

    bool isTransitioning = false;
    bool crashEnabled = true;

    [SerializeField] AudioClip victorySound;
    [SerializeField] AudioClip crashSound;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] float levelDelay = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        movementScript = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        SkipLevel();
        GodMode();
    }

    void GodMode()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            crashEnabled = !crashEnabled;
        }
    }


    void OnCollisionEnter(Collision other) 
    {
        if(isTransitioning || !crashEnabled) { return; }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing is friendly");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();      
                break;   
        }
    }

    void StartSuccessSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(victorySound);
        successParticles.Play();
        movementScript.enabled = false;
        Invoke("LoadNextLevel", levelDelay);
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSound);
        crashParticles.Play();
        movementScript.enabled = false;
        Invoke("ReloadLevel", levelDelay);
        
    }

    // Restart at level 1
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        //Current Scene
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        //Next Scene
        int nextSceneIndex = currentSceneIndex + 1;
        //If no next scene, go back to start
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void SkipLevel()
    {
        if (Input.GetKey(KeyCode.L))
        {
            LoadNextLevel();
        }
    }
}
