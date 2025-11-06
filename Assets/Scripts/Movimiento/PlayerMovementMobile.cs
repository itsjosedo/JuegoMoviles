using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementMobile : MonoBehaviour
{
    [Header("Joystick Settings")]
    public float joystickRadius = 150f;  // en p�xeles
    public float deadZone = 10f;         // p�xeles m�nimos para moverse
    public float maxSpeed = 6f;          // velocidad m�xima del jugador
    public bool useSmoothing = false;
    public float smoothTime = 0.08f;

    [Header("Camera Bounds")]
    public float padding = 0.5f;

    private Rigidbody rb;
    private Vector2 touchStartPos;
    private bool isTouching = false;
    private Vector3 velocitySmoothRef = Vector3.zero;

    private PlayerInputAction controls;
    private float minX, maxX, minZ, maxZ;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.freezeRotation = true;

        controls = new PlayerInputAction();

        // Calcular los l�mites de la c�mara
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
        rb.linearVelocity = Vector3.zero;
        velocitySmoothRef = Vector3.zero;
    }

    void Update()
    {
        if (!isTouching) return;

        Vector2 pointerPos = controls.Player.TouchPosition.ReadValue<Vector2>();
        Vector2 delta = pointerPos - touchStartPos;

        // Ignorar movimiento peque�o
        if (delta.magnitude < deadZone)
        {
            rb.linearVelocity = Vector3.zero;
            return;
        }

        // Direccion y fuerza
        Vector2 dir = delta.normalized;
        float strength = Mathf.Clamp01(delta.magnitude / joystickRadius);

        // Mapeo XY (pantalla) -> XZ (mundo)
        Vector3 targetVel = new Vector3(dir.x, 0f, dir.y) * maxSpeed * strength;

        // Suavizado opcional
        if (useSmoothing)
            rb.linearVelocity = Vector3.SmoothDamp(rb.linearVelocity, targetVel, ref velocitySmoothRef, smoothTime);
        else
            rb.linearVelocity = targetVel;

        // Limitar dentro de la c�mara
        Vector3 pos = rb.position;
        pos.x = Mathf.Clamp(pos.x, minX + padding, maxX - padding);
        pos.z = Mathf.Clamp(pos.z, minZ + padding, maxZ - padding);
        rb.position = pos;
    }
}
