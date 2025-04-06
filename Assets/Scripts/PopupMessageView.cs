using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupMessageView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text messageText;

    private GameControllerPod gameControllerPod;

    public void DoInit(GameControllerPod gameControllerPod)
    {
        gameObject.SetActive(false);

        this.gameControllerPod = gameControllerPod;
        gameControllerPod.popupMessageEvent.AddListener(
            (message) =>
            {
                if (message == "")
                {
                    gameObject.SetActive(false);
                    return;
                }
                messageText.text = message;
                gameObject.SetActive(true);
            }
        );
    }
}
