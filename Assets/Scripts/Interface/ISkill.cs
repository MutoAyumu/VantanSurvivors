using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// スキル用のインターフェース
/// </summary>
public interface ISkill
{
    SkillDef SkillId { get; }

    void Setup();
    void Update();
    void Levelup();

}
public enum SkillDef
{
    Invalid = 0,
    MagicBullet = 1,
    DamageArea = 2,
    ShotBullet = 3,
}
