using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PepleJump;

public class PepleJumpController : MonoBehaviour
{
    public event Action onGameOver;

    [SerializeField]
    private PlatformController platformController;
    [SerializeField]
    private Peple peple;
    [SerializeField]
    private DeadZone deadZone;

    private float time = 0f;

    private Web web = new Web();

    private void Awake()
    {
        StartCoroutine(web.InitGame());
    }

    private void Update()
    {
        time += Time.deltaTime;
    }

    void Start()
    {
        deadZone.onPepleInDeadzone += OnPepleInDeadZone;
    }

    public void OnPepleInDeadZone()
    {
        peple.StopAllCoroutines();
        onGameOver?.Invoke();

        StartCoroutine(web.EndGame(peple.GetComponent<ScoreController>().score, (int)(time + 0.5f)));
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void NextScene()
    {
        Restart();
    }
}
