using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Febucci.UI;

public class EndScene : MonoBehaviour
{
    [TextArea]
    [SerializeField] string[] sentences;
    [SerializeField] float timeToWait = 3f;
    int index = 0;

    [SerializeField] TypewriterByCharacter typewriter;
    private void Awake()
    {
        FindObjectOfType<AudioManager>().StopMusic();
        ShowNexSentence();
    }
    public void ShowNexSentence()
    {
        if (index < sentences.Length)
        {
            StartCoroutine(NextSentence());
        }
        else
        {
            StartCoroutine(LoadNextScene());
        }
    }

    IEnumerator NextSentence()
    {
        yield return new WaitForSeconds(timeToWait);
        typewriter.ShowText(sentences[index]);
        index++;
    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(timeToWait * 2);
        FindObjectOfType<AudioManager>().PlayMusic(0);
        SceneManager.LoadScene(0);
    }
}
