using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Setting Word Group")]
    [SerializeField]
    private Vector2 spawnGridSetting;

    [Header("")]
    [SerializeField]
    private GameObject characterPrefab;

    [SerializeField]
    private GridLayoutGroup gridLayoutGroup;

    [SerializeField]
    private GameObject spawnerObject;

    [SerializeField]
    private AnswerHintUIView answerUIView;

    [SerializeField]
    private VirtualKeyboardView virtualKeyboardView;

    [SerializeField]
    private PopupMessageView popupMessageView;

    [SerializeField]
    private EndGameView endGameView;

    private List<CharacterCellView> characterCellViewList = new List<CharacterCellView>();
    private GameControllerPod gameControllerPod;

    void Start()
    {
        gameControllerPod = new GameControllerPod(spawnGridSetting);
        virtualKeyboardView.DoInit(gameControllerPod);
        popupMessageView.DoInit(gameControllerPod);
        endGameView.DoInit(gameControllerPod);

        gridLayoutGroup.constraintCount = (int)spawnGridSetting.x;

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
                if (gameState == GameState.Start)
                {
                    characterCellViewList.Clear();
                    gameControllerPod.resetAll();
                    foreach (Transform child in spawnerObject.transform)
                    {
                        Destroy(child.gameObject);
                    }

                    gameControllerPod.popupMessageEvent.Invoke("");
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

        gameControllerPod.summitAnswerEvent.AddListener(OnSummitAnswerEvent);
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

                CharacterCellView cell = characterCell.GetComponent<CharacterCellView>();
                cell.DoInit(characterCellData, gameControllerPod);

                characterCellViewList.Add(cell);
            }
        }
    }

    private void OnSummitAnswerEvent()
    {
        List<char> answerCharacterLineTemp = new List<char>();
        List<CharacterCellView> currentLine = characterCellViewList.FindAll(cell =>
            (int)cell.GetCharacterCellData().position.y == (int)gameControllerPod.currentPosition.y
        );

        for (int i = 0; i < currentLine.Count; i++)
        {
            CharacterCellView cell = currentLine[i];
            char guessedChar = cell.GetCurrentCharInput().ToLower()[0];
            CharacterCellType characterCellType = gameControllerPod.CheckCharacterInLine(
                i,
                guessedChar,
                answerCharacterLineTemp
            );

            cell.SetCharacterCellType(characterCellType);
            gameControllerPod.UpdateUsedKey(guessedChar.ToString(), characterCellType);
        }

        gameControllerPod.CheckAnswer();
    }
}
