using UnityEngine;
using UnityEngine.VFX;

public class CombatVFX : MonoBehaviour
{
    public GameObject blockEffect;
    public GameObject impactEffect;
    public GameObject blockImpactEffect;
    public GameObject playerImpactEffect;
    public GameObject playerDeathEffect;

    public void PlayEffect(GameObject effect, Vector3 position)
    {
        if (effect == null)
        {
            Debug.LogWarning("VFX prefab is not assigned!");
            return;
        }

        GameObject instance = Instantiate(
            effect,
            position,
            Quaternion.identity
        );

        VisualEffect vfx = instance.GetComponentInChildren<VisualEffect>();

        if (vfx != null)
        {
            vfx.Reinit();
            vfx.Play();
            Debug.Log("Playing VFX: " + effect.name);
        }
        else
        {
            Debug.LogWarning(
                effect.name + " has no VisualEffect component!"
            );
        }

        Destroy(instance, 5f);
    }
}


