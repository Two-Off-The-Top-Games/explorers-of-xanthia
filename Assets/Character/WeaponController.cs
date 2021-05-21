using Character.Events;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject StartingWeapon;
    private GameObject _CurrentWeapon;
    private Weapon _weapon;

    private void Start()
    {
        SwitchWeapon(StartingWeapon);
    }

    private void Attack() => new CharacterAttackedEvent(_weapon.Damage).Fire();

    private void SwitchWeapon(GameObject newWeapon)
    {
        _CurrentWeapon = newWeapon;
        _weapon = _CurrentWeapon.GetComponent<Weapon>();
    }
}
