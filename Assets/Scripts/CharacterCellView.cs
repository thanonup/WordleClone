using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class CharacterCellView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text characterText;

    [SerializeField]
    private Image backgroundImage;

    [SerializeField]
    private Outline outlineImage;

    private CharacterCellData characterCellData;

    private GameControllerPod gameControllerPod;

    public void DoInit(CharacterCellData characterCellData, GameControllerPod gameControllerPod)
    {
        this.characterCellData = characterCellData;
        this.gameControllerPod = gameControllerPod;
        SetCharacterCellType(characterCellData.characterCellType);
        AddListener();
    }

    private void AddListener()
    {
        gameControllerPod.updateDataEvent.AddListener(
            (character) =>
            {
                if (characterCellData.position == gameControllerPod.currentPosition)
                {
                    characterText.text = character;
                    SetCharacterCellType(
                        character == "" ? CharacterCellType.Idle : CharacterCellType.Typing
                    );
                }
            }
        );

        gameControllerPod.summitAnswerEvent.AddListener(() =>
        {
            if (characterCellData.position.y != gameControllerPod.currentPosition.y)
                return;

            string answer = gameControllerPod.answerWord.ToLower();
            string guessAnswer = gameControllerPod.currentLineAnswer.ToLower();
            char guessedChar = guessAnswer[(int)characterCellData.position.x];
            if (answer.Contains(guessedChar))
            {
                if (guessedChar == answer[(int)characterCellData.position.x])
                {
                    SetCharacterCellType(CharacterCellType.Correct);
                }
                else
                {
                    SetCharacterCellType(CharacterCellType.WrongPosition);
                }
            }
            else
            {
                SetCharacterCellType(CharacterCellType.Wrong);
            }
        });
    }

    private void SetCharacterCellType(CharacterCellType characterCellType)
    {
        characterText.gameObject.SetActive(characterCellType != CharacterCellType.Idle);
        switch (characterCellType)
        {
            case CharacterCellType.Idle:
                SetCellStyle("#181818", "#656565");
                break;
            case CharacterCellType.Typing:
                SetCellStyle("#181818", "#A6A6A6");
                break;
            case CharacterCellType.Correct:
                SetCellStyle("#2EB245", "#2EB245");
                break;
            case CharacterCellType.Wrong:
                SetCellStyle("#656565", "#656565");
                break;
            case CharacterCellType.WrongPosition:
                SetCellStyle("#CBBB3E", "#CBBB3E");
                break;
        }
    }

    private void SetCellStyle(string backgroundColorCode, string outlineColorCode)
    {
        Color backgroundColor;
        ColorUtility.TryParseHtmlString(backgroundColorCode, out backgroundColor);
        backgroundImage.color = backgroundColor;

        Color outlineColor;
        ColorUtility.TryParseHtmlString(outlineColorCode, out outlineColor);
        outlineImage.effectColor = outlineColor;
    }
}
