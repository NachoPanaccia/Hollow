public interface IStatsProvider
{
    int CurrentHealth { get; }
    int MaxHealth { get; }

    void Heal(int amount);
}