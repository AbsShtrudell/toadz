using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Platform : MonoBehaviour
{
    [System.Serializable]
    public enum Type
    {
        Normal, Spring, Fragile, Broken, Target, Disposable
    }

    private delegate void OnCollisionAction(Collision2D collision);

    [SerializeField] private Type _type = Type.Normal;
    [Zenject.Inject] private PlatformController controller;
    [Zenject.Inject] private PlatformTraits traits;
    [Zenject.Inject] private Peple peple;
    private SpriteRenderer spriteRenderer;
    private OnCollisionAction action;
    private Coroutine horizontalMovement = null;

    public event System.Action onPepleWon;

    public Type type
    {
        get => _type;
        set
        {
            _type = value;

            switch (value)
            {
                case Type.Normal:
                    action = NormalAction;
                    spriteRenderer.sprite = traits.normalPlatform;
                    break;
                case Type.Spring:
                    action = SpringAction;
                    spriteRenderer.sprite = traits.springPlatform;
                    break;
                case Type.Fragile:
                    action = FragileAction;
                    spriteRenderer.sprite = traits.fragilePlatform;
                    break;
                case Type.Broken:
                    action = null;
                    spriteRenderer.sprite = traits.brokenPlatform;
                    gameObject.layer = LayerMask.NameToLayer("BrokenPlatform");
                    StartCoroutine(Falling());
                    break;
                case Type.Target:
                    action = TargetAction;
                    spriteRenderer.sprite = traits.targetPlatform;
                    break;
                case Type.Disposable:
                    action = DisposableAction;
                    spriteRenderer.sprite = traits.disposablePlatform;
                    break;
            }
        }
    }

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        action = NormalAction;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.y < 0f)
            action(collision);
    }

    void NormalAction(Collision2D collision)
    {
        peple.Jump(traits.jumpForceNormal, transform);
    }

    void SpringAction(Collision2D collision)
    {
        if (Mathf.Abs(transform.position.x - peple.transform.position.x) < 0.45f)
            peple.Jump(traits.jumpForceSpring, transform);
        else
            peple.Jump(traits.jumpForceNormal, transform);
    }

    void FragileAction(Collision2D collision)
    {
        peple.velocity = collision.relativeVelocity;

        type = Type.Broken;
    }

    void TargetAction(Collision2D collision)
    {
        peple.Stop();

        onPepleWon?.Invoke();
    }

    void DisposableAction(Collision2D collision)
    {
        NormalAction(collision);

        StartCoroutine(Disposing());
    }

    IEnumerator Falling()
    {
        while (type == Type.Broken)
        {
            transform.Translate(Vector3.down * traits.fallingSpeed * Time.deltaTime);

            yield return new WaitForEndOfFrame();
        }
        
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    IEnumerator Disposing()
    {
        yield return new WaitForSeconds(peple.jumpDelay);

        type = Type.Broken;
    }

    public void StartHorizontalMovement()
    {
        horizontalMovement = StartCoroutine(HorizontalMovement());
    }

    public void StopHorizontalMovement()
    {
        if (horizontalMovement != null)
        {
            StopCoroutine(horizontalMovement);
            horizontalMovement = null;
        }
    }

    IEnumerator HorizontalMovement()
    {
        float targetX = controller.horizontalSpreadMax;

        while (true)
        {    
            if (transform.position.x == controller.horizontalSpreadMax)
                targetX = controller.horizontalSpreadMin;
            else if (transform.position.x == controller.horizontalSpreadMin)
                targetX = controller.horizontalSpreadMax;

            var nextPosition = transform.position;
            nextPosition.x = Mathf.MoveTowards(nextPosition.x, targetX, traits.horizontalSpeed * Time.deltaTime);
            transform.position = nextPosition;

            yield return new WaitForEndOfFrame();
        }
    }
}
