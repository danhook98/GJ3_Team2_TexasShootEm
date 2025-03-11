using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Android.LowLevel;
using UnityEngine.UI;

namespace TexasShootEm
{
    public class CountdownController : MonoBehaviour
    {
        private TextMeshProUGUI _countdownDisplay;
        private int _countdownTime;

        private void Awake()
        {
            _countdownDisplay = GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            _countdownTime = 3;
            StartCoroutine(CountdownToStart());
        }

        private IEnumerator CountdownToStart()
        {
            while (_countdownTime > 0)
            {
                _countdownDisplay.text = _countdownTime.ToString();

                yield return new WaitForSeconds(1f);

                _countdownTime--;
            }

            _countdownDisplay.text = "Draw!";

            yield return new WaitForSeconds(1f);

            _countdownDisplay.gameObject.SetActive(false);
        }
    }
}