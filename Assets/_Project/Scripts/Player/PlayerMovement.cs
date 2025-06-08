using System;
using System.Collections;
using System.Runtime.Remoting.Messaging;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

public class PlayerMovement : TickBehaviour
{
    [Header("Move Player")]
    [SerializeField] PlayerAnimBehavior playerAnimBehavior;
    [SerializeField] Transform main;
    [SerializeField] float speed = 5f;
    FixedJoystick fixedJoystick;
    CharacterController characterController;
    Transform mainCam => Camera.main.transform;
    Vector3 velocity;
    float gravity = -20f;
    [SerializeField] float drag = 1f;
    float turnSmoothTime = 0.1f;
    float turnSmoothAngle;
    float horizontal;
    float vertical;
    float eulerAngleYCamPlayer;

    [Header("Jump")]
    [SerializeField] Transform checkHead;
    [SerializeField] Transform checkGround;
    public Transform posShovel;
    public Transform posGun;
    [SerializeField] float jumpHeight;
    bool isGround;
    bool isEnemyZone;
    [Header("Landing")]
    [SerializeField] GameObject effectSmokePrefab;
    [SerializeField] float timeDisplaySmoke = 0.7f;
    float timeEffectSmoke;

    [Header("Ladder")]
    [SerializeField] float distanceLadder = 0.6f;
    bool isOnLadder;
    bool canMoveLadder;

    [Header("Jetpack")]
    [SerializeField] GameObject jetpack;
    bool isJetpack;

    [Header("Attack")]
    bool isAttack;

    bool haveBoost = false;
    float originSpeed;
    private bool haveWind;
    public float speedWind = 0.5f;

    public bool isPlayerDie = false;

    [SerializeField] PlayerAudio playerAudio;
    bool isReady;
    bool _isDriving;

