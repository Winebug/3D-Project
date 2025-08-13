using UnityEngine;

public class Ladder : MonoBehaviour
{
    // �÷��̾ Ʈ���� ���� ������ ��
    private void OnTriggerEnter(Collider other)
    {
        // ���� ������Ʈ�� �÷��̾���
        if (other.CompareTag("Player"))
        {
            // �÷��̾��� PlayerController���� "��ٸ� Ÿ�� ��� ON" ��ȣ�� ����
            other.GetComponent<PlayerController>().SetOnLadder(true);
        }
    }

    // �÷��̾ Ʈ���� ������ ������ ��
    private void OnTriggerExit(Collider other)
    {
        // ���� ������Ʈ�� �÷��̾���
        if (other.CompareTag("Player"))
        {
            // �÷��̾��� PlayerController���� "��ٸ� Ÿ�� ��� OFF" ��ȣ�� ����
            other.GetComponent<PlayerController>().SetOnLadder(false);
        }
    }
}