using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementMobile : MonoBehaviour
{
    [Header("Joystick Settings")]
    public float joystickRadius = 150f;
    public float deadZone = 10f;
    public float maxSpeed = 6f;
    public bool useSmoothing = false;
    public float smoothTime = 0.08f;
    public float padding = 0.5f;

    private Rigidbody rb;
    private PlayerInputAction controls;
    private Vector2 touchStartPos;
    private bool isTouching = false;
    private Vector3 desiredVelocity;      // ← NO movemos en Update
    private Vector3 smoothVelocityRef;

    private float minX, maxX, minZ, maxZ;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.freezeRotation = true;

        controls = new PlayerInputAction();

        // LÍMITES DE CÁMARA
        Camera cam = Camera.main;
        float distance = Mathf.Abs(transform.position.y - cam.transform.position.y);

        Vector3 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, distance));

        minX = bottomLeft.x;
        maxX = topRight.x;
        minZ = bottomLeft.z;
        maxZ = topRight.z;
    }

    void OnEnable()
    {
        controls.Player.TouchPress.started += OnTouchStarted;
        controls.Player.TouchPress.canceled += OnTouchCanceled;
        controls.Player.Enable();
    }

    void OnDisable()
    {
        controls.Player.TouchPress.started -= OnTouchStarted;
        controls.Player.TouchPress.canceled -= OnTouchCanceled;
        controls.Player.Disable();
        controls.Dispose();
    }

    void OnTouchStarted(InputAction.CallbackContext ctx)
    {
        touchStartPos = controls.Player.TouchPosition.ReadValue<Vector2>();
        isTouching = true;
    }

    void OnTouchCanceled(InputAction.CallbackContext ctx)
    {
        isTouching = false;
        desiredVelocity = Vector3.zero;
        rb.linearVelocity = Vector3.zero;
    }

    void Update()
    {
        if (!isTouching)
        {
            desiredVelocity = Vector3.zero;
            return;
        }

        Vector2 pointerPos = controls.Player.TouchPosition.ReadValue<Vector2>();
        Vector2 delta = pointerPos - touchStartPos;

        if (delta.magnitude < deadZone)
        {
            desiredVelocity = Vector3.zero;
            return;
        }

        Vector2 dir = delta.normalized;
        float strength = Mathf.Clamp01(delta.magnitude / joystickRadius);

        // DIRECCIÓN EN XZ
        Vector3 target = new Vector3(dir.x, 0f, dir.y) * maxSpeed * strength;

        if (useSmoothing)
            desiredVelocity = Vector3.SmoothDamp(desiredVelocity, target, ref smoothVelocityRef, smoothTime);
        else
            desiredVelocity = target;
    }

    void FixedUpdate()
    {
        // Mover el Rigidbody dentro de la física
        rb.linearVelocity = desiredVelocity;

        // Clamp dentro de la cámara
        Vector3 pos = rb.position;
        pos.x = Mathf.Clamp(pos.x, minX + padding, maxX - padding);
        pos.z = Mathf.Clamp(pos.z, minZ + padding, maxZ - padding);
        rb.position = pos;
    }
}
