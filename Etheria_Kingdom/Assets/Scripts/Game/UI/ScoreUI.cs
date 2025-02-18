using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    private TMP_Text _scoreText;
    private int _lastScore = 0; // Keep track of the last score to compare

    [SerializeField]
    private float _scoreUpdateSpeed = 0.1f; // Speed of score update animation (higher is faster)
    [SerializeField]
    private Color _increaseColor = Color.green; // Color for score increase
    [SerializeField]
    private Color _decreaseColor = Color.red; // Color for score decrease

    private void Awake()
    {
        _scoreText = GetComponent<TMP_Text>();
    }

    public void UpdateScore(ScoreController scoreController)
    {
        // Smooth score update
        int newScore = scoreController.Score;

        // If the score is different from the last score, update with animation
        if (newScore != _lastScore)
        {
            StartCoroutine(SmoothScoreUpdate(newScore));
            _lastScore = newScore; // Update the last score value
        }
    }

    private IEnumerator SmoothScoreUpdate(int newScore)
    {
        int currentScore = int.Parse(_scoreText.text.Split(':')[1].Trim()); // Get the current score value from the text
        int startScore = currentScore;

        // Determine if the score increased or decreased to change the text color
        bool isScoreIncrease = newScore > currentScore;

        // Change the text color temporarily when score changes
        _scoreText.color = isScoreIncrease ? _increaseColor : _decreaseColor;

        // Animate the score update
        float progress = 0f;
        while (progress < 1f)
        {
            progress += Time.deltaTime / _scoreUpdateSpeed; // Smooth the progress
            int animatedScore = Mathf.RoundToInt(Mathf.Lerp(startScore, newScore, progress));
            _scoreText.text = $"Score: {animatedScore}";

            yield return null;
        }

        // Finalize the score to avoid any floating point discrepancies
        _scoreText.text = $"Score: {newScore}";

        // Reset color back to normal after update
        _scoreText.color = Color.white; // Change back to white or default color
    }
}
