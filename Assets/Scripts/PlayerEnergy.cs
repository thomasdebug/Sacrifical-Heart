using UnityEngine;
using TMPro;

public class PlayerEnergy : MonoBehaviour
{
    public int maxEnergy = 3;
    public int currentEnergy;

    public TMP_Text energyText;

    private void Start()
    {
        currentEnergy = maxEnergy;
        UpdateEnergyUI();
    }

    public bool UseEnergy(int amount)
    {
        if (currentEnergy < amount)
        {
            Debug.Log("Not enough energy!");
            return false;
        }

        currentEnergy -= amount;
        UpdateEnergyUI();

        return true;
    }

    public void ResetEnergy()
    {
        currentEnergy = maxEnergy;
        UpdateEnergyUI();
    }

    public void UpdateEnergyUI()
    {
        energyText.text = "" + currentEnergy + "/" + maxEnergy;
    }
}
