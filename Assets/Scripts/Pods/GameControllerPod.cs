using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameControllerPod
{
    public UnityEvent<GameState> gameStateEvent = new UnityEvent<GameState>();
    public UnityEvent<string> updateDataEvent = new UnityEvent<string>();
    public UnityEvent summitAnswerEvent = new UnityEvent();
    public UnityEvent<string, CharacterCellType> updateUsedKeyEvent =
        new UnityEvent<string, CharacterCellType>();

    public UnityEvent<string> popupMessageEvent = new UnityEvent<string>();
    public string[] wordsData;

    public GameState gameState;
    public Vector2 spawnGridSetting;
    public Vector2 currentPosition;
    public string answerWord;
    public string currentLineTypingAnswer = "";
    public List<string> usedKeyList = new List<string>();

    public GameControllerPod(Vector2 spawnGridSetting)
    {
        this.spawnGridSetting = spawnGridSetting;
        updateDataEvent.AddListener(
            (character) =>
            {
                if (character == "")
                {
                    if (currentLineTypingAnswer != "")
                        currentLineTypingAnswer = currentLineTypingAnswer.Remove(
                            currentLineTypingAnswer.Length - 1
                        );
                }
                else
                {
                    if (currentLineTypingAnswer.Length >= spawnGridSetting.x)
                        return;

                    currentLineTypingAnswer += character;
                }
            }
        );

        gameStateEvent.AddListener(
            (gameState) =>
            {
                this.gameState = gameState;
            }
        );
    }

    public void resetAll()
    {
        usedKeyList.Clear();
        currentPosition.x = 0;
        currentPosition.y = 0;
        currentLineTypingAnswer = "";
        answerWord = "";
    }

    public void CheckAnswer()
    {
        if (currentLineTypingAnswer.ToLower() == answerWord.ToLower())
        {
            popupMessageEvent.Invoke("-- Correct Answer!! --");
            gameStateEvent.Invoke(GameState.End);
        }
        else
        {
            if (currentPosition.y >= spawnGridSetting.y - 1)
            {
                popupMessageEvent.Invoke("Answers is : " + answerWord.ToUpper());
                gameStateEvent.Invoke(GameState.End);
                return;
            }
            else
            {
                SetNextLinePosition();
            }
        }
    }

    private void SetNextLinePosition()
    {
        if (currentPosition.y >= spawnGridSetting.y)
            return;
        currentLineTypingAnswer = "";
        currentPosition.x = 0;
        currentPosition.y += 1;
    }

    public void SetNextPosition()
    {
        currentPosition.x += 1;
        if (currentPosition.x > spawnGridSetting.x)
        {
            currentPosition.x = spawnGridSetting.x;
        }
    }

    public void SetPrevPosition()
    {
        currentPosition.x -= 1;

        if (currentPosition.x < 0)
        {
            currentPosition.x = 0;
        }
    }

    public void SetWords(string[] words)
    {
        this.wordsData = words;

        Debug.Log(string.Join(", ", words));
    }

    public void SetCurrentWord(string currentWord)
    {
        this.answerWord = currentWord;
    }

    public void UpdateUsedKey(string key, CharacterCellType type)
    {
        if (!usedKeyList.Contains(key))
        {
            usedKeyList.Add(key);
            updateUsedKeyEvent.Invoke(key, type);
        }
    }
}
