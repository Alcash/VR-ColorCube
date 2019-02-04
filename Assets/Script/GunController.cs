using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : UsableItem {


    float shootSpeed = 2f;
    float cooldownShootTime = 0;

    float shellSpeed = 50;

    bool canShoot = true;
    public GameObject ShellPrefab;
    public Transform socketShell;

    public Color[] SignalColor = new Color[4] { Color.white, Color.red, Color.green, Color.black };
    


    Material gunMaterial;

    public EffectType shellType { get; protected set; }

    private void Start()
    {
        gunMaterial = transform.GetChild(0).GetComponent<Renderer>().sharedMaterial;
    }

    private void FixedUpdate()
    {
        CoolDown(Time.fixedDeltaTime);
    }

    void CoolDown(float deltaTime)
    {
        if(cooldownShootTime > 0)
        {
            cooldownShootTime -= deltaTime;
        }
        else
        {
            canShoot = true;
        }
    }

    public override void TouchPad(Vector2 touch, bool touchPress)
    {
        if (touchPress)
        {
            if (touch.x > 0.5f)
            {
                shellType = EffectType.White;

            }
            else if (touch.x < -0.5f)
            {
                shellType = EffectType.Black;
            }
            else if (touch.y > 0.5f)
            {
                shellType = EffectType.Red;
            }
            else if (touch.y < -0.5f)
            {
                shellType = EffectType.Green;
            }

            gunMaterial.color = SignalColor[(int)shellType];
        }
    }

    public override void UseItem()
    {
        base.UseItem();
        Shoot();
    }

    void Shoot()
    {
        if (canShoot)
        {
            canShoot = false;
            cooldownShootTime =1 / shootSpeed;
            var shell = Instantiate(ShellPrefab, socketShell.position, socketShell.rotation);

            shell.GetComponent<Rigidbody>().velocity = shell.transform.forward * shellSpeed;

            shell.GetComponent<ShellController>().effectcore.m_EffectType = shellType;
        }
    }
}
