using System;
using UnityEngine;

public class GameControllerPod
{
    public string[] words;
    public string currentWord;

    public void setWords(string[] words)
    {
        this.words = words;

        Debug.Log(string.Join(", ", words));
    }

    public void setCurrentWord(string currentWord)
    {
        this.currentWord = currentWord;
    }
}
