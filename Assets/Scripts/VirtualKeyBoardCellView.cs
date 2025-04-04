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
            if (character == "Enter")
            {
                Debug.Log("Button Clicked: Enter");
            }
            else if (character == "Del")
            {
                Debug.Log("Button Clicked: Del");
                gameControllerPod.setPrevPosition();
                gameControllerPod.updateDataEvent.Invoke("");
            }
            else
            {
                Debug.Log("Button Clicked: " + character);
                gameControllerPod.updateDataEvent.Invoke(character);
                gameControllerPod.setNextPosition();
            }
        });
    }
}
