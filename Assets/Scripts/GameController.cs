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
    private AnswerUIView answerUIView;

    [SerializeField]
    private VirtualKeyboardView virtualKeyboardView;

    private GameControllerPod gameControllerPod;

    void Start()
    {
        gameControllerPod = new GameControllerPod();
        virtualKeyboardView.DoInit(gameControllerPod);
        DoInit();
    }

    public void DoInit()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("words");

        gameControllerPod.setWords(JsonUtility.FromJson<WordData>(jsonFile.text).words);
        gameControllerPod.setCurrentWord(
            gameControllerPod.words[UnityEngine.Random.Range(0, gameControllerPod.words.Length)]
        );
        answerUIView.SetAnswer(gameControllerPod.currentWord);

        SpawnCharacterCellGroup();

        gameControllerPod.currentDataEvent.Invoke(new Vector2(0, 0), "");
    }

    private void SpawnCharacterCellGroup()
    {
        for (int i = 0; i < spawnGridSetting.x; i++)
        {
            for (int j = 0; j < spawnGridSetting.y; j++)
            {
                GameObject characterCell = Instantiate(characterPrefab, spawnerObject.transform);
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
