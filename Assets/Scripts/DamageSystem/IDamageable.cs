namespace DamageSystem
{
    public interface IDamageable
    {
        public DamageType[] DamageResistances { get;}
        public DamageType[] DamageImmunities { get;}
        
        public void ApplyDamage(IDamage damage);
    }
}