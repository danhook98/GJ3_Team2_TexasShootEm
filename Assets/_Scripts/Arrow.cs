using UnityEngine;

namespace TexasShootEm
{
    public class Arrow : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;
        
        private BoxCollider2D _collider;

        private void Awake()
        {
            _collider = GetComponent<BoxCollider2D>();
        }

        private void Update()
        {
            // transform.Translate(moveSpeed * Time.deltaTime * Vector2.left);
        }
        
        public void SetPosition(Vector2 pos) => transform.position = pos;
        public Bounds GetBounds() => _collider.bounds;
    }
}