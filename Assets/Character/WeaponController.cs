using Character.Events;
using Items.Equipment.Weapons;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private GameObject _instantiatedWeapon;
    public Weapon Weapon;

    private void Start()
    {
        SwitchWeapon(new Longsword());
    }

    private void Attack()
    {
        new CharacterAttackedEvent(Weapon.Damage).Fire();
    }

    private void SwitchWeapon(Weapon newWeapon)
    {
        Destroy(_instantiatedWeapon);
        Weapon = newWeapon;
        _instantiatedWeapon = Instantiate(Weapon.EquippableGameObject, transform);
    }
}
