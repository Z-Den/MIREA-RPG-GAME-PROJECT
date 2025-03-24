namespace Units.Health
{
    public class PlayerHealth : UnitHealth
    {
        protected override void Die()
        {
            OnDeath?.Invoke();
        }
    }
}