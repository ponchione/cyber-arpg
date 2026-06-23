using Godot;

public readonly struct DamageRequest
{
    public DamageRequest(Node source, Node target, int amount, string abilityName)
    {
        Source = source;
        Target = target;
        Amount = amount;
        AbilityName = abilityName;
    }

    public Node Source { get; }
    public Node Target { get; }
    public int Amount { get; }
    public string AbilityName { get; }
}
