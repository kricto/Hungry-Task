using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAim : MonoBehaviour
{
    [SerializeField] private Transform gunHolder;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePosition;

    [SerializeField] private List<WeaponStructure> weaponList = new List<WeaponStructure>();
    private WeaponType weaponIndex = WeaponType.Light;

    private float lightBulletForce = 8f;
    private float rapidBulletForce = 15f;
    private float splashBulletForce = 10f;

    private bool lightShootCooldownFlag = false;
    private bool rapidShootCooldownFlag = false;
    private bool splashShootCooldownFlag = false;

    private float lightShootCooldown = .5f;
    private float rapidShootCooldown = .2f;
    private float splashShootCooldown = 1.2f;


    private Vector3 MousePosition => GetMousePosition();

    public enum WeaponType
    {
        Light,
        Rapid,
        Splash,
    }

    [System.Serializable]
    public struct WeaponStructure
    {
        public WeaponType name;
        public GameObject weaponObject;
    }

    private void Update()
    {
        HandleAiming();
        HandleShooting();
        SwitchWeapon();
    }

    private void HandleAiming()
    {
        Vector3 aimDirection = (MousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        gunHolder.eulerAngles = new Vector3(0f, 0f, angle);
    }
    
    private void HandleShooting()
    {
        switch (weaponIndex)
        {
            case WeaponType.Light:
                Shoot(ref lightShootCooldownFlag, lightBulletForce, nameof(LightShootCooldown), lightShootCooldown);
                break;
            case WeaponType.Rapid:
                Shoot(ref rapidShootCooldownFlag, rapidBulletForce, nameof(RapidShootCooldown), rapidShootCooldown);
                break;
            case WeaponType.Splash:
                Shoot(ref splashShootCooldownFlag, splashBulletForce, nameof(SplashShootCooldown), splashShootCooldown);
                break;
        }
    }

    private void Shoot(ref bool cooldownType, float bulletForce, string shootCooldownName, float coolDown)
    {
        if (!cooldownType && Input.GetMouseButton(0))
        {
            if (weaponIndex == WeaponType.Splash)
            {
                GameObject bullet = Instantiate(bulletPrefab, firePosition.position, firePosition.rotation);
                Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
                bulletRigidbody.AddForce(Quaternion.AngleAxis(-20, firePosition.forward) * firePosition.up * bulletForce, ForceMode2D.Impulse);

                bullet = Instantiate(bulletPrefab, firePosition.position, firePosition.rotation);
                bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
                bulletRigidbody.AddForce(firePosition.up * bulletForce, ForceMode2D.Impulse);

                bullet = Instantiate(bulletPrefab, firePosition.position, firePosition.rotation);
                bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
                bulletRigidbody.AddForce(Quaternion.AngleAxis(20, firePosition.forward) * firePosition.up * bulletForce, ForceMode2D.Impulse);
            }
            else
            {
                GameObject bullet = Instantiate(bulletPrefab, firePosition.position, firePosition.rotation);
                Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
                bulletRigidbody.AddForce(firePosition.up * bulletForce, ForceMode2D.Impulse);
            }
            cooldownType = true;

            Invoke(shootCooldownName, coolDown);
        }
    }

    private void SwitchWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weaponList.Find(weapon => weapon.name == weaponIndex).weaponObject.SetActive(false);
            weaponIndex = WeaponType.Light;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weaponList.Find(weapon => weapon.name == weaponIndex).weaponObject.SetActive(false);
            weaponIndex = WeaponType.Rapid;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            weaponList.Find(weapon => weapon.name == weaponIndex).weaponObject.SetActive(false);
            weaponIndex = WeaponType.Splash;
        }

        weaponList.Find(weapon => weapon.name == weaponIndex).weaponObject.SetActive(true);
    }

    private void LightShootCooldown()
    {
        lightShootCooldownFlag = false;
    }

    private void RapidShootCooldown()
    {
        rapidShootCooldownFlag = false;
    }

    private void SplashShootCooldown()
    {
        splashShootCooldownFlag = false;
    }

    private Vector3 GetMousePosition()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = .0f;

        return mousePosition;
    }
}
