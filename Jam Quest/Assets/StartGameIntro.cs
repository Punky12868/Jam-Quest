using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using Febucci.UI;

public class StartGameIntro : MonoBehaviour
{
    [TextArea]
    [SerializeField] string[] sentences;
    [SerializeField] float timeToWait = 3f;
    [SerializeField] GameObject introPanel;

    [SerializeField] UnityEvent onIntroStart;
    [SerializeField] UnityEvent onIntroEnd;
    int index = 0;

    [SerializeField] TypewriterByCharacter typewriter;
    private void Awake()
    {
        onIntroStart?.Invoke();
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
            StartCoroutine(DeletePanel());
        }
    }

    IEnumerator NextSentence()
    {
        yield return new WaitForSeconds(timeToWait);
        typewriter.ShowText(sentences[index]);
        index++;
    }

    IEnumerator DeletePanel()
    {
        yield return new WaitForSeconds(timeToWait / 2);
        onIntroEnd?.Invoke();
        Destroy(introPanel);
    }
}
