namespace DamageSystem
{
    public class Projectile : Hitbox
    {
        public override void CollideWith(IDamageable damageable)
        {
            base.CollideWith(damageable);
            Destroy(gameObject);
        }
    }
}