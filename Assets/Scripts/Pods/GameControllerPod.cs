using UnityEngine;
using UnityEngine.Events;

public class GameControllerPod
{
    public UnityEvent<GameState> gameStateEvent = new UnityEvent<GameState>();
    public UnityEvent<string> updateDataEvent = new UnityEvent<string>();
    public UnityEvent summitAnswerEvent = new UnityEvent();
    public UnityEvent<string> popupMessage = new UnityEvent<string>();
    public string[] wordsData;

    public GameState gameState;
    public Vector2 spawnGridSetting;
    public Vector2 currentPosition;
    public string answerWord;
    public string currentLineAnswer = "";

    public GameControllerPod(Vector2 spawnGridSetting)
    {
        this.spawnGridSetting = spawnGridSetting;
        updateDataEvent.AddListener(
            (character) =>
            {
                if (character == "")
                {
                    if (currentLineAnswer != "")
                        currentLineAnswer = currentLineAnswer.Remove(currentLineAnswer.Length - 1);
                }
                else
                {
                    if (currentLineAnswer.Length >= spawnGridSetting.x)
                        return;

                    currentLineAnswer += character;
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
        currentPosition.x = 0;
        currentPosition.y = 0;
        currentLineAnswer = "";
        answerWord = "";
    }

    public void CheckAnswer()
    {
        if (currentLineAnswer.ToLower() == answerWord.ToLower())
        {
            popupMessage.Invoke("-- Correct Answer!! --");
            gameStateEvent.Invoke(GameState.End);
        }
        else
        {
            if (currentPosition.y >= spawnGridSetting.y - 1)
            {
                popupMessage.Invoke("Answers is : " + answerWord.ToUpper());
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
        currentLineAnswer = "";
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
}
