using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMetricsUIManager : MonoBehaviour
{
    PlayerManager playerManager;

    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text strengthText;
    void Start()
    {
        playerManager = PlayerManager.Instance;

        if (playerManager != null)
        {
            //Subscribe to the OnStatsChanged event
            playerManager.OnStatsChanged += UpdateUI;
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        healthText.text = "Health: " + playerManager.Health.ToString() + "/100";
        strengthText.text = "Strength: " + playerManager.Strength.ToString();
    }
}
