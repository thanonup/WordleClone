using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirtualKeyboardView : MonoBehaviour
{
    [SerializeField]
    private GameObject linePrefab;

    [SerializeField]
    private GameObject virtualKeyboardCellPrefab;
    private GameControllerPod gameControllerPod;

    public void DoInit(GameControllerPod gameControllerPod)
    {
        this.gameControllerPod = gameControllerPod;
        CreateVirtualKeyboard();
    }

    private void CreateVirtualKeyboard()
    {
        List<List<string>> keyboards = new List<List<string>>
        {
            new() { "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P" },
            new() { "A", "S", "D", "F", "G", "H", "J", "K", "L" },
            new() { "Enter", "Z", "X", "C", "V", "B", "N", "M", "Del" },
        };

        Debug.Log(string.Join(", ", keyboards[0]));

        for (int i = 0; i < keyboards.Count; i++)
        {
            GameObject line = Instantiate(linePrefab, transform);
            for (int j = 0; j < keyboards[i].Count; j++)
            {
                GameObject characterCell = Instantiate(virtualKeyboardCellPrefab, line.transform);
                characterCell.name = $"VirtualKeyboardCell_{keyboards[i][j]}";
                characterCell
                    .GetComponent<VirtualKeyBoardCellView>()
                    .DoInit(keyboards[i][j], gameControllerPod);
            }
        }
    }
}
