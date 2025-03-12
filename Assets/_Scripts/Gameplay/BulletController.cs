using UnityEngine;

namespace TexasShootEm.Gameplay
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
        
        private void FixedUpdate()
        {
            _rigidbody.velocity = new Vector2(_direction * moveSpeed * Time.fixedDeltaTime, _rigidbody.velocity.y);
        }

        public void SetDirection(bool shootRight)
        {
            _direction = shootRight ? 1 : -1;
            transform.localScale = new Vector3(_direction * 2, 2, 2);
        }
    }
}
