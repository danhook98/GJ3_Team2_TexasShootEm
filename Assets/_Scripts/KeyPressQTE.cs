using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TexasShootEm
{
    public class KeyPressQTE : MonoBehaviour
    {
        [SerializeField] private InputReader inputReader;
        
        [Header("Key Press Variables")]
        [SerializeField] private int numberOfKeysToPress = 2;
        [SerializeField] private Vector2 spawnPosition = new Vector2(1.25f, 0.25f);
        [SerializeField] private Vector2 arrowDisplayPosition = new Vector2(0.5f, 0.25f);
        [SerializeField] private float spaceBetween = 1.5f;

        [Header("Key Game Object")] 
        [SerializeField] private Arrow[] arrowPrefabs;
        
        private RandomKeyPressGenerator _keyGenerator;
        private List<Key> _queuedKeys;
        private List<Arrow> _arrowObjects;
        
        private Camera _mainCamera;
        private Vector2 _startPosition;
        private Vector2 _arrowDisplayPosition;
        
        private GameObject _arrowContainer;

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

        private void Update()
        {
            // FOR TESTING ONLY.
            if (Input.GetKeyDown(KeyCode.F))
            {
                GenerateKeys(numberOfKeysToPress);
            }
        }
        
        private void KeyPress(Vector2 input)
        {
            if (_queuedKeys.Count == 0) return; 
            
            var key = _keyGenerator.GetKeyFromDirection(input);

            if (key == _queuedKeys[0])
            {
                Debug.Log("Valid key pressed in sequence!");
                _queuedKeys.RemoveAt(0);
                Destroy(_arrowObjects[0].gameObject);
                _arrowObjects.RemoveAt(0);
            }
        }

        private void GenerateKeys(int numberOfKeys = 2)
        {
            RandomKeyPressGenerator.GenerateKeys(ref _queuedKeys, numberOfKeys);
            
            SpawnArrows(numberOfKeys);
                
            foreach (Key key in _queuedKeys)
            {
                Debug.Log(key);
            }
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
        
        public void LoadData(int value) => numberOfKeysToPress = value;
    }
}