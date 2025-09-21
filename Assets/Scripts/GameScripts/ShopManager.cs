using UnityEngine;
using UnityEngine.UI; // For the Image and Button components
using TMPro; // For the text

// This class holds all the info for a single shop item.
// The [System.Serializable] attribute lets you edit this in the Inspector.
[System.Serializable]
public class ShopItem
{
    public string skinName;
    public Sprite skinSprite;
    public int price;
    public bool isUnlocked = false;
}
public class ShopManager : MonoBehaviour
{
    // Drag your item slot prefab here
    public GameObject itemSlotPrefab;

    // Drag the parent object of your scroll view here
    public Transform contentParent;

    // This is a list of all your shop items.
    // You'll fill this in the Inspector.
    public ShopItem[] shopItems;

    [SerializeField] private HomeController homeController;
    [SerializeField] private TextMeshProUGUI noofcoinsText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (noofcoinsText != null)
        {
            noofcoinsText.text = PlayerPrefs.GetInt("TotalCoins", 0).ToString();
        }
        PopulateShop();
        LoadUnlockedItems();
    }
    void PopulateShop()
    {
        // Loop through every item in our list
        for (int i = 0; i < shopItems.Length; i++)
        {
            // Create a new item slot from our prefab
            GameObject newSlot = Instantiate(itemSlotPrefab, contentParent);

            // Get the components of the new slot
            // You'll need to name these in your prefab to match
            Image skinImage = newSlot.transform.Find("SkinImage").GetComponent<Image>();
            TextMeshProUGUI nameText = newSlot.transform.Find("NameText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI priceText = newSlot.transform.Find("PriceText").GetComponent<TextMeshProUGUI>();
            Button buyButton = newSlot.transform.Find("BuyButton").GetComponent<Button>();

            // Now, set the values based on the data in our list
            skinImage.sprite = shopItems[i].skinSprite;
            nameText.text = shopItems[i].skinName;
            priceText.text = shopItems[i].price.ToString();

            // This is the CRITICAL change:
            // Create a local variable that captures the value of 'i' for this iteration.
            int itemIndex = i;
            buyButton.onClick.AddListener(() => PurchaseItem(itemIndex));
        }
    }

    // A placeholder method for now, we'll fill this out later.
    // Inside your ShopManager class

    public void PurchaseItem(int itemIndex)
    {
        Debug.Log("Item Index" + itemIndex);
        ShopItem itemToBuy = shopItems[itemIndex];

        // 1. Check if the item is already unlocked.
      /*  if (itemToBuy.isUnlocked)
        {
            Debug.Log(itemToBuy.skinName + " is already unlocked. Activating skin.");
            // We'll call the activation method here.
            ActivateSkin(itemIndex);
            return;
        }*/

        // 2. Check if the player has enough coins.
        int currentCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        if (currentCoins >= itemToBuy.price)
        {
            // 3. Subtract the coins and save.
            currentCoins -= itemToBuy.price;
            PlayerPrefs.SetInt("TotalCoins", currentCoins);
            noofcoinsText.text = currentCoins.ToString();

            // Update the coin display in the shop UI
            homeController.UpdateCoinDisplay(); // You'll need to create this method

            // 4. Mark the item as unlocked and save.
            itemToBuy.isUnlocked = true;
            SaveUnlockedItems(); // We'll create this method too

            Debug.Log("Purchase successful! " + itemToBuy.skinName + " unlocked.");
            // Update the UI button to say "Equipped" or "Owned"

            // Update the UI button for this specific item slot.
            // This is the new part.
            GameObject slotToUpdate = contentParent.GetChild(itemIndex).gameObject;
            UpdateItemSlotUI(slotToUpdate, itemIndex);
        }
        else
        {
            Debug.Log("Not enough coins to purchase " + itemToBuy.skinName);
            // You can add a pop-up here saying "Not enough coins"
        }
    }
    // Inside your ShopManager class

    // Call this in the Start() method to load the data
    public void LoadUnlockedItems()
    {
        string unlockedString = PlayerPrefs.GetString("UnlockedSkins", "");
        string[] unlockedList = unlockedString.Split(',');

        foreach (string skinName in unlockedList)
        {
            foreach (ShopItem item in shopItems)
            {
                if (item.skinName == skinName)
                {
                    item.isUnlocked = true;
                    break;
                }
            }
        }
    }

    // Call this whenever an item is purchased
    public void SaveUnlockedItems()
    {
        string unlockedString = "";
        foreach (ShopItem item in shopItems)
        {
            if (item.isUnlocked)
            {
                unlockedString += item.skinName + ",";
            }
        }
        PlayerPrefs.SetString("UnlockedSkins", unlockedString);
    }
    // Inside your ShopManager script

    void UpdateItemSlotUI(GameObject slot, int itemIndex)
    {
        // Find the button and text within the slot
        Button button = slot.GetComponentInChildren<Button>();
        TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();

        // Find the price text to hide it
        TextMeshProUGUI priceText = slot.transform.Find("PriceText").GetComponent<TextMeshProUGUI>();

        // If the item is unlocked, change the button
        if (shopItems[itemIndex].isUnlocked)
        {
            // Change the button text
            buttonText.text = "ACTIVATE";

            // Re-assign the button's OnClick event
            // This is important to change what the button does.
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => ActivateSkin(slot ,itemIndex));

            // Hide the price
            if (priceText != null)
            {
                priceText.gameObject.SetActive(false);
            }
        }
    }
    // Inside your ShopManager script

    public void ActivateSkin(GameObject slot,int itemIndex)
    {
        // Find the button and text within the slot
        Button button = slot.GetComponentInChildren<Button>();
        TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
        button.onClick.RemoveAllListeners();
        buttonText.text = "ACTIVATED";
        ShopItem itemToActivate = shopItems[itemIndex];
        // Save the name of the activated skin using PlayerPrefs.
        PlayerPrefs.SetString("ActiveSkin", itemToActivate.skinName);
       // Debug.Log("Active skin set to: " + itemToActivate.skinName);
    }
}
