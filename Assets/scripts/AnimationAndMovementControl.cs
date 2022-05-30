using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class AnimationAndMovementControl : MonoBehaviour
{
    PlayerInput playerInput;
    CharacterController characterController;
    Animator animator;

    public Camera fpsCam;
    public GameObject bullet;
    public Transform attackPoint;

    int isWalkingHash;
    int isRunningHash;
    int isShootingHash;

    Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector2 currentShootInput;
    Vector3 currentShoot;
    Vector3 positionToLookAt;
    bool isMovementPressed;
    bool isRunPressed;
    bool isShootPressed;
    bool allowInvoke = true;
    bool readyToShoot = true;
    float rotationFactorPerFrame = 7.0f;

    public float damage = 10.0f;
    public float attackSpeed = 0.5f;
    public float range = 15.0f;
    public float multiplier = 5.0f;

    void Awake() {
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isShootingHash = Animator.StringToHash("isShooting");

        playerInput.CharacterControls.Move.started += onMovementInput;
        playerInput.CharacterControls.Move.canceled += onMovementInput;
        playerInput.CharacterControls.Move.performed += onMovementInput;

        playerInput.CharacterControls.Run.started += onRun;
        playerInput.CharacterControls.Run.canceled += onRun;

        playerInput.CharacterControls.Shoot.started += onShoot;
        playerInput.CharacterControls.Shoot.canceled += onShoot;
        playerInput.CharacterControls.Shoot.performed += onShoot;


    }

    // Input states
    void onShoot(InputAction.CallbackContext context){
        currentShootInput = context.ReadValue<Vector2>();
        currentShoot.x = currentShootInput.x;
        currentShoot.z = currentShootInput.y;
        isShootPressed = currentShootInput.x != 0 || currentShootInput.y != 0;
    }

    void onRun(InputAction.CallbackContext context){
        isRunPressed = context.ReadValueAsButton();
    }

    void onMovementInput (InputAction.CallbackContext context){
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }


    // phisics
    void handleRotation(){
        Vector3 positionToLookAt;

        if(isShootPressed){
            positionToLookAt.x = currentShoot.x;
            positionToLookAt.y = 0f;
            positionToLookAt.z = currentShoot.z;
        } else {
            positionToLookAt.x = currentMovement.x;
            positionToLookAt.y = 0f;
            positionToLookAt.z = currentMovement.z;
        }
        



        Quaternion currentRotation = transform.rotation;
        if(isMovementPressed || isShootPressed){
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }
    }

    void handleAnimation(){
        // get parameters
        bool isWalking = animator.GetBool( isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);
        bool isShooting = animator.GetBool( isShootingHash);

        if ((isMovementPressed && !isWalking)){
            animator.SetBool( isWalkingHash, true);
        }
        else if ((!isMovementPressed && isWalking)){
            animator.SetBool(isWalkingHash, false);
        }

        if(((isMovementPressed && !isRunPressed) && !isRunning)){
            animator.SetBool(isRunningHash, true);
        }
        else if (((!isMovementPressed || isRunPressed)&& isRunning)){
            animator.SetBool(isRunningHash, false);
        }
        // if (isShootPressed)
        // {
        //     animator.SetBool(isShootingHash, true);
        // } else if (!isShootPressed && isShooting){
        //     animator.SetBool(isShootingHash, false);
        // }
    }

    void handleGravity(){
        if(characterController.isGrounded){
            float groundedGravity = -0.05f;
            currentMovement.y = groundedGravity;
        } else {
            float gravity = -0.3f;
            currentMovement.y += gravity;
        }
    }
    void shoot_now(){
        if(readyToShoot) {
            Invoke("handleShooting",0.2f);
            readyToShoot = false;
            }
    }

    void handleShooting(){
        //Instantiate(bullet, attackPoint.position, attackPoint.rotation);

        // RaycastHit hit;
         Vector3 targetPoint;
        
        // if(Physics.Raycast(transform.position,positionToLookAt,out hit,range )){
        //     targetPoint = hit.point;
        // } else {
           targetPoint.x = attackPoint.position.x - transform.position.x;
            targetPoint.y = 0f;
            targetPoint.z = attackPoint.position.z -transform.position.z;
        // }
        // Vector3 direction = targetPoint;
         GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);

        // currentBullet.transform.forward = direction.normalized;
        currentBullet.GetComponent<Rigidbody>().AddForce(currentShoot * 7f, ForceMode.Impulse);

        if(allowInvoke){
            Invoke("ResetShot", attackSpeed);
            allowInvoke = false;
        }
    }
    void ResetShot(){
        allowInvoke = true;
        readyToShoot = true;
    }


    void Update()
    {
        handleGravity();
        handleRotation();
        handleAnimation();

        if (isShootPressed){
            shoot_now();
        }
        if (!isRunPressed){
            characterController.Move(currentMovement * multiplier * Time.deltaTime);
        } else {
            characterController.Move(currentMovement * Time.deltaTime);
        }

    }


    void OnEnable() {
        playerInput.CharacterControls.Enable();
    }

    void OnDisable() {
        playerInput.CharacterControls.Disable();
    }
    private void OnDestroy() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
