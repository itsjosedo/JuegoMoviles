using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementMobile : MonoBehaviour
{
    public float joystickRadius = 150f;
    public float deadZone = 10f;
    public float maxSpeed = 7f;
    public float padding = 0.5f;

    private PlayerInputAction controls;
    private Rigidbody rb;

    private bool isTouching = false;
    private Vector2 startTouch;
    private Vector2 prevTouch;

    private Vector3 targetVelocity;

    private float minX, maxX, minZ, maxZ;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.freezeRotation = true;
        rb.interpolation = RigidbodyInterpolation.Interpolate;

        controls = new PlayerInputAction();

        Camera cam = Camera.main;
        float dist = Mathf.Abs(transform.position.y - cam.transform.position.y);

        Vector3 bl = cam.ViewportToWorldPoint(new Vector3(0, 0, dist));
        Vector3 tr = cam.ViewportToWorldPoint(new Vector3(1, 1, dist));

        minX = bl.x; maxX = tr.x;
        minZ = bl.z; maxZ = tr.z;
    }

    void OnEnable()
    {
        controls.Player.TouchPress.started += ctx => {
            startTouch = controls.Player.TouchPosition.ReadValue<Vector2>();
            prevTouch = startTouch;
            isTouching = true;
        };

        controls.Player.TouchPress.canceled += ctx => {
            isTouching = false;
            targetVelocity = Vector3.zero;
            rb.linearVelocity = Vector3.zero;
        };

        controls.Player.Enable();
    }

    void OnDisable()
    {
        controls.Player.Disable();
    }

    void Update()
    {
        if (!isTouching)
        {
            targetVelocity = Vector3.zero;
            return;
        }

        Vector2 rawTouch = controls.Player.TouchPosition.ReadValue<Vector2>();
        rawTouch = Vector2.Lerp(prevTouch, rawTouch, 0.35f); 
        prevTouch = rawTouch;

        Vector2 delta = rawTouch - startTouch;

        if (delta.magnitude < deadZone)
        {
            targetVelocity = Vector3.zero;
            return;
        }

        Vector2 dir = delta.normalized;
        float strength = Mathf.Clamp01(delta.magnitude / joystickRadius);

        targetVelocity = new Vector3(dir.x, 0f, dir.y) * maxSpeed * strength;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = targetVelocity;

        Vector3 pos = rb.position;
        pos.x = Mathf.Clamp(pos.x, minX + padding, maxX - padding);
        pos.z = Mathf.Clamp(pos.z, minZ + padding, maxZ - padding);

        rb.MovePosition(pos);
    }
}

