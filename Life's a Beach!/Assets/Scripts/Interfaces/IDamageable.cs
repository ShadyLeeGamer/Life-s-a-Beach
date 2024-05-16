using System;
public interface IDamageable
{
    public float Hp { get; set; }
    public event Action OnKill;
    void TakeDamage(float amount);
    void Kill();
}