using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class WeaponsControl : MonoBehaviour
{
    public float HP_Player;

    public Text TextBullets, TextHpPlayer;
    public Image IconAim;
    public Color ColorDefaultAim, ColorFireAim;
    public Transform PointRayCam;
    public float DistanceRay;
    public List<string> TagsEnemy;

    [HideInInspector] public bool Walk, Shot, Reload;
    private float TimerShot;

    [System.Serializable]
    public class Weapon
    {
        public string NameWeapon;
        public Animator AnimatorWapon;
        public GameObject ObjectWeapon;
        public Transform PointParticleSpawn;
        public GameObject[] ParticlePrefab;
        public int Bullets_InStore, All_Bullets;
        public float DelayShot;
        public AudioClip ReloadSound;
        public AudioSource audioSourceGun;
        public float MinDamage, MaxDamage;
    }

    public Weapon[] Weapons;

    public Weapon CurrentWeapon;

    RaycastHit hit;

    private void Awake()
    {
        CurrentWeapon = Weapons[0];
        TextHpPlayer.text = HP_Player.ToString();
    }

    public void DamagePlayer (float _damage)
    {
        HP_Player -= _damage;
        if(HP_Player < 0)
        {
            HP_Player = 0;
        }
        TextHpPlayer.text = HP_Player.ToString();

    }

    private void Update()
    {
        if (CurrentWeapon.Bullets_InStore > 0)
        {
            if(Physics.Raycast(PointRayCam.position, PointRayCam.forward, out hit, DistanceRay))
            {
                if(hit.collider)
                {
                    if(TagsEnemy.Contains(hit.collider.tag))
                    {
                        IconAim.color = ColorFireAim;
                        Shot = true;
                        TimerShot += Time.deltaTime;
                        if(TimerShot >= CurrentWeapon.DelayShot)
                        {
                            Instantiate(CurrentWeapon.ParticlePrefab[Random.Range(0, CurrentWeapon.ParticlePrefab.Length)], CurrentWeapon.PointParticleSpawn.position, CurrentWeapon.PointParticleSpawn.rotation);
                            if(hit.collider.GetComponent<ZombieControl>() != null)
                                hit.collider.GetComponent<ZombieControl>().DamageZombie(Random.Range(CurrentWeapon.MinDamage, CurrentWeapon.MaxDamage));
                            CurrentWeapon.Bullets_InStore--;
                            TimerShot = 0;
                        }
                    }
                    else
                    {
                        IconAim.color = ColorDefaultAim;
                        Shot = false;
                       // TimerShot = CurrentWeapon.DelayShot;
                    }
                }
                else
                {
                    IconAim.color = ColorDefaultAim;
                    Shot = false;
                   //TimerShot = CurrentWeapon.DelayShot;
                }
            }
            else
            {
                IconAim.color = ColorDefaultAim;
                Shot = false;
               // TimerShot = CurrentWeapon.DelayShot;
            }
        }
        else if (CurrentWeapon.Bullets_InStore <= 0)
        {
            IconAim.color = ColorDefaultAim;
            Shot = false;
            //TimerShot = CurrentWeapon.DelayShot;
            if (CurrentWeapon.All_Bullets > 0)
            {
                if(!Reload)
                {
                    if(!CurrentWeapon.audioSourceGun.isPlaying)
                    {
                        CurrentWeapon.audioSourceGun.clip = CurrentWeapon.ReloadSound;
                        CurrentWeapon.audioSourceGun.Play();
                    }
                }
                Reload = true;
            }
        }

        TextBullets.text = CurrentWeapon.Bullets_InStore + "/" + CurrentWeapon.All_Bullets;
    }

    private void FixedUpdate()
    {
        CurrentWeapon.AnimatorWapon.SetBool("Walk", Walk);
        CurrentWeapon.AnimatorWapon.SetBool("Shot", Shot);
        CurrentWeapon.AnimatorWapon.SetBool("Reload", Reload);


    }

    public void OnEndReload()
    {
        CurrentWeapon.Bullets_InStore = 30;
        CurrentWeapon.All_Bullets -= 30;
        Reload = false;
    }
}
