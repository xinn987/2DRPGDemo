using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneSkill : Skill
{
    [SerializeField] private GameObject clonePrefab;
    [SerializeField] private float cloneDuration;
    [SerializeField] private float cloneFadeSpeed;

    public void Create(Transform spawnPoint)
    {
        GameObject clone = Instantiate(clonePrefab);
        clone.GetComponent<CloneSkillController>().Setup(spawnPoint, cloneDuration, cloneFadeSpeed);
    }
}
