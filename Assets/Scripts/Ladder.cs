using UnityEngine;

public class Ladder : MonoBehaviour
{
    // 플레이어가 트리거 존에 들어왔을 때
    private void OnTriggerEnter(Collider other)
    {
        // 들어온 오브젝트가 플레이어라면
        if (other.CompareTag("Player"))
        {
            // 플레이어의 PlayerController에게 "사다리 타기 모드 ON" 신호를 보냄
            other.GetComponent<PlayerController>().SetOnLadder(true);
        }
    }

    // 플레이어가 트리거 존에서 나갔을 때
    private void OnTriggerExit(Collider other)
    {
        // 나간 오브젝트가 플레이어라면
        if (other.CompareTag("Player"))
        {
            // 플레이어의 PlayerController에게 "사다리 타기 모드 OFF" 신호를 보냄
            other.GetComponent<PlayerController>().SetOnLadder(false);
        }
    }
}