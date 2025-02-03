using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemySpawner : MonoBehaviour
{
    public static event Action<EnemySpawner> OnEnemyKilled;
    
    [SerializeField] float health, maxHealth = 2f;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] GameObject enemyPrefab; // Préfabriqué de l'ennemi
    [SerializeField] float spawnInterval = 2f; // Temps entre chaque spawn d'ennemi
    
    Rigidbody2D rb;
    Transform target;
    Vector2 moveDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); // Récupère le Rigidbody2D
    }

    private void Start()
    {
        health = maxHealth;
        target = GameObject.Find("Player").transform;
        
        // Démarre la coroutine de spawn des ennemis à un intervalle fixe
        StartCoroutine(SpawnEnemies());
    }

    private void Update()
    {
        if (target)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle; // Mise à jour de la rotation
            moveDirection = direction;
        }
    }

    private void FixedUpdate()
    {
        if (target)
        {
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed;
        }
    }

    // Coroutine qui spawn des ennemis à l'extérieur de l'écran à intervalles réguliers
    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval); // Attend avant de spawn un nouvel ennemi
        }
    }

    // Fonction pour spawn un ennemi à une position aléatoire à l'extérieur de l'écran
    private void SpawnEnemy()
    {
        // Calculer une position aléatoire hors écran
        float screenWidth = Camera.main.orthographicSize * Camera.main.aspect; // Largeur de l'écran en unités
        float screenHeight = Camera.main.orthographicSize; // Hauteur de l'écran en unités

     Vector2 spawnPosition = Vector2.zero;
if (UnityEngine.Random.Range(0, 2) == 0)
{
    spawnPosition.x = UnityEngine.Random.Range(-screenWidth - 1f, screenWidth + 1f);
    spawnPosition.y = UnityEngine.Random.Range(-screenHeight - 1f, screenHeight + 1f);
}
else
{
    spawnPosition.x = UnityEngine.Random.Range(-screenWidth - 1f, screenWidth + 1f);
    spawnPosition.y = UnityEngine.Random.Range(-screenHeight - 1f, screenHeight + 1f);
}
        
        // Créer l'ennemi à la position calculée
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    // Méthode pour gérer les dégâts et détruire l'ennemi
    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            // Notifie qu'un ennemi a été tué
            OnEnemyKilled?.Invoke(this);
            Destroy(gameObject); // Détruire l'ennemi
        }
    }
}