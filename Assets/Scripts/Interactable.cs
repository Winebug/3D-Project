using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool isLadder = false; // 사다리 확인

    public string promptMessage;
    public ItemData itemData;

    public string objectName;
    [TextArea(3, 5)]
    public string description;
}