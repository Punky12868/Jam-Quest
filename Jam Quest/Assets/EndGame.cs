using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class EndGame : MonoBehaviour
{
    [SerializeField] float timeToWait = 3f;
    [SerializeField] Volume volume;
    Bloom bloom;

    [SerializeField] float bloomTargetIntensity;
    [SerializeField] float bloomLerpSpeed;
    [SerializeField] float speedRateIncrease;

    bool isBloom = false;

    private void Awake()
    {
        volume.profile.TryGet<Bloom>(out bloom);
    }

    private void Update()
    {
        if (isBloom)
        {
            bloomLerpSpeed += speedRateIncrease * Time.deltaTime;
            bloom.intensity.value = Mathf.Lerp(bloom.intensity.value, bloomTargetIntensity, bloomLerpSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FindObjectOfType<PlayerController>().SetCanMove(false);
            FindObjectOfType<Death>().enabled = false;
            FindObjectOfType<RestartController>().enabled = false;
            FindObjectOfType<PauseGame>().enabled = false;

            EnemyBehaviour[] enemies = FindObjectsOfType<EnemyBehaviour>();

            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].enabled = false;
            }

            StartCoroutine(LoadNextScene());

            isBloom = true;
            CameraShake.Instance.ShakeCamera(5f, 3.5f);
        }
    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(timeToWait);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
