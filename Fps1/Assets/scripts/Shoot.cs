using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class Shoot : MonoBehaviour
{
    public float cooldown = 0.1f;
    float lastFireTime = 0;
    public int defaultAmmo = 120;
    public int magSize = 30;
    public int currentAmmo;
    public int currentMagAmmo;
    public Camera Camera;
    public int Range;
    [Header("Gun Damage On Hit")]
    public int Damage;
    public GameObject bloodPrefab;
    public GameObject decalPrefab;
    public GameObject magObject;
    public ParticleSystem muzzleParticle;
    float minAngle = -0.5f;
    float maxAngle = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = defaultAmmo-magSize;
        currentMagAmmo = magSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
        if (Input.GetMouseButton(0))
        {
            if (CanFire())
            {
                Fire();
            }
           
        }
    }

    private void Reload()
    {
        if (currentAmmo == 0 || currentMagAmmo==magSize)
        {
            return;
        }
        if (currentAmmo<magSize)
        {
            currentMagAmmo +=currentAmmo;
            currentAmmo = 0;
        }
        else
        {
            currentAmmo -= magSize - currentMagAmmo;
            currentMagAmmo = magSize;
        }
        GameObject newMagObject = Instantiate(magObject);
        newMagObject.transform.position = magObject.transform.position;
        newMagObject.AddComponent<Rigidbody>();
    }

    private bool CanFire()
    {
       
        if (currentMagAmmo > 0 && lastFireTime + cooldown< Time.time)
        {
           lastFireTime = Time.time + cooldown ;
           return true;
        }
        else
        {
           return false;
        }

   
    }

    private void Fire()
    {
        muzzleParticle.Play(true);//emit yerine bunu kullandýk childlarla beraber çalýþýyor bu komut
        currentMagAmmo -= 1;
        RaycastHit hit;
        if (Physics.Raycast(Camera.transform.position,Camera.transform.forward,out hit,Range))
        {
            if (hit.transform.tag=="Zombie")
            {
                hit.transform.GetComponent<ZombieHealth>().hit(Damage);
                GenerateBloodEffect(hit);
            }
            else
            {
                GenerateHitEffect(hit);
            }
        }
      transform.localEulerAngles = new Vector3(Random.Range(minAngle, maxAngle), Random.Range(minAngle, maxAngle), Random.Range(minAngle, maxAngle));//titreme kýsmý 
    
    }

    private void GenerateHitEffect(RaycastHit hit)
    {
        GameObject hitObject = Instantiate(decalPrefab, hit.point, Quaternion.identity);
        hitObject.transform.rotation = Quaternion.FromToRotation(decalPrefab.transform.forward*-1, hit.normal);
    }

    private void GenerateBloodEffect(RaycastHit hit)
    {
        GameObject bloodObject = Instantiate(bloodPrefab, hit.point, hit.transform.rotation);
       
    }
}
