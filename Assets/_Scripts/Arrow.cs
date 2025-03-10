using UnityEngine;

namespace TexasShootEm
{
    public class Arrow : MonoBehaviour
    {
        private BoxCollider2D _collider;

        private void Awake() => _collider = GetComponent<BoxCollider2D>();
        
        public void SetPosition(Vector2 pos) => transform.position = pos;
    }
}