using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float jumpVelocity = 30f;

    private void OnCollisionEnter(Collision collision)
    {
        // 충돌한 오브젝트가 플레이어인지 확인
        if (collision.gameObject.CompareTag("Player"))
        {
            // 플레이어의 PlayerController 컴포넌트를 가져옴

            PlayerController controller = collision.gameObject.GetComponent<PlayerController>();

            if (controller != null)
            {
                controller.LaunchPlayer(jumpVelocity);
            }
        }
    }
}