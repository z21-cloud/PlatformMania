using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

namespace PlatfromMania.UI
{
    public class CoinsView : MonoBehaviour, ICoinsView
    {
        [SerializeField] private TextMeshProUGUI coinsText;
        [SerializeField] private string prefix = "Coins: ";

        public void UpdateCoins(int amoint)
        {
            if (coinsText != null)
                coinsText.text = $"{prefix}{amoint}";
        }
    }
}
