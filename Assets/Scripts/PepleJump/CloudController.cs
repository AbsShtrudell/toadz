using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour
{
    [SerializeField] private GameObject cloudRef;
    [SerializeField, Min(0f)] private float verticalSpreadMin = 0.5f;
    [SerializeField, Min(0f)] private float verticalSpreadMax = 1f;
    [SerializeField] private float _horizontalSpreadMin = -1.8f;
    [SerializeField] private float _horizontalSpreadMax = 1.8f;
    [SerializeField, Min(1)] protected int maxCloudsCount = 7;
    [SerializeField] private List<Sprite> cloudsSprites;

    private float nextY;

    public float horizontalSpreadMin => _horizontalSpreadMin;
    public float horizontalSpreadMax => _horizontalSpreadMax;

    protected virtual void Start()
    {
        nextY = cloudRef.transform.position.y + Random.Range(verticalSpreadMin, verticalSpreadMax);

        for (int i = 1; i <= maxCloudsCount; i++)
        {
            MovingCloud cloud = GameObject.Instantiate(cloudRef).GetComponent<MovingCloud>();

            SpawnNext(cloud);
        }
    }

    public virtual void SpawnNext(MovingCloud cloud)
    {
        Vector3 position = cloud.transform.position;
        position.y = nextY;
        position.x = Random.Range(horizontalSpreadMin, horizontalSpreadMax);
        cloud.transform.position = position;
        cloud.speed = Random.Range(1f, 2f);
        cloud.SetSprite(cloudsSprites[Random.Range(0, cloudsSprites.Count - 1)]);
        nextY += Random.Range(verticalSpreadMin, verticalSpreadMax);
    }
}
