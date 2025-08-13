using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    public Camera playerCamera;
    public float interactionDistance = 30f;
    public Text interactionText;

    // 플레이어의 아이템 효과 스크립트를 참조할 변수
    private PlayerEffects playerEffects;
    private PlayerController playerController;

    void Awake()
    {
        playerEffects = GetComponent<PlayerEffects>();
        // PlayerController 컴포넌트도 여기서 찾아옵니다.
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hitInfo;

        // 인식률을 위해 SphereCast를 사용하는 것이 좋습니다.
        if (Physics.SphereCast(ray, 0.5f, out hitInfo, interactionDistance))
        {
            Interactable interactable = hitInfo.collider.GetComponentInParent<Interactable>();

            if (interactable != null)
            {
                interactionText.text = interactable.promptMessage;

                if (Input.GetKeyDown(KeyCode.F))
                {
                    // 아이템 줍기 로직
                    if (interactable.itemData != null)
                    {
                        playerEffects.UseItem(interactable.itemData);
                        Destroy(interactable.gameObject);
                        interactionText.text = "";
                    }
                    // 사다리 타기 로직
                    else if (interactable.isLadder)
                    {
                        // PlayerController에게 "사다리 타기 모드 ON" 신호를 보냄
                        playerController.SetOnLadder(true);
                    }
                }
            }
            else
            {
                interactionText.text = "";
            }
        }
        else
        {
            interactionText.text = "";
        }
    }
}