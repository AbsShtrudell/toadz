using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCloud : MonoBehaviour
{
    public float speed = 2;

    void Update()
    {
        transform.Translate(new Vector3(1 * speed * Time.deltaTime, 0, 0));

        Vector3 goscreen = Camera.main.WorldToScreenPoint(transform.position);
        float distx = Vector3.Distance(new Vector3(Screen.width / 2, 0f, 0f), new Vector3(goscreen.x, 0f, 0f));

        if (distx >= Screen.width)
            transform.position = new Vector3(Camera.main.transform.position.x - Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 1.5f, 0f, 0f)).x, transform.position.y, transform.position.z);

    }

    public void SetSprite(Sprite sprite)
    {
        GetComponent<SpriteRenderer>().sprite = sprite;
    }
}
