using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireButton : MonoBehaviour
{
    [SerializeField] Image image;

    [SerializeField] Sprite clickSprite;
    [SerializeField] float delay;
    private Sprite sprite;

    private void Awake()
    {
        sprite = image.sprite;
    }

    public void Click()
    {
        StopAllCoroutines();

        image.sprite = clickSprite;

        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(delay);

        image.sprite = sprite;
    }
}
