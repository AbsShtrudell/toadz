using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    public event Action<bool> onRaundPlayed;
    public void PlayRound()
    {
        PlayCroackAnimation();
    }

    private bool Croack()
    {
        int result = UnityEngine.Random.Range(0, 100);
        if (result < 80)
            return true;
        return false;
    }

    private void PlayCroackAnimation()
    {
        StartCoroutine(CroackAnimationTest());
    }

    private void OnCroack()
    {
        onRaundPlayed?.Invoke(Croack());
    }

    IEnumerator CroackAnimationTest()
    {
        yield return new WaitForSeconds(3);
        OnCroack();
    }
}
