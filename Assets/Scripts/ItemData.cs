using UnityEngine;
public enum ItemType
{
    SpeedBoost,
    JumpBoost
}

// 유니티 메뉴에서 파일 생성
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item Data")]
public class ItemData : ScriptableObject
{
    [Header("아이템 정보")]
    public string itemName;        
    public string description;   
    public Sprite icon;        

    [Header("아이템 효과")]
    public ItemType itemType;       // 아이템 효과 종류
    public float duration;       
    public float value;           
}