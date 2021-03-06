﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] float baseMoveSpeed;
    [SerializeField] float platformMoveSpeedModifier;
    [SerializeField] float jumpSpeed;
    [SerializeField] float groundCheckRadius;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] Transform groundCheck;
    [SerializeField] LevelManager levelManager;

    private bool isGrounded;
    private bool onPlatform;
    private float moveSpeed;

    private SpriteRenderer spriteRenderer;   // Use to change color of character
    private Rigidbody2D rigidBody;
    private Animator animator;

	void Start () {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.rigidBody = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        this.onPlatform = false;
        this.isGrounded = true;
        this.moveSpeed = this.baseMoveSpeed;
	}
	
	void Update () {
        moveRightOrLeft();
        jump();
        doAnimations();
	}

    private void moveRightOrLeft() {
        if (this.onPlatform) {
            this.moveSpeed = this.baseMoveSpeed * this.platformMoveSpeedModifier;
        } else {
            this.moveSpeed = this.baseMoveSpeed;
        }
        if (Input.GetAxisRaw("Horizontal") > 0) {
            this.transform.localScale = new Vector3(.25f, .25f, 1f);
            this.rigidBody.velocity = new Vector3(moveSpeed, this.rigidBody.velocity.y, 0f);
        } else if (Input.GetAxisRaw("Horizontal") < 0) {
            this.rigidBody.velocity = new Vector3(-1 * moveSpeed, this.rigidBody.velocity.y, 0f);
            this.transform.localScale = new Vector3(-.25f, .25f, 1f);
        } else {
            this.rigidBody.velocity = new Vector3(0f, this.rigidBody.velocity.y, 0f);
        }
    }

    private void jump() {
        this.isGrounded = Physics2D.OverlapCircle(this.groundCheck.position, this.groundCheckRadius, this.whatIsGround);
        if (Input.GetButtonDown("Jump") && this.isGrounded) {
            this.rigidBody.velocity = new Vector3(this.rigidBody.velocity.x, this.jumpSpeed, 0f);
        }
    }

    private void doAnimations() {
        this.animator.SetFloat("Speed", Mathf.Abs(this.rigidBody.velocity.x));
        this.animator.SetBool("isGrounded", this.isGrounded);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "StreamOfConsciousness") {
            this.levelManager.handlePlayerDeath();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "MovingPlatform") {
            this.onPlatform = true;
            this.transform.parent = collision.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.tag == "MovingPlatform") {
            this.onPlatform = false;
            this.transform.parent = null;
        }
    }


}
