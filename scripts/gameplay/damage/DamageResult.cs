public readonly struct DamageResult
{
    private DamageResult(bool applied, string targetName, int appliedAmount, int remainingHealth, int maxHealth)
    {
        Applied = applied;
        TargetName = targetName;
        AppliedAmount = appliedAmount;
        RemainingHealth = remainingHealth;
        MaxHealth = maxHealth;
    }

    public bool Applied { get; }
    public string TargetName { get; }
    public int AppliedAmount { get; }
    public int RemainingHealth { get; }
    public int MaxHealth { get; }

    public static DamageResult AppliedTo(string targetName, int appliedAmount, int remainingHealth, int maxHealth)
    {
        return new DamageResult(true, targetName, appliedAmount, remainingHealth, maxHealth);
    }

    public static DamageResult NotApplied(string targetName, int remainingHealth, int maxHealth)
    {
        return new DamageResult(false, targetName, 0, remainingHealth, maxHealth);
    }
}
