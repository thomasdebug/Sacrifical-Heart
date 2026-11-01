using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public DeathScreen deathScreen;

    private int enemiesAlive = 0;

    public void RegisterEnemy()
    {
        enemiesAlive++;
    }

    public void EnemyDefeated()
    {
        enemiesAlive--;

        if (enemiesAlive <= 0)
        {
            deathScreen.ShowVictoryScreen();
        }
    }
}

