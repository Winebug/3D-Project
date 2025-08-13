using UnityEngine;

public class Ladder : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 플레이어의 PlayerController에게 "사다리 타기 모드 ON" 신호를 보냄
            other.GetComponent<PlayerController>().SetOnLadder(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 플레이어의 PlayerController에게 "사다리 타기 모드 OFF" 신호를 보냄
            other.GetComponent<PlayerController>().SetOnLadder(false);
        }
    }
}