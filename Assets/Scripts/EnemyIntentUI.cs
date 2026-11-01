using UnityEngine;
using TMPro;

public class EnemyIntentUI : MonoBehaviour
{
    public TMP_Text intentText;

    public void UpdateIntent(int damage)
    {
        intentText.text = "" + damage;
    }
}

