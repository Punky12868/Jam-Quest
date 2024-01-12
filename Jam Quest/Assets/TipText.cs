using Febucci.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TipText : MonoBehaviour
{
    [TextArea]
    [SerializeField] string sentence;
    [SerializeField] float timeToWait = 3f;
    int index = 0;

    [SerializeField] TypewriterByCharacter typewriter;

    public void ShowNexSentence()
    {
        if (index < 1)
        {
            StartCoroutine(NextSentence());
        }
        else
        {
            StartCoroutine(UndoSentence());
        }
    }

    public void OnDissapear()
    {
        StartCoroutine(Delete());
    }

    IEnumerator NextSentence()
    {
        yield return new WaitForSeconds(timeToWait);
        typewriter.ShowText(sentence);
        index++;
    }

    IEnumerator UndoSentence()
    {
        yield return new WaitForSeconds(timeToWait * 2);
        typewriter.StartDisappearingText();
    }

    IEnumerator Delete()
    {
        yield return new WaitForSeconds(timeToWait);
    }
}
