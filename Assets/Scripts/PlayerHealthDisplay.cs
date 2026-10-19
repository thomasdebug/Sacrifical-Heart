using UnityEngine;
using TMPro;

public class PlayerHealthDisplay : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public TMP_Text healthText;

    private void Update()
    {
        if (playerHealth == null || healthText == null)
            return;

        healthText.text =
            playerHealth.currentHealth + " / " +
            playerHealth.maxHealth + " HP";
    }
}