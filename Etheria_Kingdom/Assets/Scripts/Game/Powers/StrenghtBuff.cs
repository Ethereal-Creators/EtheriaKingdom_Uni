using UnityEngine;

[CreateAssetMenu(menuName = "Powerup/StrengthBuff")]
public class StrengthBuffEffect : PowerUpEffect
{
    public int damageBuffAmount = 5;
    public GameObject animationPrefab;
    public Vector3 animationOffset = new Vector3(0f, 1f, 0f);
    public float animationDuration = 2f;
    public float buffDuration = 10f;

    private int originalMinDamage;
    private int originalMaxDamage;
    private float buffTimer;

    public override void Apply(GameObject target)
    {
        Debug.Log("Applying Strength Buff to: " + target.name);

        PlayerShoot playerShoot = target.GetComponent<PlayerShoot>();
        if (playerShoot != null)
        {
            Bullet playerBullet = playerShoot.GetComponentInChildren<Bullet>();
            if (playerBullet != null)
            {
                originalMinDamage = playerBullet.minDamage;
                originalMaxDamage = playerBullet.maxDamage;

                playerBullet.SetDamage(originalMinDamage + damageBuffAmount, originalMaxDamage + damageBuffAmount);
                Debug.Log("Strength buff applied: " + damageBuffAmount + " damage added. New damage range: " +
                          playerBullet.minDamage + " - " + playerBullet.maxDamage);

                ApplyAnimation(target);

                buffTimer = buffDuration;
                Debug.Log("Buff duration started: " + buffDuration + " seconds.");
            }
            else
            {
                Debug.LogWarning("No Bullet component found on player's shooting mechanism.");
            }
        }
        else
        {
            Debug.LogWarning("PlayerShoot component not found on target.");
        }
    }

    private void ApplyAnimation(GameObject target)
    {
        Debug.Log("Attempting to instantiate animation effect...");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && animationPrefab != null)
        {
            Vector3 animationPosition = player.transform.position + animationOffset;
            GameObject animationInstance = Instantiate(animationPrefab, animationPosition, Quaternion.identity);
            animationInstance.transform.SetParent(player.transform);
            animationInstance.transform.localPosition = new Vector3(0, 1, 0);
            Destroy(animationInstance, animationDuration);
            Debug.Log("Animation instantiated at: " + animationPosition);
        }
        else
        {
            Debug.LogWarning("No animation prefab assigned or player not found.");
        }
    }

    private void Update()
    {
        if (buffTimer > 0)
        {
            buffTimer -= Time.deltaTime;
            Debug.Log("Buff timer counting down: " + buffTimer.ToString("F2") + " seconds remaining.");

            if (buffTimer <= 0)
            {
                Debug.Log("Buff timer expired. Reverting damage...");
                RevertDamage();
            }
        }
    }

    private void RevertDamage()
    {
        Debug.Log("Reverting Strength Buff...");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Bullet playerBullet = player.GetComponentInChildren<Bullet>();
            if (playerBullet != null)
            {
                playerBullet.SetDamage(originalMinDamage, originalMaxDamage);
                Debug.Log("Strength buff removed. Damage reverted to: " +
                          originalMinDamage + " - " + originalMaxDamage);
            }
            else
            {
                Debug.LogWarning("No Bullet component found while trying to revert damage.");
            }
        }
        else
        {
            Debug.LogWarning("Player object not found while trying to revert damage.");
        }
    }
}
