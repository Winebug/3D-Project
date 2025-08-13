using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    public Camera playerCamera;
    public float interactionDistance = 30f;
    public Text interactionText;

    // �÷��̾��� ������ ȿ�� ��ũ��Ʈ�� ������ ����
    private PlayerEffects playerEffects;
    private PlayerController playerController;

    void Awake()
    {
        playerEffects = GetComponent<PlayerEffects>();
        // PlayerController ������Ʈ�� ���⼭ ã�ƿɴϴ�.
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hitInfo;

        // �νķ��� ���� SphereCast�� ����ϴ� ���� �����ϴ�.
        if (Physics.SphereCast(ray, 0.5f, out hitInfo, interactionDistance))
        {
            Interactable interactable = hitInfo.collider.GetComponentInParent<Interactable>();

            if (interactable != null)
            {
                interactionText.text = interactable.promptMessage;

                if (Input.GetKeyDown(KeyCode.F))
                {
                    // ������ �ݱ� ����
                    if (interactable.itemData != null)
                    {
                        playerEffects.UseItem(interactable.itemData);
                        Destroy(interactable.gameObject);
                        interactionText.text = "";
                    }
                    // ��ٸ� Ÿ�� ����
                    else if (interactable.isLadder)
                    {
                        // PlayerController���� "��ٸ� Ÿ�� ��� ON" ��ȣ�� ����
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