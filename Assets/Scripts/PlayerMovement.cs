using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Joystick Settings")]
    public float joystickRadius = 150f;  // en p�xeles
    public float deadZone = 10f;         // p�xeles m�nimos para moverse
    public float maxSpeed = 6f;          // velocidad m�xima del jugador
    public bool useSmoothing = false;
    public float smoothTime = 0.08f;

    private Rigidbody rb;
    private Vector2 touchStartPos;
    private bool isTouching = false;
    private Vector3 velocitySmoothRef = Vector3.zero;

    // Referencia al Input Action generado
    private PlayerInputAction controls;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;   // para top-down sin que gire
        rb.useGravity = false;

        controls = new PlayerInputAction();
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
        rb.linearVelocity = Vector3.zero;
        velocitySmoothRef = Vector3.zero;
    }

    void Update()
    {
        if (!isTouching) return;

        Vector2 pointerPos = controls.Player.TouchPosition.ReadValue<Vector2>();
        Vector2 delta = pointerPos - touchStartPos;

        if (delta.magnitude < deadZone)
        {
            rb.linearVelocity = Vector3.zero;
            return;
        }

        Vector2 dir = delta.normalized;
        dir.y = -dir.y; // invertir eje vertical si quieres que arriba sea positivo Z

        // Mapeo XY pantalla -> XZ mundo
        Vector3 targetVel = new Vector3(dir.x, 0f, -dir.y) * maxSpeed * Mathf.Clamp01(delta.magnitude / joystickRadius);

        if (useSmoothing)
        {
            rb.linearVelocity = Vector3.SmoothDamp(rb.linearVelocity, targetVel, ref velocitySmoothRef, smoothTime);
        }
        else
        {
            rb.linearVelocity = targetVel;
        }
    }
}
