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

    public void DoInit(string character, GameControllerPod gameControllerPod)
    {
        keyBoardText.text = character;
        keyBoardButton.onClick.AddListener(() => {
            // gameControllerPod.currentPosition.Invoke(new Vector2(0, 0), character);
        });
    }
}
