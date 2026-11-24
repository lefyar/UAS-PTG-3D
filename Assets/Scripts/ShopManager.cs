using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;

    [System.Serializable]
    public class CarItem
    {
        public string carName;
        public int price;
        public GameObject carModel;
        public bool isUnlocked;
    }

    public CarItem[] carList;

    [Header("UI References")]
    public TMP_Text coinsText;
    public TMP_Text carNameText;
    public TMP_Text priceText;
    public Button actionButton;
    public TMP_Text actionBtnText;

    private int currentCarIndex = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Start()
    {
        for (int i = 0; i < carList.Length; i++)
        {
            if (PlayerPrefs.GetInt($"Car_{i}_Unlocked", 0) == 1)
            {
                carList[i].isUnlocked = true;
            }
        }

        currentCarIndex = PlayerPrefs.GetInt("SelectedCarIndex", 0);

        UpdateUI();
    }

    public void NextCar()
    {
        carList[currentCarIndex].carModel.SetActive(false);

        currentCarIndex++;
        if (currentCarIndex >= carList.Length) currentCarIndex = 0;

        UpdateUI();
    }

    public void PreviousCar()
    {
        carList[currentCarIndex].carModel.SetActive(false);

        currentCarIndex--;
        if (currentCarIndex < 0) currentCarIndex = carList.Length - 1;

        UpdateUI();
    }

    public void OnActionButtonClick()
    {
        CarItem car = carList[currentCarIndex];

        if (car.isUnlocked)
        {
            PlayerPrefs.SetInt("SelectedCarIndex", currentCarIndex);
            PlayerPrefs.Save();
            UpdateUI();
        }
        else
        {
            int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);

            if (totalCoins >= car.price)
            {
                totalCoins -= car.price;
                PlayerPrefs.SetInt("TotalCoins", totalCoins);

                PlayerPrefs.SetInt($"Car_{currentCarIndex}_Unlocked", 1);
                car.isUnlocked = true;

                PlayerPrefs.SetInt("SelectedCarIndex", currentCarIndex);
                PlayerPrefs.Save();

                UpdateUI();
            }
            else
            {
                Debug.Log("Uang tidak cukup!");
            }
        }
    }

    public void UpdateUI()
    {
        int coins = PlayerPrefs.GetInt("TotalCoins", 0);
        if (coinsText != null) coinsText.text = "Coins: " + coins.ToString();

        for (int i = 0; i < carList.Length; i++)
        {
            if (i == currentCarIndex)
            {
                carList[i].carModel.SetActive(true);
            }
            else
            {
                carList[i].carModel.SetActive(false);
            }
        }

        CarItem car = carList[currentCarIndex];
        if (carNameText != null) carNameText.text = car.carName;

        int selectedIndex = PlayerPrefs.GetInt("SelectedCarIndex", 0);

        if (car.isUnlocked)
        {
            if (priceText != null) priceText.text = "Owned";

            if (currentCarIndex == selectedIndex)
            {
                actionBtnText.text = "Selected";
                actionButton.interactable = false;
            }
            else
            {
                actionBtnText.text = "Select";
                actionButton.interactable = true;
            }
        }
        else
        {
            actionBtnText.text = "Buy";
            if (priceText != null) priceText.text = car.price.ToString();

            actionButton.interactable = (coins >= car.price);
        }
    }
}