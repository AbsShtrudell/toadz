using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [System.Serializable]
    public enum Type
    {
        Normal, Spring, Fragile, Broken, Target
    }

    private delegate void OnCollisionAction(Collision2D collision);

    [SerializeField, Min(0f)] private float jumpForceNormal = 20f;
    [SerializeField, Min(0f)] private float jumpForceSpring = 30f;
    [SerializeField, Min(0f)] private float fallingSpeed = 5f;
    [SerializeField] private Type _type = Type.Normal;
    [Zenject.Inject] private PlatformController controller;
    private SpriteRenderer spriteRenderer;
    private OnCollisionAction action;

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
                    spriteRenderer.sprite = controller.platformSprites[(int)Type.Normal];
                    break;
                case Type.Spring:
                    action = SpringAction;
                    spriteRenderer.sprite = controller.platformSprites[(int)Type.Spring];
                    break;
                case Type.Fragile:
                    action = FragileAction;
                    spriteRenderer.sprite = controller.platformSprites[(int)Type.Fragile];
                    break;
                case Type.Broken:
                    action = null;
                    spriteRenderer.sprite = controller.platformSprites[(int)Type.Broken];
                    gameObject.layer = LayerMask.NameToLayer("BrokenPlatform");
                    StartCoroutine(Falling());
                    break;
                //case Type.Target:

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
        if (collision.relativeVelocity.y <= 0f)
            action(collision);
    }

    void NormalAction(Collision2D collision)
    {
        collision.collider.GetComponent<Peple>().Jump(jumpForceNormal);
    }

    void SpringAction(Collision2D collision)
    {
        var peple = collision.collider.GetComponent<Peple>();

        if (Mathf.Abs(transform.position.x - peple.transform.position.x) < 0.45f)
            peple.Jump(jumpForceSpring);
        else
            peple.Jump(jumpForceNormal);
    }

    void FragileAction(Collision2D collision)
    {
        collision.collider.GetComponent<Rigidbody2D>().velocity = collision.relativeVelocity;

        type = Type.Broken;
    }

    void TargetAction(Collision2D collision)
    {

    }

    IEnumerator Falling()
    {
        while (type == Type.Broken)
        {
            transform.Translate(Vector3.down * fallingSpeed * Time.deltaTime);

            yield return new WaitForEndOfFrame();
        }
        
        gameObject.layer = LayerMask.NameToLayer("Default");
    }
}
