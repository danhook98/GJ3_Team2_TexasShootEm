using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Android.LowLevel;
using UnityEngine.UI;

namespace TexasShootEm { }

public class CountdownController : MonoBehaviour
{
    private int countdownTime;
    public GameObject countdownDisplay;

    private void Start()
    {
        StartCoroutine(CountdownToStart());
        //countdownDisplay = GetComponent<Text>();
        countdownTime=3;
    }

    IEnumerator CountdownToStart()
    {
        while (countdownTime > 0)
        {
            //countdownDisplay.text = countdownTime.ToString();

            yield return new WaitForSeconds(1f);

            countdownTime--;
        }

        //countdownDisplay.text = "GO!";

        

        yield return new WaitForSeconds(1f);

        countdownDisplay.gameObject.SetActive(false);
    }
}
    