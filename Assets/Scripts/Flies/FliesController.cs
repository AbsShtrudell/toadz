using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FliesController : MonoBehaviour
{
    public event System.Action onFliesCountChange;

    [SerializeField, Min(0)] private int _flyCount = 3;
    [SerializeField] private Fly flyRef;
    [SerializeField] private Vector2 flySpawnBottomRightPoint;
    [SerializeField, Min(0f)] private float flySpawnHeight;
    [SerializeField] private List<Transform> clouds;
    private int _currentFlies;
    private int nextDirection;

    public bool isTongueThrusted { get; set; } = false;

    public Fly fly => flyRef;
    public int flyCount => _flyCount;
    public int currentFlies
    {
        get => _currentFlies;
        private set
        {
            _currentFlies = value;
            onFliesCountChange?.Invoke();
        }
    }
    private Transform randomCloud => clouds[Random.Range(0, clouds.Count)];

    void Start()
    {
        currentFlies = flyCount;
        SpawnNewFly();
    }

    public void SpawnNewFly()
    {
        if (currentFlies == 0)
        {
            Destroy(flyRef.gameObject);
            currentFlies--;
            SceneManager.LoadScene("PepleJump");
            return;
        }

        flyRef.transform.SetParent(null);
        currentFlies--;
        nextDirection = Random.Range(0, 2) == 1 ? 1 : -1;
        RespawnCurrentFly();
    }

    public void RespawnCurrentFly()
    {
        var nextPosition = flySpawnBottomRightPoint;

        nextPosition.x *= nextDirection;
        nextDirection *= -1;

        nextPosition.y += Random.Range(0f, flySpawnHeight);

        flyRef.transform.position = nextPosition;

        flyRef.SetTarget(randomCloud.position);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        var topRight = flySpawnBottomRightPoint;
        topRight.y += flySpawnHeight;

        var bottomLeft = flySpawnBottomRightPoint;
        bottomLeft.x *= -1f;

        var topLeft = topRight;
        topLeft.x *= -1f;

        Gizmos.DrawLine(flySpawnBottomRightPoint, topRight);
        Gizmos.DrawLine(bottomLeft, topLeft);
    }
}
