using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PepleJump;

public class Peple : MonoBehaviour
{
    [SerializeField, Min(0f)] private float speed = 5f;
    [SerializeField, Min(0f)] private float _jumpDelay = 0.3f;
    [SerializeField] private Animator animator;
    [Zenject.Inject] private InputHandler inputHandler;
    private new Rigidbody2D rigidbody;
    private SpriteRenderer sprite;
    private float horizontalInput = 0f;
    private bool stopped = true;
    private bool onFirstPlatform = true;
    public bool fade = true;

    public float jumpDelay => _jumpDelay;

    private Coroutine jumpCoroutine;

    public Vector2 velocity
    {
        get => rigidbody.velocity;
        set => rigidbody.velocity = value;
    }

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Move();

        var screenPosition = Camera.main.WorldToScreenPoint(transform.position);

        if (screenPosition.x < 0)
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - 1, screenPosition.y, screenPosition.z));
        else if (screenPosition.x > Screen.width)
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(0f, screenPosition.y, screenPosition.z));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (rigidbody.velocity.y > 0) return;

        IPlatform platform;

        if (collision.gameObject.TryGetComponent<IPlatform>(out platform))
        {
            platform.Action(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (rigidbody.velocity.y > 0) return;

        IPlatform platform;

        if (collision.gameObject.TryGetComponent<IPlatform>(out platform))
        {
            platform.Action(this);
        }
    }

    private void Move()
    {
        if (stopped)
            return;
        if (inputHandler.ActiveInput.HasFlag(InputHandler.Type.None))
            rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
        if (inputHandler.ActiveInput.HasFlag(InputHandler.Type.Move_Left))
        {
            rigidbody.velocity = new Vector2(-1 * speed, rigidbody.velocity.y);
            sprite.flipX = false;
        }
        if (inputHandler.ActiveInput.HasFlag(InputHandler.Type.Move_Right))
        {
            rigidbody.velocity = new Vector2(speed, rigidbody.velocity.y);
            sprite.flipX = true;
        }
    }

    public void Jump(float jumpForce, Transform platform)
    {
        Stop();
        transform.SetParent(platform);
        if(jumpCoroutine != null) StopCoroutine(jumpCoroutine);
        jumpCoroutine = StartCoroutine(WaitingForJump(jumpForce));
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
        transform.SetParent(null);
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce);
        animator.SetBool("Jump", true);
        stopped = false;
    }
}
