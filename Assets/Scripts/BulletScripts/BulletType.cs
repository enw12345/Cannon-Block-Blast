using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Bullet Object", menuName = "Bullet Type")]
public class BulletType : ScriptableObject
{
    [Header("Bullet Prefab")]
    public GameObject bulletPrefab;

    [Header("Bullet UI Data")]
    public Sprite BulletUI;
    public int BulletSelectionNumber;

    [Header("Bullet Data")]
    public int AmmoCount;
    public int DefaultAmmoCount;
    public int destroyAmount = 5;
    public string bulletName;
}
