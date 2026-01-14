using UnityEngine;

public class SkillMenuUI : MonoBehaviour
{
    public GameObject player;

    public void EquipFireball()
    {
        var loadout = player.GetComponent<SkillLoadout>();
        loadout.EquipActive(player.AddComponent<Fireball>());
    }

    public void AddMoreProjectiles()
    {
        var loadout = player.GetComponent<SkillLoadout>();
        loadout.EquipSupport(player.AddComponent<MoreProjectiles>());
    }

    public void AddMoreDuration()
    {
        var loadout = player.GetComponent<SkillLoadout>();
        loadout.EquipSupport(player.AddComponent<MoreDuration>());
    }
}
