using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpecialSkill
{
    void Setup();

    void Action();

}
public enum SpecalSkillDef
{
    Invalid = 0,
    ReflectedLaser = 1,
}