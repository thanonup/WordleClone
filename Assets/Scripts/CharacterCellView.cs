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
        gameControllerPod.currentDataEvent.AddListener(
            (vec2, character) =>
            {
                if (characterCellData.position == vec2)
                {
                    characterText.text = character;
                    // SetCharacterCellType();
                }
            }
        );
    }

    private void SetCharacterCellType(CharacterCellType characterCellType)
    {
        characterText.gameObject.SetActive(characterCellType != CharacterCellType.Idle);
        switch (characterCellType)
        {
            case CharacterCellType.Idle:
                setCellStyle("#181818", "#656565");
                break;
            case CharacterCellType.Typing:
                setCellStyle("#181818", "#A6A6A6");
                break;
            case CharacterCellType.Correct:
                setCellStyle("#2EB245", "#2EB245");
                break;
            case CharacterCellType.Wrong:
                setCellStyle("#656565", "#656565");
                break;
            case CharacterCellType.WrongPosition:
                setCellStyle("#CBBB3E", "#CBBB3E");
                break;
        }
    }

    private void setCellStyle(string backgroundColorCode, string outlineColorCode)
    {
        Color backgroundColor;
        ColorUtility.TryParseHtmlString(backgroundColorCode, out backgroundColor);
        backgroundImage.color = backgroundColor;

        Color outlineColor;
        ColorUtility.TryParseHtmlString(outlineColorCode, out outlineColor);
        outlineImage.effectColor = outlineColor;
    }
}
