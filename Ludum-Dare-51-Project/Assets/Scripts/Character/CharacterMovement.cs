using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _rotationSpeed = 5;

    private float startHight;

    private bool canDash = true;
    private bool isDashing;
    [SerializeField] private float _dashingPower = 30f;
    [SerializeField] private float dashingTime = 0.1f;

    private TrailRenderer trailRenderer;
    private float dashingCooldown = 1f;

    private Vector2 previousInput;

    private Rigidbody rb;

    private Controls controls;

    public float GetDashCooldown()
    {
        return dashingCooldown;
    }

    private void Start()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        startHight = transform.position.y + 0.2f;
        Debug.Log(startHight);

        rb = GetComponent<Rigidbody>();

        controls = new Controls();

        controls.Player.Rotation.performed += SetRotation;

        controls.Player.Movement.performed += SetPreviousInput;
        controls.Player.Movement.canceled += SetPreviousInput;

        controls.Player.Dash.performed += HandleDash;

        controls.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
    }

    private void SetRotation(InputAction.CallbackContext ctx)
    {
        Vector2 mousePosition = ctx.ReadValue<Vector2>();
        Vector2 objectPosition = (Vector2)Camera.main.WorldToScreenPoint(transform.position);
        Vector2 direction = (mousePosition - objectPosition).normalized;
        float angle = -Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg -90f;
        transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
    }

    private void UpdateMovement()
    {
        if (isDashing)
        {
            return;
        }

        Vector3 val = new Vector3(0f,0f,0f);

        if (previousInput != Vector2.zero)
        {
            val += new Vector3(previousInput.x, 0f, previousInput.y) * _movementSpeed * Time.deltaTime;
        }

        rb.velocity = val;
        transform.position = new Vector3(transform.position.x, startHight,transform.position.z);
    }

    private void SetPreviousInput(InputAction.CallbackContext ctx)
    {
        previousInput = ctx.ReadValue<Vector2>();
    }

    private void HandleDash(InputAction.CallbackContext ctx)
    {
        Debug.Log("Dash");
        if (canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        trailRenderer.enabled = true;
        canDash = false;
        isDashing = true;
        rb.velocity = new Vector3(previousInput.x, 0f, previousInput.y) * _dashingPower;
        yield return new WaitForSeconds(dashingTime);
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
        trailRenderer.enabled = false;
    }
    private void OnDestroy()
    {
        controls.Player.Rotation.performed -= SetRotation;

        controls.Player.Movement.performed -= SetPreviousInput;
        controls.Player.Movement.canceled -= SetPreviousInput;

        controls.Player.Dash.performed -= HandleDash;
    }
}
