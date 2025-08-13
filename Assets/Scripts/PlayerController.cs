using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpPower = 10f;
    public float ladderClimbSpeed = 10f;

    [Header("Look Settings")]
    public Transform cameraContainer;
    public float minXLook = -60f;
    public float maxXLook = 60f;
    public float lookSensitivity = 1f;

    [Header("Ground Check")]
    public LayerMask groundLayerMask;

    // --- ���� ������ ---
    private Rigidbody _rigidbody;
    private Vector2 curMovementInput;
    private Vector2 mouseDelta;
    private float camCurXRot;

    // --- ���� ���� ������ ---
    private bool onLadder = false;
    private Vector3? launchCommand = null;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // ���� ������Ʈ�� FixedUpdate���� ���� ����
    void FixedUpdate()
    {
        // 1����: ������ ����� �ִ°�?
        if (launchCommand.HasValue)
        {
            _rigidbody.velocity = launchCommand.Value;
            launchCommand = null; // ��� ���� �� �ʱ�ȭ
            return; // �ٸ� ������ ���� ����
        }

        // 2����: ��ٸ��� Ÿ�� �ִ°�?
        if (onLadder)
        {
            ClimbLadder();
        }
        // 3����: �Ϲ� ����
        else
        {
            Move();
        }
    }

    // ī�޶� �������� LateUpdate���� ó��
    private void LateUpdate()
    {
        CameraLook();
    }

    // �Ϲ� ������ ���� ������
    void Move()
    {
        _rigidbody.useGravity = true; // �Ϲ� ���¿����� �߷� �׻� �ѱ�
        Vector3 dir = (transform.forward * curMovementInput.y) + (transform.right * curMovementInput.x);
        dir *= moveSpeed;
        dir.y = _rigidbody.velocity.y; // y�� �ӵ��� ���� �ӵ� ���� (�߷�, ���� ��)
        _rigidbody.velocity = dir;
    }

    // ��ٸ� ������ ���� ������
    void ClimbLadder()
    {
        _rigidbody.useGravity = false; // ��ٸ������� �߷� ��Ȱ��ȭ
        // W/S Ű �Է����� ���Ʒ� �ӵ� ����
        Vector3 dir = new Vector3(0, curMovementInput.y * ladderClimbSpeed, 0);
        _rigidbody.velocity = dir;
    }

    // ī�޶� ���� ����
    void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);
        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    // �ܺο��� ��ٸ� ���¸� ��������� �Լ�
    public void SetOnLadder(bool status)
    {
        onLadder = status;
    }

    // �ܺο��� ������ ����� ���� �Լ�
    public void LaunchPlayer(float launchVelocity)
    {
        launchCommand = new Vector3(_rigidbody.velocity.x, launchVelocity, _rigidbody.velocity.z);
    }

    // --- �Է� ó�� �Լ��� (Input System) ---

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        // ���� Ű�� ������ ��
        if (context.phase == InputActionPhase.Started)
        {
            // ���� ��ٸ��� Ÿ�� �ִٸ�, �ٸ� ���� ���� ��� ��ٸ� ��常 ����
            if (onLadder)
            {
                SetOnLadder(false);
            }
            // ��ٸ��� Ÿ�� ���� �ʰ�, ���� �ִٸ�, �Ϲ� ����
            else if (IsGrounded())
            {
                _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
            }
        }
    }

    bool IsGrounded()
    {
        // ... (���� �� üũ ������ ����)
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f), Vector3.down)
        };
        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }
        return false;
    }
}