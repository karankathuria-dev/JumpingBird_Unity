using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    public int maxLives = 3;
    int currentLives;
    [SerializeField] GameObject livePanel;
    public GameManager gameManager;
    private bool isHit = false; // Prevent multiple triggers
    public Image[] hearts;
    public Transform birdTransform; // Assign bird's transform
    private Vector3 startPosition; // Store initial position
    void Start()
    {
        currentLives = maxLives;
        UpdateLiveUI();
        startPosition = birdTransform.position;
    }
   public void LooseLife()
    {
        if (!isHit && currentLives > 0) {
            isHit = true;
            currentLives--;
            UpdateLiveUI();
            StartCoroutine(ShowLivePanel());
           
        }
       
        if (currentLives <= 0)
        {
            if (!gameManager) return;
            livePanel.SetActive(false);
            ResetLife();
            gameManager.GameOver();
        }
    }

    private void UpdateLiveUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = i < currentLives;
        }
    }
    public void GainLives()
    {
        if (currentLives < maxLives)
        {
            currentLives++;
            UpdateLiveUI();
        }
    }
    IEnumerator ShowLivePanel()
    {
        livePanel.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        livePanel.SetActive(false);
        isHit = false;

    }
    public void ResetLife()
    {
        currentLives = maxLives;
    }
    public void GainLife()
    {
        if (currentLives < maxLives)
        {
            currentLives++;
            UpdateLiveUI();
            Debug.Log("Life Gained! Current lives: " + currentLives);
        }
    }

}
