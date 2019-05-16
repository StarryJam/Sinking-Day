using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretData{
    public TurretType type;
    public GameObject turretBased;
    public GameObject turretUpgraded;
    public int cost_build;
    public int cost_upgrade;
}
public enum TurretType
{
    StandTurret,
    MissileTurret,
    LaserTurret
}
