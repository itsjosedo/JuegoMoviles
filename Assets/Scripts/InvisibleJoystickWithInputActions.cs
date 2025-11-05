using UnityEngine;
using UnityEngine.InputSystem; // Nuevo Input System

[RequireComponent(typeof(Rigidbody))]
public class InvisibleJoystickWithInputActions : MonoBehaviour
{
    [Header("Joystick (en p xeles)")]
    [Tooltip("Arrastre en px que equivale a velocidad m xima")]
    public float joystickRadius = 150f;
    [Tooltip("Ignorar arrastres muy peque os")]
    public float deadZone = 10f;

    [Header("Movimiento")]
    public float maxSpeed = 6f; // unidades por segundo

    // Para suavizado opcional
    [Header("Opcional - Suavizado")]
    public bool useSmoothing = false;
    public float smoothTime = 0.08f; // menor = m s reactivo

    // Estado interno
    Vector2 touchStartPos;         // origen del "joystick virtual" (screen coords)
    bool isTouching = false;
    Rigidbody rb;

    // Para suavizado
    Vector2 velocitySmoothRef = Vector2.zero;

    // Clase generada a partir del InputActions (aseg rate de usar el mismo nombre que generaste)
    PlayerInputAction controls;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        //rb.gravityScale = 0f;
        rb.freezeRotation = true;

        // Instancia la clase generada por el Input System
        controls = new PlayerInputAction();
    }

    void OnEnable()
    {
        // Suscribimos callbacks para detectar inicio y fin del toque
        controls.Player.TouchPress.started += OnTouchStarted;
        controls.Player.TouchPress.canceled += OnTouchCanceled;

        // Activamos el mapa de acciones
        controls.Player.Enable();
    }

    void OnDisable()
    {
        // Desuscribimos para evitar memory leaks
        controls.Player.TouchPress.started -= OnTouchStarted;
        controls.Player.TouchPress.canceled -= OnTouchCanceled;

        controls.Player.Disable();
        controls.Dispose();
    }

    void OnTouchStarted(InputAction.CallbackContext ctx)
    {
        // Guardamos la posici n inicial del dedo (en coordenadas de pantalla)
        touchStartPos = controls.Player.TouchPosition.ReadValue<Vector2>();
        isTouching = true;
    }

    void OnTouchCanceled(InputAction.CallbackContext ctx)
    {
        // Se levant  el dedo: detenemos
        isTouching = false;
        rb.linearVelocity = Vector2.zero;
        velocitySmoothRef = Vector2.zero;
    }

    void Update()
    {
        if (!isTouching) return;

        // Leemos la posici n actual del dedo (screen coordinates)
        Vector2 pointerPos = controls.Player.TouchPosition.ReadValue<Vector2>();

        // Delta (en p xeles) respecto al punto donde empez  el "joystick"
        Vector2 delta = pointerPos - touchStartPos;

        // Si est  dentro de deadZone -> no mueve
        if (delta.magnitude < deadZone)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        // Direcci n normalizada (0..1) y fuerza (0..1) seg n joystickRadius
        Vector2 dir = delta.normalized;
        dir.y = -dir.y;
        float strength = Mathf.Clamp01(delta.magnitude / joystickRadius);

        // Velocidad objetivo en unidades del mundo por segundo
        Vector2 targetVel = dir * maxSpeed * strength;

        if (useSmoothing)
        {
            // SmoothDamp para evitar cambios bruscos
            Vector2 currentVel = rb.linearVelocity;

        }
        else
        {
            rb.linearVelocity = targetVel;
        }
    }

    void LateUpdate()
    {
        // Mantener el jugador dentro del viewport de la c mara principal
        Camera cam = Camera.main;
        if (cam == null) return;

        Vector3 vp = cam.WorldToViewportPoint(transform.position);
        vp.x = Mathf.Clamp01(vp.x);
        vp.y = Mathf.Clamp01(vp.y);
        Vector3 clamped = cam.ViewportToWorldPoint(vp);
        clamped.z = transform.position.z;
        transform.position = clamped;
    }

    // Debug visual: muestra desde donde empez  el joystick hasta la posici n actual en la escena (Editor)
    void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        if (!isTouching) return;

        Vector3 startWorld = Camera.main.ScreenToWorldPoint(new Vector3(touchStartPos.x, touchStartPos.y, Camera.main.nearClipPlane + 1f));
        Vector2 pointerPos = controls != null ? controls.Player.TouchPosition.ReadValue<Vector2>() : Vector2.zero;
        Vector3 currentWorld = Camera.main.ScreenToWorldPoint(new Vector3(pointerPos.x, pointerPos.y, Camera.main.nearClipPlane + 1f));
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(startWorld, currentWorld);
        Gizmos.DrawWireSphere(startWorld, 0.15f);
    }
}

