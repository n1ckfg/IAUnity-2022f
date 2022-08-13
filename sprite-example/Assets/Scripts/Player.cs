using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float jumpAmount = 1f;
	public float lerpSpeed = 0.1f;
    public bool alive = true;
    public int health = 3;
    public int score = 0;
    //public Explosion explosionPrefab;

    [HideInInspector] public Vector2 screenCoords;
    [HideInInspector] public bool isRunning = false;
    [HideInInspector] public bool isJumping = false;

    private Vector3 screenBounds;
    private Rigidbody2D rb;
    private Animator animator;
       
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }

    private void Update() {
        if (!isJumping && Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log("Jumping");
            setJump(true);
            rb.AddForce(Vector2.up * jumpAmount, ForceMode2D.Impulse);
        }

        screenCoords = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        screenCoords.x = Mathf.Clamp(screenCoords.x, -screenBounds.x, screenBounds.x); 

        transform.position = Vector2.Lerp(transform.position, new Vector2(screenCoords.x, transform.position.y), lerpSpeed);

		if (screenCoords.x < transform.position.x) {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        } else {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

		if (screenCoords.x > -screenBounds.x/8f && screenCoords.x < screenBounds.x/8f) {
            isRunning = false;
		} else {
            isRunning = true;
        }

        if (alive && health <= 0) {
            alive = false;
            Debug.Log("Player has died.");
            //Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        animator.SetBool("Run", isRunning);
    }

    public void setHit() {
        animator.SetTrigger("Hit");
        health--;
        Debug.Log("Player has taken damage, health: " + health);
    }

    public void setJump(bool b) {
        isJumping = b;
        animator.SetBool("Jump", isJumping);
    }

}
