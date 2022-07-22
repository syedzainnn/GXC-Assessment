using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Photon.Pun;


public class CharacterController : MonoBehaviour
{
    public float speed;
    public Animator playerAnimator;
    public Vector3 lastMoveDirection;
    private PhotonView view;
    
    void Start()
    {
        view = GetComponent<PhotonView>();
    }
    void Update()
    {
        if (!view.IsMine) return;
        HandleMovement();
    }

    private void HandleMovement()
    {
        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(KeyCode.W))
        {
            moveY = +1f;
        }

        if (Input.GetKey(KeyCode.S))
        {
            moveY = -1f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveX = -1f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveX = +1f;
        }



        Vector3 moveDir = new Vector3(moveX, moveY).normalized;
        
        bool isIdle = moveX == 0 && moveY == 0;
        if (isIdle)
        {
            PlayIdleAnimation(lastMoveDirection);
        }
        else
        {
            lastMoveDirection = moveDir;
            transform.position += moveDir * speed * Time.deltaTime;
            PlayWalkingAnimations(moveDir);
        }
    }

    public void PlayWalkingAnimations(Vector3 moveDir)
    {
        StopIdleAnimations();
        // Upwards
        if (Vector3.Distance(moveDir, Vector3.up) <= 0.5f)
        {
            playerAnimator.SetBool("UpRun", true);
            playerAnimator.SetBool("DownRun", false);
            playerAnimator.SetBool("RightRun", false);
            playerAnimator.SetBool("LeftRun", false);
        }
        // Downwards
        else if(Vector3.Distance(moveDir, Vector3.down) <= 0.5f)
        {
            playerAnimator.SetBool("UpRun", false);
            playerAnimator.SetBool("DownRun", true);
            playerAnimator.SetBool("RightRun", false);
            playerAnimator.SetBool("LeftRun", false);
        }
        // Left
        else if(Vector3.Distance(moveDir, Vector3.left) <= 0.5f)
        {
            playerAnimator.SetBool("UpRun", false);
            playerAnimator.SetBool("DownRun", false);
            playerAnimator.SetBool("RightRun", false);
            playerAnimator.SetBool("LeftRun", true);
        }
        // Right
        else if(Vector3.Distance(moveDir, Vector3.right) <= 0.01f)
        {
            playerAnimator.SetBool("UpRun", false);
            playerAnimator.SetBool("DownRun", false);
            playerAnimator.SetBool("RightRun", true);
            playerAnimator.SetBool("LeftRun", false);
        }
    }

    public void PlayIdleAnimation(Vector3 faceDir)
    {
        // Upwards
        if (Vector3.Distance(faceDir, Vector3.up) <= 0.5f)
        {
            playerAnimator.SetBool("UpIdle", true);
            playerAnimator.SetBool("DownIdle", false);
            playerAnimator.SetBool("RightIdle", false);
            playerAnimator.SetBool("LeftIdle", false);
        }
        // Downwards
        else if(Vector3.Distance(faceDir, Vector3.down) <= 0.5f)
        {
            playerAnimator.SetBool("UpIdle", false);
            playerAnimator.SetBool("DownIdle", true);
            playerAnimator.SetBool("RightIdle", false);
            playerAnimator.SetBool("LeftIdle", false);
        }
        // Left
        else if(Vector3.Distance(faceDir, Vector3.left) <= 0.5f)
        {
            playerAnimator.SetBool("UpIdle", false);
            playerAnimator.SetBool("DownIdle", false);
            playerAnimator.SetBool("RightIdle", false);
            playerAnimator.SetBool("LeftIdle", true);
        }
        // Right
        else if(Vector3.Distance(faceDir, Vector3.right) <= 0.01f)
        {
            playerAnimator.SetBool("UpIdle", false);
            playerAnimator.SetBool("DownIdle", false);
            playerAnimator.SetBool("RightIdle", true);
            playerAnimator.SetBool("LeftIdle", false);
        }
    }

    public void StopIdleAnimations()
    {
        playerAnimator.SetBool("UpIdle", false);
        playerAnimator.SetBool("DownIdle", false);
        playerAnimator.SetBool("RightIdle", false);
        playerAnimator.SetBool("LeftIdle", false);
    }
    
}
