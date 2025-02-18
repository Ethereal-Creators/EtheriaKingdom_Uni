using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // If you are using TMP_Text for better text management

public class EnemyScoreAllocator : MonoBehaviour
{
    [SerializeField]
    private int _killScore;

    [SerializeField]
    private GameObject _scoreTextPrefab; // Prefab to show score above the enemy
    [SerializeField]
    private float _scoreDisplayTime = 1.5f; // How long the score will be visible
    [SerializeField]
    private float _scoreMoveSpeed = 1f; // Speed at which the score moves up

    private ScoreController _scoreController;

    private void Awake()
    {
        _scoreController = FindObjectOfType<ScoreController>();
    }

    public void AllocateScore()
    {
        // Add the score to the controller
        _scoreController.AddScore(_killScore);

        // Show the score above the enemy
        ShowScoreAboveEnemy();
    }

    private void ShowScoreAboveEnemy()
    {
        // Instantiate the score text prefab above the enemy's position
        if (_scoreTextPrefab != null)
        {
            GameObject scoreText = Instantiate(_scoreTextPrefab, transform.position, Quaternion.identity);
            TMP_Text scoreTextComponent = scoreText.GetComponent<TMP_Text>();

            if (scoreTextComponent != null)
            {
                scoreTextComponent.text = _killScore.ToString(); // Set the score text
            }

            // Start the floating and fading animation
            StartCoroutine(FloatAndFadeScore(scoreText));
        }
    }

    private IEnumerator FloatAndFadeScore(GameObject scoreText)
    {
        float elapsedTime = 0f;

        Vector3 startPosition = scoreText.transform.position;
        Vector3 targetPosition = startPosition + Vector3.up * 1.5f; // Move upwards by 1.5 units

        Color startColor = scoreText.GetComponent<TMP_Text>().color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 0); // Fade to transparent

        TMP_Text scoreTextComponent = scoreText.GetComponent<TMP_Text>();

        while (elapsedTime < _scoreDisplayTime)
        {
            // Move the score up smoothly
            scoreText.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / _scoreDisplayTime);

            // Fade the score text smoothly
            scoreTextComponent.color = Color.Lerp(startColor, targetColor, elapsedTime / _scoreDisplayTime);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Destroy the score text object after it finishes floating and fading
        Destroy(scoreText);
    }
}
