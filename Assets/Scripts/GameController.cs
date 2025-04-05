using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Setting Word Group")]
    [SerializeField]
    private Vector2 spawnGridSetting;

    [Header("")]
    [SerializeField]
    private GameObject characterPrefab;

    [SerializeField]
    private GameObject spawnerObject;

    [SerializeField]
    private AnswerHintUIView answerUIView;

    [SerializeField]
    private VirtualKeyboardView virtualKeyboardView;

    [SerializeField]
    private PopupMessageView popupMessageView;

    private GameControllerPod gameControllerPod;

    void Start()
    {
        gameControllerPod = new GameControllerPod(spawnGridSetting);
        virtualKeyboardView.DoInit(gameControllerPod);
        popupMessageView.DoInit(gameControllerPod);

        TextAsset jsonFile = Resources.Load<TextAsset>("words");
        gameControllerPod.SetWords(JsonUtility.FromJson<WordData>(jsonFile.text).words);

        DoInit();

        gameControllerPod.gameStateEvent.Invoke(GameState.Start);
    }

    public void DoInit()
    {
        gameControllerPod.gameStateEvent.AddListener(
            (gameState) =>
            {
                Debug.Log($"Game State: {gameState}");
                if (gameState == GameState.Start)
                {
                    gameControllerPod.SetCurrentWord(
                        gameControllerPod.wordsData[
                            UnityEngine.Random.Range(0, gameControllerPod.wordsData.Length)
                        ]
                    );
                    answerUIView.SetAnswer(gameControllerPod.answerWord);

                    SpawnCharacterCellGroup();
                }
            }
        );
    }

    private void SpawnCharacterCellGroup()
    {
        for (int i = 0; i < spawnGridSetting.y; i++)
        {
            for (int j = 0; j < spawnGridSetting.x; j++)
            {
                GameObject characterCell = Instantiate(characterPrefab, spawnerObject.transform);
                characterCell.name = $"CharacterCell_{i}_{j}";
                CharacterCellData characterCellData = new CharacterCellData(
                    new Vector2(j, i),
                    CharacterCellType.Idle
                );
                characterCell
                    .GetComponent<CharacterCellView>()
                    .DoInit(characterCellData, gameControllerPod);
            }
        }
    }
}
