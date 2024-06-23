using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RewardedAdsButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float remainingTime;
    private bool isButtonClicked = false;
    private float timeStorage;

    private void Start()
    {
        timeStorage = remainingTime;
        timerText.enabled = false;
        button.onClick.AddListener(OnButtonClick);
    }

    private void Update()
    {
        if (isButtonClicked)
        {
            if (remainingTime > 0)
            {
                remainingTime -= Time.deltaTime;
            }
            else if (remainingTime < 0)
            {
                remainingTime = 0;
                button.interactable = true;
                timerText.enabled = false;
                isButtonClicked = false;
            }

            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    private void OnButtonClick()
    {
        isButtonClicked = true;
        button.interactable = false;
        timerText.enabled = true;
        remainingTime = timeStorage;
    }


}
