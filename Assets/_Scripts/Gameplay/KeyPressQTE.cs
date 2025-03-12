using System.Collections.Generic;
using TexasShootEm.EventSystem;
using UnityEngine;

namespace TexasShootEm.Gameplay
{
    public class KeyPressQTE : MonoBehaviour
    {
        [SerializeField] private InputReader inputReader;
        
        [Header("Audio")]
        [SerializeField] private AudioClipSOEvent playSfxEvent;
        [SerializeField] private AudioClipSO clickSound;
        
        [Header("Events")]
        [SerializeField] private FloatEvent sendScoreEvent;
        
        [Header("Key Press Variables")]
        [SerializeField] private int numberOfKeysToPress = 2;
        [SerializeField] private Vector2 spawnPosition = new Vector2(1.25f, 0.25f);
        [SerializeField] private Vector2 arrowDisplayPosition = new Vector2(0.5f, 0.25f);
        [SerializeField] private float spaceBetween = 1.5f;

        [Header("Key Game Object")] 
        [SerializeField] private Arrow[] arrowPrefabs;
        
        private bool _keyPressQteActive = false;
        
        private RandomKeyPressGenerator _keyGenerator;
        private List<Key> _queuedKeys;
        private List<Arrow> _arrowObjects;
        
        private Camera _mainCamera;
        private Vector2 _startPosition;
        private Vector2 _arrowDisplayPosition;
        
        private GameObject _arrowContainer;

        private float _qteScore = 0;

        private void Awake()
        {
            _keyGenerator = new RandomKeyPressGenerator();
            _queuedKeys = new List<Key>();
            _arrowObjects = new List<Arrow>();
            
            _mainCamera = Camera.main;
            _startPosition = _mainCamera.ViewportToWorldPoint(spawnPosition);
            _arrowDisplayPosition = _mainCamera.ViewportToWorldPoint(arrowDisplayPosition);
            
            CreateArrowContainer();
        }

        private void OnEnable() => inputReader.OnDirectionalEvent += KeyPress;
        private void OnDisable() => inputReader.OnDirectionalEvent -= KeyPress;
        
        private void KeyPress(Vector2 input)
        {
            if (!_keyPressQteActive) return; 
            
            if (_queuedKeys.Count == 0) return; 
            
            Key key = _keyGenerator.GetKeyFromDirection(input);
            float numOfKeysToPress = numberOfKeysToPress;
            
            playSfxEvent.Invoke(clickSound);
            
            if (key == _queuedKeys[0])
            {
                Debug.Log("Valid key pressed in sequence!");
                _qteScore += 1/numOfKeysToPress;
                Debug.Log(_qteScore);
            }
            _queuedKeys.RemoveAt(0);
            Destroy(_arrowObjects[0].gameObject);
            _arrowObjects.RemoveAt(0);
            
            // This was the last key.
            if (_queuedKeys.Count == 0)
            {
                // TODO: calculate score percentage to send
                Debug.Log(_qteScore);
                sendScoreEvent.Invoke(_qteScore);
                _keyPressQteActive = false;
            }
        }

        private void GenerateKeys(int numberOfKeys = 2)
        {
            RandomKeyPressGenerator.GenerateKeys(ref _queuedKeys, numberOfKeys);
            
            SpawnArrows(numberOfKeys);
        }

        private void SpawnArrows(int arrowsToSpawn)
        {
            _arrowObjects.Clear();
            _arrowContainer.transform.position = _startPosition;
            
            for (int i = 0; i < arrowsToSpawn; i++)
            {
                Vector2 pos = _startPosition + new Vector2(i * spaceBetween, 0);
                
                int arrowIndex = (int)_queuedKeys[i];
                Arrow arrow = Instantiate(arrowPrefabs[arrowIndex], transform);
                arrow.SetPosition(pos);
                arrow.transform.SetParent(_arrowContainer.transform);
                
                _arrowObjects.Add(arrow);
            }

            float centerX = ((arrowsToSpawn - 1) * spaceBetween) / 2;
            
            _arrowContainer.transform.position = _arrowDisplayPosition - new Vector2(centerX, 0f);
        }

        private void CreateArrowContainer()
        {
            _arrowContainer = new GameObject();
            _arrowContainer.transform.SetParent(transform);
            _arrowContainer.name = "ArrowContainer";
        }

        public void ActivateKeyPressQTE()
        {
            _keyPressQteActive = true;
            GenerateKeys(numberOfKeysToPress);
        }
        
        public void LoadData(int value) => numberOfKeysToPress = value;
    }
}