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

    // --- 내부 변수들 ---
    private Rigidbody _rigidbody;
    private Vector2 curMovementInput;
    private Vector2 mouseDelta;
    private float camCurXRot;

    // --- 상태 관리 변수들 ---
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

    // 물리 업데이트는 FixedUpdate에서 통합 관리
    void FixedUpdate()
    {
        // 1순위: 점프대 명령이 있는가?
        if (launchCommand.HasValue)
        {
            _rigidbody.velocity = launchCommand.Value;
            launchCommand = null; // 명령 수행 후 초기화
            return; // 다른 움직임 실행 방지
        }

        // 2순위: 사다리를 타고 있는가?
        if (onLadder)
        {
            ClimbLadder();
        }
        // 3순위: 일반 상태
        else
        {
            Move();
        }
    }

    // 카메라 움직임은 LateUpdate에서 처리
    private void LateUpdate()
    {
        CameraLook();
    }

    // 일반 상태일 때의 움직임
    void Move()
    {
        _rigidbody.useGravity = true; // 일반 상태에서는 중력 항상 켜기
        Vector3 dir = (transform.forward * curMovementInput.y) + (transform.right * curMovementInput.x);
        dir *= moveSpeed;
        dir.y = _rigidbody.velocity.y; // y축 속도는 현재 속도 유지 (중력, 점프 등)
        _rigidbody.velocity = dir;
    }

    // 사다리 상태일 때의 움직임
    void ClimbLadder()
    {
        _rigidbody.useGravity = false; // 사다리에서는 중력 비활성화
        // W/S 키 입력으로 위아래 속도 결정
        Vector3 dir = new Vector3(0, curMovementInput.y * ladderClimbSpeed, 0);
        _rigidbody.velocity = dir;
    }

    // 카메라 시점 조절
    void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);
        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    // 외부에서 사다리 상태를 변경시켜줄 함수
    public void SetOnLadder(bool status)
    {
        onLadder = status;
    }

    // 외부에서 점프대 명령을 내릴 함수
    public void LaunchPlayer(float launchVelocity)
    {
        launchCommand = new Vector3(_rigidbody.velocity.x, launchVelocity, _rigidbody.velocity.z);
    }

    // --- 입력 처리 함수들 (Input System) ---

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
        // 점프 키를 눌렀을 때
        if (context.phase == InputActionPhase.Started)
        {
            // 만약 사다리를 타고 있다면, 다른 점프 로직 대신 사다리 모드만 해제
            if (onLadder)
            {
                SetOnLadder(false);
            }
            // 사다리를 타고 있지 않고, 땅에 있다면, 일반 점프
            else if (IsGrounded())
            {
                _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
            }
        }
    }

    bool IsGrounded()
    {
        // ... (기존 땅 체크 로직과 동일)
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