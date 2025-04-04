using System;
using UnityEngine;
using UnityEngine.Events;

public class GameControllerPod
{
    public UnityEvent<Vector2, string> currentDataEvent = new UnityEvent<Vector2, string>();
    public string[] words;

    public Vector2 currentPosition;
    public string currentWord;

    public GameControllerPod()
    {
        currentDataEvent.AddListener(
            (vec2, character) =>
            {
                currentPosition = vec2;
                currentWord = character;
                Debug.Log($"Current Position: {vec2} Character: {character}");
            }
        );
    }

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
