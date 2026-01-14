using UnityEngine;
using System.Collections.Generic;

public class SkillLoadout : MonoBehaviour
{
    public MonoBehaviour activeSkill;
    public List<MonoBehaviour> supportSkills = new List<MonoBehaviour>(3);
    public MonoBehaviour passiveSkill;

    public void EquipActive(MonoBehaviour skill)
    {
        if (activeSkill != null)
            Destroy(activeSkill);

        activeSkill = skill;
    }

    public void EquipSupport(MonoBehaviour support)
    {
        if (supportSkills.Count >= 3)
            return;

        supportSkills.Add(support);
    }

    public void EquipPassive(MonoBehaviour passive)
    {
        if (passiveSkill != null)
            Destroy(passiveSkill);

        passiveSkill = passive;
    }
}
