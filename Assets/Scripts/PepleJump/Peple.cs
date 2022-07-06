using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peple : MonoBehaviour
{
    [SerializeField, Min(0f)] private float speed = 5f;
    [SerializeField, Min(0f)] private float jumpDelay = 0.3f;
    [SerializeField] private Animator animator;
    [SerializeField] private TransitionHandler handler;
    private new Rigidbody2D rigidbody;
    private SpriteRenderer sprite;
    private float horizontalInput = 0f;
    private bool stopped = true;
    private bool onFirstPlatform = true;
    private bool fade = true;

    void Start()
    {
        handler.StartFadeIn();

        handler.fadeInFinished += OnFadeInEnd;

        rigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void OnFadeInEnd()
    {
        fade = false;
        handler.fadeInFinished -= OnFadeInEnd;
    }

    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            if (onFirstPlatform && !fade)
            {
                onFirstPlatform = false;
                JumpImmediately(21f);
            }

            horizontalInput = Input.mousePosition.x < Screen.width / 2 ? -1f : 1f;
            
            sprite.flipX = horizontalInput > 0f;
        }
        else
            horizontalInput = 0f;

        var screenPosition = Camera.main.WorldToScreenPoint(transform.position);

        if (screenPosition.x < 0)
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - 1, screenPosition.y, screenPosition.z));
        else if (screenPosition.x > Screen.width)
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(0f, screenPosition.y, screenPosition.z));
    }

    void FixedUpdate()
    {
        if (stopped)
            return;

        rigidbody.velocity = new Vector2(horizontalInput * speed, rigidbody.velocity.y);
    }

    public void Jump(float jumpForce)
    {
        Stop();
        StartCoroutine(WaitingForJump(jumpForce));
    }

    public void Stop()
    {
        stopped = true;
        animator.SetBool("Jump", false);
        rigidbody.velocity = Vector2.zero;
    }

    IEnumerator WaitingForJump(float jumpForce)
    {
        yield return new WaitForSeconds(jumpDelay);
        JumpImmediately(jumpForce);
    }

    private void JumpImmediately(float jumpForce)
    {
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce);
        animator.SetBool("Jump", true);
        stopped = false;
    }
}
