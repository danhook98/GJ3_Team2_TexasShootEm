using System.Collections;
using UnityEngine;
using TexasShootEm.EventSystem;

namespace TexasShootEm
{
    public class EntityController : MonoBehaviour
    {
        [Header("Events")] 
        [SerializeField] private VoidEvent entityShoot;
        [SerializeField] private VoidEvent entityDeath;
        [SerializeField] private VoidEvent entityAim;
        [SerializeField] private AudioClipSOEvent playSfx;

        [Header("Audio Clip SO")]
        [SerializeField] private AudioClipSO entityDeathSfx;
        [SerializeField] private AudioClipSO entityShootSfx;

        [Header("Bullet Related")] 
        [SerializeField] private Transform bulletSpawn;
        [SerializeField] private BulletController bullet;
        [SerializeField] private bool shootRight = true;
        
        private Vector3 _deathOffset = new(0f, -0.15f, 0f);

        private Transform _transform;
        
        private Animator _entityAnim;
        private bool _isDead = false;

        private void Awake()
        {
            _transform = transform;
            _entityAnim = GetComponent<Animator>();
        }

        private IEnumerator EntityShoot()
        {
            if (_isDead) yield break; // Condition needed to avoid playing gunshot SFX when dead.
            
            Debug.Log("Starting shoot coroutine");
            _entityAnim.SetTrigger("Shoot");
            
            yield return new WaitForSeconds(0.5f);
            
            playSfx.Invoke(entityShootSfx);
            SpawnBullet();
            
            yield return new WaitForSeconds(0.5f);
            
            entityShoot.Invoke(new Empty());
        }
        
        private IEnumerator EntityDeath()
        {
            _isDead = true;
            Debug.Log("Starting death coroutine");
            _entityAnim.SetTrigger("Death");
            _transform.position += _deathOffset;
            entityDeath.Invoke(new Empty());
            playSfx.Invoke(entityDeathSfx);
            yield return new WaitForSeconds(0.9f);
            _entityAnim.SetTrigger("StayDead");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                SpawnBullet();
            }
        }

        private void SpawnBullet()
        {
            BulletController spawnedBullet = Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
            spawnedBullet.SetDirection(shootRight);
        }
        
        public void Shoot() => StartCoroutine(EntityShoot());
        public void Death() => StartCoroutine(EntityDeath());
    }
}