using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delayTime = 1f;
    [SerializeField] AudioClip successSound;
    [SerializeField] AudioClip crashSound;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem rocketPartciles;
    AudioSource playerAudio;
    bool isTransitioning = false;

    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || GetComponent<Movement>().collisionDisabled) { return; }
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
        rocketPartciles.Stop();
        playerAudio.Stop();
        playerAudio.PlayOneShot(successSound);
        successParticles.Play(successSound);
        GetComponent<Movement>().enabled = false;
        Invoke("NextLevel", delayTime);
    }

    void CrashSequence()
    {
        ChangeState();
        rocketPartciles.Stop();
        playerAudio.Stop();
        playerAudio.PlayOneShot(crashSound);
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", delayTime);
    }

    public void NextLevel()
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
