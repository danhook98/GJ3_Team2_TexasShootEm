using UnityEngine;

namespace TexasShootEm
{
    public class BulletController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float lifeTime;
        private Rigidbody2D _rigidbody;
        private float _direction = 1f;
        
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            Destroy(this.gameObject, lifeTime);
        }
        
        private void Update()
        {
            _rigidbody.velocity = new Vector2(_direction * moveSpeed * Time.deltaTime, _rigidbody.velocity.y);
        }

        public void SetDirection(float direction)
        {
            Debug.Log("Direction: " + direction);
            _direction = direction;
            transform.localScale = new Vector3(direction * 3, 3, 3);
        }
    }
}
