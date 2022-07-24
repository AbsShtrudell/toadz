using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PepleJump
{
    public class Jetpack : MonoBehaviour
    {
        [SerializeField] private float time = 3f;
        [SerializeField] private float force = 100f;
        [SerializeField] private float speed = 20f;
        private new Rigidbody2D rigidbody;
        private float clock = 0f;

        [Zenject.Inject]
        void SetRigidbody(Peple peple)
        {
            rigidbody = peple.GetComponent<Rigidbody2D>();
        }

        void Update()
        {        
            rigidbody.velocity = Vector2.MoveTowards(rigidbody.velocity, Vector2.up * speed, Time.deltaTime * force);

            clock += Time.deltaTime;

            if (clock >= time)
            {
                clock = 0f;
                transform.SetParent(null);
                gameObject.SetActive(false);
            }
        }
    }
}