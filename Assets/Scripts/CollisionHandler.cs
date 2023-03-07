using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    //Variables
    Movement movementScript;
    [SerializeField] float levelDelay = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
       movementScript = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other) 
    {
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
        //TODO Add sound SFX on success
        //TODO Add particle FX on success
        movementScript.enabled = false;
        Invoke("LoadNextLevel", levelDelay);
    }

    void StartCrashSequence()
    {
        //TODO Add sound SFX on crash
        //TODO Add particle FX on crash
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
}
