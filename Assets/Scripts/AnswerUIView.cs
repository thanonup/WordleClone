using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnswerUIView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text answerText;

    public void SetAnswer(string answer)
    {
        answerText.text = answer;
    }
}
