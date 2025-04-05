using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGameView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text endGameText;

    [SerializeField]
    private Button restartButton;

    private GameControllerPod gameControllerPod;

    public void DoInit(GameControllerPod pod)
    {
        gameControllerPod = pod;

        gameObject.SetActive(false);

        pod.gameStateEvent.AddListener(onGameStateEvent);

        restartButton.onClick.AddListener(() =>
        {
            pod.gameStateEvent.Invoke(GameState.Start);
        });
    }

    private void onGameStateEvent(GameState gameState)
    {
        gameObject.SetActive(gameState == GameState.End);

        if (gameState == GameState.End)
        {
            endGameText.text =
                "You guessed the word: "
                + ((int)gameControllerPod.currentPosition.y + 1)
                + $" / {gameControllerPod.spawnGridSetting.y} Times \n Answer is: {gameControllerPod.answerWord.ToUpper()}";
        }
    }

    void OnDestroy()
    {
        gameControllerPod.gameStateEvent.RemoveListener(onGameStateEvent);
        restartButton.onClick.RemoveAllListeners();
    }
}
