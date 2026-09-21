using BoolonxMenu.Mods;
using GorillaLocomotion;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using BoolonxMenu;
using BoolonxMenu.Classes.Better_II_temp;
public class PhysicsHand : MonoBehaviourPunCallbacks
{
    [Header("PID")]
    public float frequency = 50f;
    public float damping = 1f;
    public float rotfrequency = 100f;
    public float rotDamping = 0.9f;
    public Rigidbody playerRigidbody;
    public Transform target;
    [Space]
    [Header("Springs")]
    public float climbForce = 400f;
    public float climbDrag = 100f;
    public bool leftController = false;

    Vector3 _previousPosition;
    Rigidbody _rigidbody;
    bool _isColliding = false;
    public bool stopMovement;
    void Start()
    {
        transform.position = target.position;
        transform.rotation = target.rotation;
        _rigidbody = gameObject.AddComponent<Rigidbody>();
        _rigidbody.excludeLayers = ~Plugin.defaultPlayerLayer;
        _rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        _rigidbody.angularDamping = 0f;
        _rigidbody.maxAngularVelocity = 50f;
        _previousPosition = transform.position;
        gameObject.layer = LayerMask.NameToLayer("GorillaThrowable");
    }

    void FixedUpdate()
    {
        bool readyToClimb = PhysicsGorilla.PhysicsGrabEverywhere && (leftController ? SimpleInputs.LeftGrab : SimpleInputs.RightGrab) && _isColliding;
        PIDMovement();
        PIDRotation();
        if (readyToClimb)
        {
            _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }
        else
        {
            _rigidbody.constraints = RigidbodyConstraints.None;
        }
        if ((_isColliding && !stopMovement) || readyToClimb) HookesLaw();

        float distance = Vector3.Distance(transform.position, target.position);
        Rigidbody rb = _rigidbody;
        if (distance > 2.0f)
        {
            rb.position = target.position;
            rb.rotation = target.rotation;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            return;
        }

    }

    void Update()
    {
        GetComponent<SphereCollider>().radius = GTPlayer.Instance.minimumRaycastDistance * GorillaTagger.Instance.offlineVRRig.scaleFactor;
        if (playerRigidbody.linearVelocity.magnitude > 25)
        {
            playerRigidbody.linearVelocity = Vector3.zero;
        }
    }

    public void PIDMovement()
    {
        float kp = (6f * frequency) * (6f * frequency) * 0.25f;
        float kd = 4.5f * frequency * damping;
        float g = 1 / (1 + kd * Time.fixedDeltaTime + kp * Time.fixedDeltaTime * Time.fixedDeltaTime);
        float ksg = kp * g;
        float kdg = (kd + kp * Time.fixedDeltaTime) * g;
        Vector3 force = (target.position - transform.position) * ksg + (playerRigidbody.linearVelocity - _rigidbody.linearVelocity) * kdg;
        _rigidbody.AddForce(force, ForceMode.Acceleration);
    }

    void PIDRotation()
    {
        float kp = (6f * rotfrequency) * (6f * rotfrequency) * 0.25f;
        float kd = 4.5f * rotfrequency * rotDamping;
        float g = 1 / (1 + kd * Time.fixedDeltaTime + kp * Time.fixedDeltaTime * Time.fixedDeltaTime);
        float ksg = kp * g;
        float kdg = (kd + kp * Time.fixedDeltaTime) * g;
        Quaternion q = target.rotation * Quaternion.Inverse(transform.rotation);
        if (q.w < 0)
        {
            q.x = -q.x;
            q.y = -q.y;
            q.z = -q.z;
            q.w = -q.w;
        }
        q.ToAngleAxis(out float angle, out Vector3 axis);
        axis.Normalize();
        axis *= Mathf.Deg2Rad;
        Vector3 torque = ksg * axis * angle + -_rigidbody.angularVelocity * kdg;
        _rigidbody.AddTorque(torque, ForceMode.Acceleration);
    }

    public void HookesLaw()
    {
        Vector3 displacementFromResting = transform.position - target.position;
        Vector3 force = displacementFromResting * climbForce;
        float drag = GetDrag();

        playerRigidbody.AddForce(force, ForceMode.Acceleration);
        playerRigidbody.AddForce(drag * -playerRigidbody.linearVelocity * climbDrag, ForceMode.Acceleration);
    }

    float GetDrag()
    {
        Vector3 handVelocity = (target.localPosition - _previousPosition) / Time.fixedDeltaTime;
        float drag = 1 / handVelocity.magnitude + 0.01f;
        drag = drag > 1 ? 1 : drag;
        drag = drag < 0.03f ? 0.03f : drag;
        _previousPosition = transform.position;
        return drag;
    }

    void OnCollisionEnter(Collision collision)
    {
        _isColliding = true;

        if (Time.time < lastTapTime + GorillaTagger.Instance.tapCoolDown) return;

        float impactSpeed = collision.relativeVelocity.magnitude;

        float volume = GorillaTagger.Instance.handTapVolume;
        int audioIndex = 0;
        if (collision.gameObject.TryGetComponent<GorillaSurfaceOverride>(out var surface))
        {
            audioIndex = surface.overrideIndex;
        }

        if (GorillaTagger.Instance != null && GorillaTagger.Instance.offlineVRRig != null)
        {
            GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(audioIndex, leftController, volume / 10);
        }


        if (GorillaTagger.Instance != null)
        {
            GorillaTagger.Instance.StartVibration(leftController, GorillaTagger.Instance.tapHapticStrength, GorillaTagger.Instance.tapHapticDuration);
        }

        lastTapTime = Time.time;
    }

    void OnCollisionExit(Collision other)
    {
        _isColliding = false;
    }

    public override void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    public override void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        fix();
    }
    private void OnSceneUnloaded(Scene unloadedScene)
    {
        fix();
    }

    void fix()
    {
        _rigidbody.linearVelocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        playerRigidbody.linearVelocity = Vector3.zero;
        playerRigidbody.angularVelocity = Vector3.zero;
        StartCoroutine(newMapWait());
    }

    IEnumerator newMapWait()
    {
        GetComponent<Collider>().isTrigger = true;
        GTPlayer.Instance.playerRigidBody.isKinematic = true;
        yield return new WaitForSeconds(2f);
        GTPlayer.Instance.playerRigidBody.isKinematic = false;
        GetComponent<Collider>().isTrigger = false;
    }
    private float lastTapTime;
}