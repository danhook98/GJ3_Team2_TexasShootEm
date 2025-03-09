using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace TexasShootEm
{
    public class BulletController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float _lifeTime;
        private Rigidbody2D _rigidbody;
        private float _direction = 1f;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            Destroy(this.gameObject, _lifeTime);
        }
        
        private void Update()
        {
            _rigidbody.velocity = new Vector2(_direction * moveSpeed, _rigidbody.velocity.y) * Time.deltaTime;

            // TODO: Testing purposes only, remove after testing.
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SetDirection(-1);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                SetDirection(1);
            }
        }

        public void SetDirection(float direction)
        {
            _direction = direction;
            this.transform.localScale = new Vector3(direction, 1, 1);
        }
    }
}
