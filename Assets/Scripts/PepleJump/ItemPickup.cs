using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PepleJump
{
    public class ItemPickup : MonoBehaviour
    {
        [SerializeField] private GameObject item;
        private Transform pool;

        void Awake()
        {
            pool = transform.parent;
            gameObject.SetActive(false);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<Peple>() != null)        
            {
                item.transform.SetParent(other.transform);
                item.transform.localPosition = Vector3.zero;
                item.SetActive(true);

                ReturnToPool();
            }
        }

        public void ReturnToPool()
        {
            transform.SetParent(pool);
            gameObject.SetActive(false);
        }
    }
}
