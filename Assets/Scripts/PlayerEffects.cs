using System.Collections;
using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    private PlayerController playerController;

    [Header("테스트용 아이템")]
    public ItemData testItem;

    void Awake()
    {
        // PlayerController 스크립트를 미리 찾아옴
        playerController = GetComponent<PlayerController>();
    }

    // 아이템을 사용하는 함수 (외부에서 호출)
    public void UseItem(ItemData item)
    {
        Debug.Log($"{item.itemName} 아이템 사용!");

        // 아이템 종류에 따라 다른 코루틴을 실행
        if (item.itemType == ItemType.SpeedBoost)
        {
            // 이미 스피드 부스트가 적용 중이라면 기존 것을 멈추고 새로 시작
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

    // 스피드 부스트 효과를 처리하는 코루틴
    private IEnumerator SpeedBoostCoroutine(float duration, float speedMultiplier)
    {
        // 
        float originalSpeed = playerController.moveSpeed;

        // 
        playerController.moveSpeed *= speedMultiplier;
        Debug.Log($"속도 증가! 현재 속도: {playerController.moveSpeed}");

        // 
        yield return new WaitForSeconds(duration);

        // 원래 속도로 복구
        playerController.moveSpeed = originalSpeed;
        Debug.Log($"효과 종료! 속도 복구: {playerController.moveSpeed}");
    }
}