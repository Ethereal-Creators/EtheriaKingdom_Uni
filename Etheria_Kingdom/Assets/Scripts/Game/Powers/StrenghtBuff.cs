using UnityEngine;

[CreateAssetMenu(menuName = "Powerup/StrengthBuff")]
public class StrengthBuffEffect : PowerUpEffect
{
    public int damageBuffAmount = 5; // How much damage is added to the player's bullet damage

    public override void Apply(GameObject target)
    {
        // Find the player's Bullet component to apply the damage buff
        Bullet bullet = target.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.minDamage += damageBuffAmount;
            bullet.maxDamage += damageBuffAmount;
        }
    }
}
