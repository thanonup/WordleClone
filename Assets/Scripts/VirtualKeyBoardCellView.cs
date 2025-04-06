using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VirtualKeyBoardCellView : MonoBehaviour
{
    [SerializeField]
    private Button keyBoardButton;

    [SerializeField]
    private TMP_Text keyBoardText;

    [SerializeField]
    private Image backgorundButton;

    private GameControllerPod gameControllerPod;

    public void DoInit(string character, GameControllerPod pod)
    {
        gameControllerPod = pod;
        keyBoardText.text = character;
        keyBoardButton.onClick.AddListener(() =>
        {
            if (gameControllerPod.gameState != GameState.Start)
                return;

            if (character == "Enter")
            {
                if (
                    gameControllerPod.currentLineTypingAnswer.Length
                    == gameControllerPod.spawnGridSetting.x
                )
                {
                    gameControllerPod.summitAnswerEvent.Invoke();
                }
                else
                {
                    gameControllerPod.popupMessageEvent.Invoke("Not enough letters");
                }
            }
            else if (character == "Del")
            {
                gameControllerPod.SetPrevPosition();
                gameControllerPod.updateDataEvent.Invoke("");
                gameControllerPod.popupMessageEvent.Invoke("");
            }
            else
            {
                gameControllerPod.updateDataEvent.Invoke(character);
                gameControllerPod.SetNextPosition();
                gameControllerPod.popupMessageEvent.Invoke("");
            }
        });

        AddListener();
    }

    private void AddListener()
    {
        gameControllerPod.updateUsedKeyEvent.AddListener(onUpdateDataEvent);
        gameControllerPod.gameStateEvent.AddListener(onGameStateEvent);
    }

    private void onGameStateEvent(GameState gameState)
    {
        if (gameState == GameState.Start)
        {
            SetCharacterCellType(CharacterCellType.Idle);
        }
    }

    private void onUpdateDataEvent(string character, CharacterCellType characterCellType)
    {
        if (keyBoardText.text.ToLower() == character.ToLower())
        {
            SetCharacterCellType(characterCellType);
        }
    }

    private void SetCharacterCellType(CharacterCellType characterCellType)
    {
        switch (characterCellType)
        {
            case CharacterCellType.Idle:
            case CharacterCellType.Typing:
                SetCellStyle("#FFFFFF");
                break;
            case CharacterCellType.Correct:
                SetCellStyle("#2EB245");
                break;
            case CharacterCellType.Wrong:
                SetCellStyle("#656565");
                break;
            case CharacterCellType.WrongPosition:
                SetCellStyle("#CBBB3E");
                break;
        }
    }

    private void SetCellStyle(string backgroundColorCode)
    {
        Color backgroundColor;
        ColorUtility.TryParseHtmlString(backgroundColorCode, out backgroundColor);
        backgorundButton.color = backgroundColor;
    }
}
