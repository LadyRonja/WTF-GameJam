using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;


public enum CameraStates
{
    Inactive,
    TrackingSingle,
    TrackingMultiple,
    LookAhead,
    MoveToTarget,
    Shaking
}

public class CameraController : MonoBehaviour
{
    [Header("Generic")]
    public static CameraController Instance;
    [SerializeField] private Camera cam;
    public CameraStates state = CameraStates.TrackingSingle;
    [SerializeField] float debugShakeAmount = 2f;
    [SerializeField] float debugFreezeAmount = 0.1f;
    Vector3 positionToCenter;

    [Header("Single object Tracking")]
    public Transform objectToFollow;
    public bool ignoreX = false;
    public bool ignoreY = false;

    [Header("Look Ahead")]
    [SerializeField] PlayerMovement player; 
    public float maxLookAhead = 3f;
    public float smoothTime = 0.3f;
    Vector3 mousePosLastFrame = Vector3.zero;
    public float xOffSet = 0f;
    public float yOffSet = 0f;
    Vector3 velocity = Vector3.zero;

    [Header("Screen Shake")]
    [SerializeField] bool additiveShake = true;
    [SerializeField] bool prioritizeLargeShake = true;
    float shakeLeft = 0f;
    [SerializeField] float shakeStabalizer = 5f;
    Vector2 shakeDirection = Vector2.zero;
    CameraStates previousState = CameraStates.TrackingSingle;

    public float Height { get => GetHeight(); }
    public float Width { get => GetWidth(); }

    private void Awake()
    {
        #region Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        #endregion

        if(cam == null)
            cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            GameSettings.usingGamepad = !GameSettings.usingGamepad;
        }
    }

    private void FixedUpdate()
    {
        if (Time.timeScale == 0) return;

        CameraBehaivour(state);
    }

    private void LateUpdate()
    {
        mousePosLastFrame = Input.mousePosition;
    }

    private void CameraBehaivour(CameraStates operationState)
    {
        switch (operationState)
        {
            case CameraStates.Inactive:
                // Do nothing
                break;
            case CameraStates.TrackingSingle:
                TrackSingleTarget();
                break;
            case CameraStates.TrackingMultiple:
                Debug.Log("Tracking Multiple not implemented yet, setting state to track single");
                state = CameraStates.TrackingSingle;
                break;
            case CameraStates.MoveToTarget:
                Debug.Log("MoveToTarget not implemented yet, setting state to track single");
                state = CameraStates.TrackingSingle;
                break;
            case CameraStates.Shaking:
                Shaking();
                break;
            case CameraStates.LookAhead:
                LookAhead();
                break;
            default:
                Debug.LogError("Reached end of switch-state-machine, states not covered?");
                Debug.Log("Switching to Inactive cameaState");
                state = CameraStates.Inactive;
                break;
        }
    }

    private void TrackSingleTarget()
    {
        if(!ignoreX) positionToCenter.x = objectToFollow.position.x;
        if(!ignoreY) positionToCenter.y = objectToFollow.position.y;


        positionToCenter = new Vector3(positionToCenter.x, positionToCenter.y, cam.transform.position.z);

        SetCameraPosition();
    }

    private void LookAhead()
    {
        Vector2 dir = Vector2.zero;

        if (GameSettings.usingGamepad)
        {
            dir.x = Input.GetAxis("HorAimController");
            dir.y = Input.GetAxis("VerAimController");

            if (dir == Vector2.zero)
            {
                if (player.rb != null)
                    dir.x = player.rb.velocity.x / (player.groundSpeedMax / 2f);
            }

        }
        else 
        {
            dir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.transform.position);
            dir.Normalize();
        }

        positionToCenter.x = player.transform.position.x;
        positionToCenter.y = player.transform.position.y;

        positionToCenter = new Vector3(
            positionToCenter.x + dir.x * maxLookAhead + xOffSet,
            positionToCenter.y + dir.y * maxLookAhead + yOffSet,
            cam.transform.position.z);


            cam.transform.position = Vector3.SmoothDamp(cam.transform.position, positionToCenter, ref velocity, smoothTime);
    }

    private void SetCameraPosition()
    {
        cam.transform.position = positionToCenter;
    }

    private void Shaking()
    {
        // Long shakes should not keep camera in place
        CameraBehaivour(previousState);

        // Get a random direction and amount of shake
        Vector2 direction = Random.insideUnitCircle.normalized;
        float amount = Random.Range(-shakeLeft, shakeLeft);

        // If shake should not be random, fix direction and don't let the amount go behind the direction
        if (shakeDirection != Vector2.zero)
        {
            direction = shakeDirection.normalized;
            amount = Mathf.Abs(amount);
        }

        // Update position
        cam.transform.position += (Vector3)direction * amount;
       
        // Reduce amount of shake left
        shakeLeft -= shakeStabalizer * Time.deltaTime;

        // If done shaking, return to the previous camera behaivor
        if (shakeLeft <= 0f)
        {
            state = previousState;
            shakeDirection = Vector2.zero;
        }
    }

    public void ShakeScreen(float amount)
    {
        if (additiveShake) shakeLeft += amount;
        else if (prioritizeLargeShake) shakeLeft = Mathf.Max(shakeLeft, amount);
        else shakeLeft = amount;

        if (state != CameraStates.Shaking) previousState = state;
        state = CameraStates.Shaking;
    }

    public void ShakeScreen(float amount, Vector2 direction)
    {
        shakeDirection = direction;
        ShakeScreen(amount);
    }

    public void FreezeGame(float duration)
    {
        StartCoroutine(Freeze(duration));
    }

    private IEnumerator Freeze(float duration)
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1f;
    }

    private float GetHeight()
    {
        return Camera.main.orthographicSize * 2;
    }

    private float GetWidth()
    {
        return GetHeight() * Camera.main.aspect;
    }
}