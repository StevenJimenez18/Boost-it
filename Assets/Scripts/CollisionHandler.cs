using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delayTime = 1f;
    [SerializeField] AudioClip successSound;
    [SerializeField] AudioClip crashSound;

    AudioSource playerAudio;

    bool isTransitioning = false;

    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning) { return; }
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("You have collided with a friendly object.");
                break;
            case "Finish":
                SuccessSequence();
                break;
            default:
                CrashSequence();
                break;
        }
    }

    void ChangeState()
    {
        if (isTransitioning)
        {
            isTransitioning = false;
        }
        else
        {
            isTransitioning = true;
        }
    }
    void SuccessSequence()
    {
        ChangeState();
        playerAudio.Stop();
        playerAudio.PlayOneShot(successSound);
        GetComponent<Movement>().enabled = false;
        Invoke("NextLevel", delayTime);
    }
    void CrashSequence()
    {
        ChangeState();
        playerAudio.Stop();
        playerAudio.PlayOneShot(crashSound);
        //Add particle effect on crash
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", delayTime);
    }
    void NextLevel()
    {
        ChangeState();
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
    void ReloadLevel()
    {
        ChangeState();
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

}
