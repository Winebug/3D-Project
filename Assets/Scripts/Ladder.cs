using UnityEngine;

public class Ladder : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // �÷��̾��� PlayerController���� "��ٸ� Ÿ�� ��� ON" ��ȣ�� ����
            other.GetComponent<PlayerController>().SetOnLadder(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // �÷��̾��� PlayerController���� "��ٸ� Ÿ�� ��� OFF" ��ȣ�� ����
            other.GetComponent<PlayerController>().SetOnLadder(false);
        }
    }
}