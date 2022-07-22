using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTraits : MonoBehaviour
{
    [SerializeField, Min(0f)] private float _jumpForceNormal = 21f;
    [SerializeField, Min(0f)] private float _jumpForceSpring = 31f;
    [SerializeField, Min(0f)] private float _horizontalSpeed = 5f;
    [SerializeField, Min(0f)] private float _fallingSpeed = 5f;
    [SerializeField] private Sprite _normalPlatform;
    [SerializeField] private Sprite _springPlatform;
    [SerializeField] private Sprite _fragilePlatform;
    [SerializeField] private Sprite _brokenPlatform;
    [SerializeField] private Sprite _targetPlatform;
    [SerializeField] private Sprite _disposablePlatform;

    public float jumpForceNormal => _jumpForceNormal;
    public float jumpForceSpring => _jumpForceSpring;
    public float horizontalSpeed => _horizontalSpeed;
    public float fallingSpeed => _fallingSpeed;
    public Sprite normalPlatform => _normalPlatform;
    public Sprite springPlatform => _springPlatform;
    public Sprite fragilePlatform => _fragilePlatform;
    public Sprite brokenPlatform => _brokenPlatform; 
    public Sprite targetPlatform => _targetPlatform;
    public Sprite disposablePlatform => _disposablePlatform;

}
