using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWeapon : MonoBehaviour
{
    [SerializeField] private GameObject[] _weapons;

    /// <summary>
    /// Перелопатить это скрипт к чертям собачьим
    /// </summary>

    private void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            _weapons[0].SetActive(true);
            _weapons[2].SetActive(false);
            _weapons[1].SetActive(false);
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            _weapons[0].SetActive(false);
            _weapons[2].SetActive(false);
            _weapons[1].SetActive(true);
        }

        if (Input.GetKey(KeyCode.G))
        {
            _weapons[0].SetActive(false);
            _weapons[1].SetActive(false);
            _weapons[2].SetActive(true);
        }
    }
}
