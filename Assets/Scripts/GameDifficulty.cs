/*
 *	Utility class for game difficulty settings
 *	Here are stored all the values related to turns times and maximum damage,
 *	the functions are triggered when changing difficulty level in the game settings
 */

public class GameDifficulty {
	public int difficulty = 0;
	
	public float TimeEnemyTurn = 30.0f;
	public float TimePlayerTurn = 30.0f;
	public float TimeBeforeSpawn = 2.0f;
	public float TimeBetweenSpawns = 5.0f;

	public float MaxTurnDamage = 20.0f;

	public void SetOnEasy() {
		difficulty = 0;
		TimeEnemyTurn = 30.0f;
		TimePlayerTurn = 30.0f;
		TimeBeforeSpawn = 2.0f;
		TimeBetweenSpawns = 5.0f;
		MaxTurnDamage = 20.0f;
	}

	public void SetOnNormal() {
		difficulty = 1;
		TimeEnemyTurn = 40.0f;
        TimePlayerTurn = 20.0f;
        TimeBeforeSpawn = 2.0f;
        TimeBetweenSpawns = 4.0f;
        MaxTurnDamage = 15.0f;
	}

	public void SetOnHard() {
		difficulty = 2;
		TimeEnemyTurn = 50.0f;
		TimePlayerTurn = 10.0f;
		TimeBeforeSpawn = 1.5f;
		TimeBetweenSpawns = 3.0f;
		MaxTurnDamage = 10.0f;
	}
}