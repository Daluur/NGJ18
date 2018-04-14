public interface IEnemy {
	void TakeDamage(int amount, IPlayer player);
	bool IsBoss();
}