    protected override void Awake()
    {
        base.Awake();
        characterController = GetComponent<CharacterController>();
        fixedJoystick = UIManager.Panel<PanelGamePlay>().FixedJoystick;
    }
    private void Start()
    {
        isReady = true;
        StopAttack();
        originSpeed = speed;
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        //if (!AudioBGSource.isPlaying) AudioBGSource.Play();
        if (isPlayerDie || _isDriving) {
            return;
        }
       
        if (GameController.Instance.GameState == GameState.Pause)
        {
            if(isJetpack) velocity.y = 0;
            if(playerAnimBehavior.PlayerState == PlayerState.Run)
            {
                playerAnimBehavior.SetPlayerState(PlayerState.Idle);
            }
            return;
        }
        if(!isJetpack)
        {
            CheckForLadder();
            isSlideDown = false;
           // if(!isOnLadder) CheckSlope();
        }
        bool inputKey = false;
#if UNITY_EDITOR
        inputKey = Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;
#endif
        horizontal = isSlideDown ? 0 : (inputKey ? Input.GetAxis("Horizontal") : fixedJoystick.Horizontal);
        vertical = isSlideDown ? 0 : (inputKey ? Input.GetAxis("Vertical") : fixedJoystick.Vertical);
        Vector3 dirInput = new Vector3(horizontal, 0f, vertical);
        float inputMagnitude = dirInput.magnitude;
        Vector3 forwardCam = mainCam.forward;
        Vector3 rightCam = mainCam.right;
        dirInput.Normalize();
        eulerAngleYCamPlayer = mainCam.eulerAngles.y;

        forwardCam.y = 0f;
        rightCam.y = 0f;

        Vector3 moveDirection = forwardCam * dirInput.z + rightCam * dirInput.x;
        moveDirection.Normalize();

        float targetAngle = Mathf.Atan2(dirInput.x, dirInput.z) * Mathf.Rad2Deg + eulerAngleYCamPlayer;
        float smoothAngle = Mathf.SmoothDampAngle(main.eulerAngles.y, targetAngle, ref turnSmoothAngle, turnSmoothTime);

        if((!isOnLadder || isJetpack) && !isAttack)
        {
            if(inputMagnitude != 0) main.rotation = Quaternion.Euler(isJetpack ? main.eulerAngles.x : 0f, smoothAngle, 0f);
        }
        forwardCam.Normalize();
        Vector3 move = (rightCam * dirInput.x + forwardCam * dirInput.y).normalized;
        move.y = 0.1f;
        float adjustedSpeed = speed * inputMagnitude;
        if(!isAttack && !isSlideDown)
        {
            if(!isOnLadder)
            {
                characterController.Move(velocWindy + moveDirection * adjustedSpeed * Time.deltaTime);
            }
            else
            {
                HandleLadderMovement();
            }
        }

        if(isOnLadder || isAttack)
        {
            if(isAttack && !isJetpack)
            {
                characterController.Move(velocity * Time.deltaTime);
            }
            return;
        }
        if(!isJetpack)
        {
            
            CheckJump(inputMagnitude);
        }
        else
        {
        }
        characterController.Move(velocity * Time.deltaTime);
    }
    #region Slide Down
    [SerializeField] float slideSpeed;
    void CheckSlope()
    {
        Vector3 rayOrigin = transform.position + Vector3.up * 0.1f;
        float rayLength = 1f;
        int layerMask = LayerMask.GetMask(Preconsts.Layer_Ground, Preconsts.Layer_Wall);
        Vector3[] directions = {
            Vector3.forward,
            Vector3.back,
            Vector3.left,
            Vector3.right,
            Vector3.forward + Vector3.left,
            Vector3.forward + Vector3.right,
            Vector3.back + Vector3.left,
            Vector3.back + Vector3.right
        };
        foreach(Vector3 direction in directions)
        {
            Vector3 rayDirection = direction + Vector3.down * 0.5f;
            if(Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hit1, rayLength, layerMask))
            {
                Debug.DrawRay(rayOrigin, rayDirection * rayLength, Color.blue);
                Vector3 normal = hit1.normal;
                float slopeAngle = Vector3.Angle(Vector3.up, normal);
                if(slopeAngle > characterController.slopeLimit)
                {
                    if(velocity.y < 0 && (vertical == 0 && horizontal == 0))
                    {
                        isSlideDown = true;
                        SlideDown(normal);
                    }

                }
            }
            else
            {
                Debug.DrawRay(rayOrigin, rayDirection * rayLength, Color.green);
            }
        }
    }
    bool isSlideDown;
    void SlideDown(Vector3 surfaceNormal)
    {
        Vector3 slideDirection = new Vector3(surfaceNormal.x, -surfaceNormal.y, surfaceNormal.z);
        characterController.Move(slideDirection * slideSpeed * Time.deltaTime);
    }
    #endregion
    #region Move Ladder
    public void SetMoveLadder(bool isMove)
    {
        canMoveLadder = isMove;
    }
    bool isCanOutLadder = false;
    void CheckForLadder()
    {
        int layerLadder = LayerMask.GetMask(Preconsts.Layer_Ladder);
        int layerGround = LayerMask.GetMask(Preconsts.Layer_Ground);
        Vector3 rayOrigin = isOnLadder ? main.position : main.position + Vector3.up * 0.5f;
        Ray rayOnLadder = new Ray(rayOrigin, main.forward);
        Ray rayOutLadder = new Ray(main.position + Vector3.up * 0.05f, Vector3.down * 0.1f);
        isCanOutLadder = Physics.Raycast(rayOutLadder, out RaycastHit _, 0.2f, layerGround);
        bool onLadder = Physics.Raycast(rayOnLadder, out RaycastHit _, distanceLadder, layerLadder);
        isOnLadder = isCanOutLadder ? onLadder && vertical > 0 : onLadder;
        if(isOnLadder) playerAnimBehavior.SetPlayerState(PlayerState.Ladder);
    }
    void HandleLadderMovement()
    {
        bool inputKey = false;
#if UNITY_EDITOR
        inputKey = Input.GetAxis("Vertical") != 0;
#endif
        float verticalInput = inputKey ? Input.GetAxis("Vertical") : fixedJoystick.Vertical;
        Vector3 move = new Vector3(0, verticalInput * speed / 2, 0);
        characterController.Move(move * Time.deltaTime);
        velocity.y = 0;
    }
    #endregion

    #region Jetpack
    private Coroutine corJetpack;
    IEnumerator IEJetPack(float timeJetpack)
    {
        isOnLadder = false;
        playerAnimBehavior.SetPlayerState(PlayerState.Jetpack);
        isJetpack = true;
        velocity.y = 0;
        yield return new WaitForSeconds(timeJetpack);
        isJetpack = false;
    }
    void StopJetpack()
    {
        if(corJetpack != null)
        {
            StopCoroutine(corJetpack);
        }
    }
    public void Jetpack(float timeJetpack)
    {
        StopJetpack();
        corJetpack = StartCoroutine(IEJetPack(timeJetpack));
    }
    public void SetVelocJetpack(float velocJetpack)
    {
        velocity.y = velocJetpack;
    }
    #endregion

    void OnDrawGizmos()
    {
        Gizmos.color = UnityEngine.Color.red;
        Gizmos.DrawWireSphere(checkGround.position, radiusJump);
    }
    #region Jump
    [SerializeField] float radiusJump = 0.495f;
    void SetAnimJump()
    {
        if(playerAnimBehavior.PlayerState != PlayerState.Jump)
        {
            playerAnimBehavior.SetPlayerState(PlayerState.Jump);
        }
    }

    bool wasGrounded = true;
    void CheckJump(float dirInputMagnitude)
    {
        
        int layerMask = LayerMask.GetMask(Preconsts.Layer_Ground);
        isEnemyZone = Physics.CheckSphere(checkGround.position, radiusJump, LayerMask.GetMask(Preconsts.Layer_Enemy_Zone));
        isGround = Physics.CheckSphere(checkGround.position, radiusJump, layerMask);

        if (!wasGrounded && isGround)
        {
            Debug.Log("gournd hit");
            playerAudio.PlayPlayerAudio(PlayerSound.GroudHit);
        }

        wasGrounded = isGround;


        if (!isGround)
        {

            if (!isEnemyZone) {
                if(Physics.Raycast(checkHead.position, Vector3.up, 0.1f) && velocity.y > 1)
                {
                    velocity.y = 1;
                }
                velocity.y += gravity * Time.deltaTime * drag;
                timeEffectSmoke += Time.deltaTime;
                if(velocity.y > 0)
                {
                    SetAnimJump();
                }
                else
                {
                    SetAnimJump();
                }
            } else {
                if(velocity.y > 0)
                {
                    velocity.y += gravity * Time.deltaTime;
                    return;
                }
                if(dirInputMagnitude >= 0.001f)
                {
                    // if (!audioFootSource.isPlaying) {
                    //     audioFootSource.Play();
                    // }
                    playerAudio.PlayPlayerAudio(PlayerSound.FootStep);
                    if(dirInputMagnitude > 0.5f)
                    {
                        playerAnimBehavior.SetPlayerState(PlayerState.Run);
                    }
                    else
                    {
                        playerAnimBehavior.SetPlayerState(PlayerState.Walk);
                    }
                }
                else
                {
                    //audioFootSource.Stop();
                    playerAudio.PlayPlayerAudio(PlayerSound.Idle);
                    playerAnimBehavior.SetPlayerState(PlayerState.Idle);
                }
                SmokePlayer();
                if(timeEffectSmoke != 0) { timeEffectSmoke = 0; }
            }
            
        }
        else
        {
            if(velocity.y > 0)
            {
                velocity.y += gravity * Time.deltaTime;
                return;
            }
            if(dirInputMagnitude >= 0.001f)
            {

                playerAudio.PlayPlayerAudio(PlayerSound.FootStep);
                if(dirInputMagnitude > 0.5f)
                {
                    playerAnimBehavior.SetPlayerState(PlayerState.Run);
                }
                else
                {
                    playerAnimBehavior.SetPlayerState(PlayerState.Walk);
                }
            }
            else
            {
                playerAudio.PlayPlayerAudio(PlayerSound.Idle);
                playerAnimBehavior.SetPlayerState(PlayerState.Idle);
            }
            SmokePlayer();
            if(timeEffectSmoke != 0) { timeEffectSmoke = 0; }
        }

#if UNITY_EDITOR
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
#endif
    }
    public void Jump()
    {
        if(!isGround) {
            if (!isEnemyZone) return;
        }

        playerAudio.PlayPlayerAudio(PlayerSound.Jump);
        timeEffectSmoke = 0;
        velocity.y = Mathf.Sqrt(jumpHeight * -2.5f * gravity);
    }
    #endregion

    #region Landing
    void SmokePlayer()
    {
        if(timeEffectSmoke <= timeDisplaySmoke) return;
        SpawnSmoke();
    }
    public void SpawnSmoke()
    {
        Vector3 posSmoke = transform.position + Vector3.up * 0.15f;
        playerAnimBehavior.SetPlayerState(PlayerState.Landing);
    }
    #endregion

    #region Attack
    void StopAttack()
    {
        isAttack = false;
        if(corPlayerAttack != null)
        {
            StopCoroutine(corPlayerAttack);
        }
    }
    public bool IsJetpack()
    {
        return isJetpack;
    }
    public void PlayerAttack(Transform target, Action actionAttack = null)
    {
        if(isAttack) return;
        StopAttack();
        corPlayerAttack = StartCoroutine(IEPlayerAttack(target, actionAttack));
    }
    Coroutine corPlayerAttack;
    

    IEnumerator IEPlayerAttack(Transform target, Action actionAttack = null)
    {
        isAttack = true;
        if(!isJetpack) velocity.y = -5;
        timeEffectSmoke = 0;
        Vector3 direction = (target.position - main.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        SetAnimWhenAttack();
        while(Quaternion.Angle(main.rotation, targetRotation) > 5f) // Xoay nguoi choi huong vao diem target
        {
            targetRotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);
            main.rotation = Quaternion.Slerp(main.rotation, targetRotation, 10f * Time.deltaTime);
            yield return null;
        }
        playerAnimBehavior.SetPlayerState(PlayerState.Attack);
        yield return new WaitForSeconds(0.7f);
        actionAttack?.Invoke();
        yield return new WaitForSeconds(0.6f);
        SetAnimWhenAttack();
        isAttack = false;
    }
    void SetAnimWhenAttack()
    {
        if(isJetpack)
        {
            playerAnimBehavior.SetPlayerState(PlayerState.Jetpack);
        }
        else if(!isJetpack && playerAnimBehavior.PlayerState != PlayerState.Landing)
        {
            playerAnimBehavior.SetPlayerState(PlayerState.Idle);
        }
    }
    #endregion
    public void SetPosPlayer(Vector3 pos)
    {
        transform.position = pos;
    }
    public void ResetPlayer(float angleYPlayer)
    {
        StopJetpack();
        SetRotateAndAngleCam(angleYPlayer);
        playerAnimBehavior.SetPlayerState(PlayerState.Idle);
    }
    void SetRotateAndAngleCam(float angleYPlayer)
    {
        eulerAngleYCamPlayer = 0;
        main.rotation = Quaternion.Euler(0, angleYPlayer + 180, 0);
        angleYPlayer = (angleYPlayer + 360) % 360;
    }
    
    public IEnumerator Die(float timeDelay, Action completed = null)
    {
        Debug.Log("goi 1 lan");
        playerAnimBehavior.SetPlayerState(PlayerState.Die);
        AudioManager _audio = AudioManager.Instance;
        _audio.SpawnAndPlay(transform, _audio.dieSound, 1f);
        isPlayerDie = true; 
        yield return new WaitForSeconds(timeDelay);
        UIManager.HidePanel<PanelDeath>();
        completed?.Invoke();
        Transform checkpoint = FindObjectOfType<MapController>().
            FindCheckpointByID(DataManager.GamePlayUtility.Profile.CheckPointCurrent());
        SetPosPlayer(checkpoint.position);
        ResetPlayer(0);
        isPlayerDie = false;
    }


    public void SetDrive(bool drive) {
        _isDriving = drive;
    }
    public void BoostSpeed(float speed)
    {
        if (haveBoost) return; // cho boost 1 lan 
        Debug.Log("bufff");
        haveBoost = true;
        this.speed += speed;
    }

    public void SetNormalSpeed()
    {
        haveBoost = false;
        speed = originSpeed;
        Debug.Log(speed);
    }
    Vector3 velocWindy;
   [SerializeField] Vector3 speedFan;

    private void AddWind(Vector3 speed)
    {
        velocWindy = speed;
       // velocity.x -= speedWind;
    }

    [SerializeField] float timeFly = 1f;
    [SerializeField] float velocityFly = 2f;
    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Windy")) {
            Fan fan = other.gameObject.GetComponent<Fan>();
            haveWind = true;
            AddWind(fan.speed);
        }

        if (other.CompareTag(Preconsts.Tag_Jetpack)) {
            Jetpack(timeFly);
            SetVelocJetpack(velocityFly);
        }
    }


    private void OnTriggerEnter(Collider other) {
        if (!isReady) return;

        // if (other.CompareTag(Preconsts.Tag_Car)) {
        //     _isDriving = true;
        // }
        
        if (other.CompareTag(Preconsts.Tag_Normal_Speed)) {
            Debug.Log("het bufff");
            SetNormalSpeed();
        }

        if (other.CompareTag(Preconsts.Tag_Death)) {
            if (isPlayerDie) return;
            isPlayerDie = true;
            Debug.Log("die");
            StartCoroutine(Die(3.5f));
        }

        if (other.CompareTag(Preconsts.Tag_Dash)) {
            velocWindy = new Vector3(-4f, 0.8f, 0);
        }

        
    }
    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Windy")) {
            haveWind = false;
            velocWindy = Vector3.zero;
        }

        if (other.CompareTag(Preconsts.Tag_Dash)) {
            velocWindy = Vector3.zero;
        }
    }
}