public static class DamageResolver
{
    public static DamageResult Resolve(DamageRequest request)
    {
        if (request.Target is not IDamageable damageable)
        {
            CombatDebugLog.Write($"{request.AbilityName} hit {request.Target.Name}, but it cannot take damage.");
            return DamageResult.NotApplied(request.Target.Name.ToString(), 0, 0);
        }

        DamageResult result = damageable.ApplyDamage(request);

        if (!result.Applied)
        {
            CombatDebugLog.Write($"{request.AbilityName} dealt no damage to {result.TargetName}.");
            return result;
        }

        CombatDebugLog.Write($"{request.AbilityName} dealt {result.AppliedAmount} damage to {result.TargetName}.");
        CombatDebugLog.Write($"{result.TargetName} health remaining: {result.RemainingHealth}/{result.MaxHealth}.");

        return result;
    }
}
