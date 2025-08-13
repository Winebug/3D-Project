using System.Collections;
using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    private PlayerController playerController;

    [Header("�׽�Ʈ�� ������")]
    public ItemData testItem;

    void Awake()
    {
        // PlayerController ��ũ��Ʈ�� �̸� ã�ƿ�
        playerController = GetComponent<PlayerController>();
    }

    // �������� ����ϴ� �Լ� (�ܺο��� ȣ��)
    public void UseItem(ItemData item)
    {
        Debug.Log($"{item.itemName} ������ ���!");

        // ������ ������ ���� �ٸ� �ڷ�ƾ�� ����
        if (item.itemType == ItemType.SpeedBoost)
        {
            // �̹� ���ǵ� �ν�Ʈ�� ���� ���̶�� ���� ���� ���߰� ���� ����
            StopCoroutine("SpeedBoostCoroutine");
            StartCoroutine(SpeedBoostCoroutine(item.duration, item.value));
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (testItem != null)
            {
                UseItem(testItem);
            }
        }
    }

    // ���ǵ� �ν�Ʈ ȿ���� ó���ϴ� �ڷ�ƾ
    private IEnumerator SpeedBoostCoroutine(float duration, float speedMultiplier)
    {
        // 
        float originalSpeed = playerController.moveSpeed;

        // 
        playerController.moveSpeed *= speedMultiplier;
        Debug.Log($"�ӵ� ����! ���� �ӵ�: {playerController.moveSpeed}");

        // 
        yield return new WaitForSeconds(duration);

        // ���� �ӵ��� ����
        playerController.moveSpeed = originalSpeed;
        Debug.Log($"ȿ�� ����! �ӵ� ����: {playerController.moveSpeed}");
    }
}