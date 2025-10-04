using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Core;
using Unity.Services.Leaderboards;
using Unity.Services.Leaderboards.Models;
using UnityEngine;
using Unity.Services.Authentication; // Make sure to add this!

public class LeaderboardManager : MonoBehaviour
{
    private const string LeaderboardId = "Jumping_Bird_LeaderBoard";
    private bool isInitialized = false;

    // This method will initialize the services
    private async void Start()
    {
        await InitializeAsync();
    }

    private async Task InitializeAsync()
    {
        try
        {
            await UnityServices.InitializeAsync();

            // Authenticate the player anonymously
            if (!AuthenticationService.Instance.IsSignedIn)
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            }
            isInitialized = true;
            Debug.Log("Unity Services Initialized and Player Authenticated.");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Initialization failed: {e.Message}");
        }
    }

    // Call this method to submit a score
    public async void SubmitScoreAsync(long score)
    {
        if (!isInitialized)
        {
            Debug.LogError("Leaderboards service is not initialized. Cannot submit score.");
            return;
        }

        try
        {
            var result = await LeaderboardsService.Instance.AddPlayerScoreAsync(LeaderboardId, score);
            Debug.Log($"Score submitted to leaderboard: {result.Score}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to submit score: {e.Message}");
        }
    }

    // Call this method to get and display the top scores
    public async Task<List<LeaderboardEntry>> GetScoresAsync()
    {
        if (!isInitialized)
        {
            Debug.LogError("Leaderboards service is not initialized. Cannot retrieve scores.");
            return null;
        }

        try
        {
            var scores = await LeaderboardsService.Instance.GetScoresAsync(LeaderboardId);
            return scores.Results;
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to retrieve scores: {e.Message}");
            return null;
        }
    }
}