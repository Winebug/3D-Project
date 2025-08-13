using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float jumpVelocity = 30f;

    private void OnCollisionEnter(Collision collision)
    {
        // �浹�� ������Ʈ�� �÷��̾����� Ȯ��
        if (collision.gameObject.CompareTag("Player"))
        {
            // �÷��̾��� PlayerController ������Ʈ�� ������

            PlayerController controller = collision.gameObject.GetComponent<PlayerController>();

            if (controller != null)
            {
                controller.LaunchPlayer(jumpVelocity);
            }
        }
    }
}