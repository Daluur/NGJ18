public interface IPlayer {
	void TakeDamage(int amount);
	void TakeDamage(int amount, IPlayer player);
    void RewardSanity(int amount);
}
