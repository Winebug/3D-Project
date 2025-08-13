using UnityEngine;
public enum ItemType
{
    SpeedBoost,
    JumpBoost
}

// ����Ƽ �޴����� ���� ����
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item Data")]
public class ItemData : ScriptableObject
{
    [Header("������ ����")]
    public string itemName;        
    public string description;   
    public Sprite icon;        

    [Header("������ ȿ��")]
    public ItemType itemType;       // ������ ȿ�� ����
    public float duration;       
    public float value;           
}