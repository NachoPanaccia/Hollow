using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyFactory
{
    GameObject CreateBoss(Vector3 position, Quaternion rotation);
    GameObject CreateMinion(Vector3 position, Quaternion rotation);
}