using TMPro;
using UnityEngine;

public class AnswerHintUIView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text answerText;

    public void SetAnswer(string answer)
    {
        answerText.text = answer;
    }
}
