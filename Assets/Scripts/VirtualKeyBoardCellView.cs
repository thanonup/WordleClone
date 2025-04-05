using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VirtualKeyBoardCellView : MonoBehaviour
{
    [SerializeField]
    private Button keyBoardButton;

    [SerializeField]
    private TMP_Text keyBoardText;

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
                Debug.Log("Button Clicked: Enter");
                if (
                    gameControllerPod.currentLineAnswer.Length
                    == gameControllerPod.spawnGridSetting.x
                )
                {
                    gameControllerPod.summitAnswerEvent.Invoke();
                    gameControllerPod.CheckAnswer();
                }
                else
                {
                    gameControllerPod.popupMessage.Invoke("Not enough letters");
                }
            }
            else if (character == "Del")
            {
                Debug.Log("Button Clicked: Del");
                gameControllerPod.SetPrevPosition();
                gameControllerPod.updateDataEvent.Invoke("");
                gameControllerPod.popupMessage.Invoke("");
            }
            else
            {
                Debug.Log("Button Clicked: " + character);
                gameControllerPod.updateDataEvent.Invoke(character);
                gameControllerPod.SetNextPosition();
                gameControllerPod.popupMessage.Invoke("");
            }
        });
    }
}
