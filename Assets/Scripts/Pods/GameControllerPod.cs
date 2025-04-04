using System;
using UnityEngine;
using UnityEngine.Events;

public class GameControllerPod
{
    public UnityEvent<string> updateDataEvent = new UnityEvent<string>();
    public string[] wordsData;

    public Vector2 spawnGridSetting;
    public Vector2 currentPosition;
    public string currentWord;

    public GameControllerPod(Vector2 spawnGridSetting)
    {
        this.spawnGridSetting = spawnGridSetting;
        updateDataEvent.AddListener(
            (character) =>
            {
                currentWord = character;
                Debug.Log($"Current Position: {currentPosition} Character: {character}");
            }
        );
    }

    public void setNextPosition()
    {
        currentPosition.x += 1;
        if (currentPosition.x > spawnGridSetting.x)
        {
            currentPosition.x = spawnGridSetting.x;
        }
    }

    public void setPrevPosition()
    {
        currentPosition.x -= 1;
        if (currentPosition.x < 0)
        {
            currentPosition.x = 0;
        }
    }

    public void setWords(string[] words)
    {
        this.wordsData = words;

        Debug.Log(string.Join(", ", words));
    }

    public void setCurrentWord(string currentWord)
    {
        this.currentWord = currentWord;
    }
}
