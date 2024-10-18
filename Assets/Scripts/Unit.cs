using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceEngineers.Enums;


public class Unit : MonoBehaviour
{
  public UnitType type;
  public Dictionary<UnitType, float> healModifiers = new Dictionary<UnitType, float>
  {
    { UnitType.red, 1f },
    { UnitType.green, 1f },
    { UnitType.blue, 1f }
  };

  private void Start()
  {
    healModifiers = new Dictionary<UnitType, float>
    {
      { UnitType.red, type == UnitType.red ? 2f : 1f },
      { UnitType.green, type == UnitType.green ? 2f : 1f },
      { UnitType.blue, type == UnitType.blue ? 2f : 1f }
    };
  }
}