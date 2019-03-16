using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalLevel1EnemySpawner : MonoBehaviour
{
    public static float widthOfLevel = 22f;
    public static float lengthOfLevel = 22f;
    static float widthDividedBy100;
    static float lengthDividedBy100;

    protected float cooldown1TimeCounter = 0;
    protected float cooldown1 = 0.8f;

    protected float cooldown2TimeCounter = 0;
    protected float cooldown2 = 0.8f;

    protected float secondsPassed = 0;
    protected int secondsPassedInt = 0;
    protected float enemySpawnTimer = 0;
    Random random = new Random();


    public GameObject changingText;
    protected TMPro.TextMeshProUGUI theText;

    public GameObject conglomerate;


    protected GameObject fastEnemyProjectile;
    protected EnemyProjectile fastEnemyProjectileScript;

    protected GameObject slowEnemyProjectile;
    protected EnemyProjectile slowEnemyProjectileScript;

    protected GameObject homingMissile;
    protected EnemyHomingMissile homingMissileScript;

    protected GameObject bulletShark;
    protected EnemyBulletShark bulletSharkScript;

    protected GameObject planarExplosion;
    protected PlanarExplosion planarExplosionScript;

    protected GameObject boomerang;
    protected EnemyBoomerang boomerangScript;

    protected GameObject growingProjectile;
    protected GrowingProjectile growingProjectileScript;

    protected const float enemySpawnFunctionCallInterval = 0.0166666666666667f;


    protected Hashtable timeToPhase;
    protected int phase = 1;

    protected GameObject thePlayer;

    protected void LoadEnemiesFromConglomerate()
    {
        Conglomerate temp = conglomerate.gameObject.GetComponent<Conglomerate>();

        fastEnemyProjectile = temp.fastEnemyProjectile;
        fastEnemyProjectileScript = fastEnemyProjectile.gameObject.GetComponent<EnemyProjectile>();

        fastEnemyProjectile.tag = "Enemy"; //for some reason this one became untagged


        slowEnemyProjectile = temp.slowEnemyProjectile;
        slowEnemyProjectileScript = slowEnemyProjectile.gameObject.GetComponent<EnemyProjectile>();

        homingMissile = temp.homingMissile;
        homingMissileScript = homingMissile.gameObject.GetComponent<EnemyHomingMissile>();

        bulletShark = temp.bulletShark;
        bulletSharkScript = bulletShark.gameObject.GetComponent<EnemyBulletShark>();

        planarExplosion = temp.planarExplosion;
        planarExplosionScript = planarExplosion.gameObject.GetComponent<PlanarExplosion>();

        boomerang = temp.boomerang;
        boomerangScript = boomerang.gameObject.GetComponent<EnemyBoomerang>();

        growingProjectile = temp.growingProjectile;
        growingProjectileScript = growingProjectile.gameObject.GetComponent<GrowingProjectile>();
    }

    protected void SetupEnemyDefaultVariables()
    {
        fastEnemyProjectileScript.maximumLifespanAllowed = 10;
        fastEnemyProjectileScript.speed = 10f;

        slowEnemyProjectileScript.maximumLifespanAllowed = 12;
        slowEnemyProjectileScript.speed = 6f;

        homingMissileScript.maximumLifespanAllowed = 12;

        bulletSharkScript.maximumLifespanAllowed = 12;

        boomerangScript.maximumLifespanAllowed = 10;
    }

    protected void SetupThePlayerVariable()
    {

        thePlayer = GeometryShift.playerStatus.gameObject;
        widthDividedBy100 = widthOfLevel / 100f;
        lengthDividedBy100 = lengthOfLevel / 100f;
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadEnemiesFromConglomerate();
        SetupEnemyDefaultVariables();

        SetupThePlayerVariable();
        thePlayer.AddComponent<Simple3DMovement>();

        timeToPhase = new Hashtable();//unique for each level

        //use: timeToPhase.Add(secondsPassed, phaseNumber);
        //starts at phase 1, so having timeToPhase.Add(0, 1) is unnessecary

        int testPhase = 0;
        bool fastMode = false;//if true, phases change faster than normal
        if (testPhase == 0)
        {
            if (fastMode)
            {
                timeToPhase.Add(1, 1);//slow projectiles
                timeToPhase.Add(2, 2);//slow + fast projectiles
                timeToPhase.Add(4, 3);//planar explosions + fast projectiles
                timeToPhase.Add(8, 4);//homing missiles
                timeToPhase.Add(12, 5);//planar explosions that spawn homing missiles
                //timeToPhase.Add(16, 0);//break time
                timeToPhase.Add(16, 6);//projectile waves from top and bottom
                timeToPhase.Add(20, 7);//double layer planar explosions: fast and slow projectiles
                timeToPhase.Add(24, 8);//double layer planar explosions: slow projectiles and homing missiles
                                       // timeToPhase.Add(47, 0);//break time
                timeToPhase.Add(28, 9);//fast projectiles from all directions
                timeToPhase.Add(32, 10);//slow projectiles from all directions, up to 45 degree angle variation
                timeToPhase.Add(36, 11);//spawn planar explosions on edge of map only
                timeToPhase.Add(40, 12);//bullet sharks from bottom
                timeToPhase.Add(44, 13);//bullet sharks from sides
                timeToPhase.Add(50, -1);//win
            }
            else
            {
                timeToPhase.Add(1, 1);//slow projectiles
                timeToPhase.Add(15, 2);//slow + fast projectiles
                timeToPhase.Add(35, 3);//planar explosions + fast projectiles
                timeToPhase.Add(55, 4);//homing missiles
                timeToPhase.Add(75, 5);//planar explosions that spawn homing missiles
                timeToPhase.Add(90, 0);//break time
                timeToPhase.Add(110, 6);//projectile waves from top and bottom
                timeToPhase.Add(130, 7);//double layer planar explosions: fast and slow projectiles
                timeToPhase.Add(150, 8);//double layer planar explosions: slow projectiles and homing missiles
                timeToPhase.Add(170, 9);//fast projectiles from all directions
                timeToPhase.Add(190, 10);//slow projectiles from all directions, up to 45 degree angle variation
                timeToPhase.Add(220, 11);//spawn planar explosions on edge of map only
                timeToPhase.Add(240, 12);//bullet sharks from bottom
                timeToPhase.Add(270, 13);//bullet sharks from sides
                timeToPhase.Add(300, -1);//win
            }
        }
        else
        {
            phase = testPhase;
        }

    }

    protected void setPhase()
    {
        if (timeToPhase.ContainsKey(secondsPassedInt))
        {
            phase = (int)timeToPhase[secondsPassedInt];
            timeToPhase.Remove(secondsPassedInt);
            cooldown1TimeCounter = 0;
        }
    }

    protected void spawnPlanarExplosion(GameObject projectile,
        int number = 6,
        bool randomSpawnLocation = true,
        float fuseTime = 1f,
        float x = 0,
        float z = 0
        )
    {
        Vector3 spawnPosition;
        if (randomSpawnLocation)
        {
            spawnPosition = new Vector3(Random.Range(-(widthOfLevel / 2), (widthOfLevel / 2)), 0f, Random.Range(-(lengthOfLevel / 2), (lengthOfLevel / 2)));
        }
        else
        {
            spawnPosition = new Vector3(x, 0f, z);
        }
        Quaternion spawnRotation = new Quaternion();
        //Quaternion.Euler(0f, Random.Range(0, 360), 0); for random rotation
        planarExplosionScript.theEnemyToSpawn = projectile;
        planarExplosionScript.numberOfEnemiesSpawned = number;
        planarExplosionScript.maximumLifespanAllowed = fuseTime;
        Instantiate(planarExplosion, spawnPosition, spawnRotation, transform.parent);
    }

    protected void spawnWave(GameObject projectile,
        int side = 3, //1 = top, 2 = right, 3 = bottom, 4 = left
        int numberOfFlankingProjectilesOnEachSide = 2,
        float widthSpacing = 0.6f,
        float distanceSpacing = 0.2f,
        float angleVariation = 0f,
        float spawningAngle = -1f
        )
    {
        float actualAngleVariation = Random.Range(-angleVariation, angleVariation);

        float x = 0;
        float y = 0;

        Vector3 spawnPosition = new Vector3(0f, 0f, 0f);
        Quaternion spawnRotation;

        bool useManualSpawnLocation = false;
        
        if (spawningAngle != -1)
        {
            spawningAngle %= 360;
            useManualSpawnLocation = true;
            if (spawningAngle >= 0 && spawningAngle < 90)
            {
                side = 1;

                spawningAngle = spawningAngle % 90;
                spawningAngle -= 45f;
                spawningAngle = spawningAngle / 0.9f;
                spawningAngle *= widthDividedBy100;

                x = spawningAngle;
                y = (lengthOfLevel / 2);
                spawnPosition = new Vector3(x, 0f, y);
            }
            else if (spawningAngle < 180)
            {
                side = 2;

                spawningAngle = spawningAngle % 90;
                spawningAngle -= 45f;
                spawningAngle = spawningAngle / 0.9f;
                spawningAngle *= widthDividedBy100;

                x = (widthOfLevel / 2);
                y = -spawningAngle;
                spawnPosition = new Vector3(x, 0f, y);
            }
            else if (spawningAngle < 270)
            {
                side = 3;

                spawningAngle = spawningAngle % 90;
                spawningAngle -= 45f;
                spawningAngle = spawningAngle / 0.9f;
                spawningAngle *= widthDividedBy100;

                x = -spawningAngle;
                y = -(lengthOfLevel / 2);
                spawnPosition = new Vector3(x, 0f, y);
            }
            else
            {
                side = 4;

                spawningAngle = spawningAngle % 90;
                spawningAngle -= 45f;
                spawningAngle = spawningAngle / 0.9f;
                spawningAngle *= widthDividedBy100;

                x = -(widthOfLevel / 2);
                y = spawningAngle;
                spawnPosition = new Vector3(x, 0f, y);
            }
        }

        switch (side)
        {
            case 1:
                if (!useManualSpawnLocation)
                {
                    x = Random.Range(-(widthOfLevel / 2), (widthOfLevel / 2));
                    y = (lengthOfLevel / 2);

                    spawnPosition = new Vector3(x, 0f, y);
                }
                spawnRotation = Quaternion.Euler(0f, 180f + actualAngleVariation, 0f);

                Instantiate(projectile, spawnPosition, spawnRotation, transform.parent);

                for (int i = 1; i <= numberOfFlankingProjectilesOnEachSide; i++)
                {
                    spawnPosition = new Vector3(x + (i * widthSpacing), 0f, y + (i * distanceSpacing));
                    Instantiate(projectile, spawnPosition, spawnRotation, transform.parent);
                    spawnPosition = new Vector3(x - (i * widthSpacing), 0f, y + (i * distanceSpacing));
                    Instantiate(projectile, spawnPosition, spawnRotation, transform.parent);
                }
                break;
            case 2:
                if (!useManualSpawnLocation)
                {
                    x = (lengthOfLevel / 2);
                    y = Random.Range(-(widthOfLevel / 2), (widthOfLevel / 2));

                    spawnPosition = new Vector3(x, 0f, y);
                }
                spawnRotation = Quaternion.Euler(0f, 270f + actualAngleVariation, 0f);

                Instantiate(projectile, spawnPosition, spawnRotation, transform.parent);

                for (int i = 1; i <= numberOfFlankingProjectilesOnEachSide; i++)
                {

                    spawnPosition = new Vector3(x + (i * distanceSpacing), 0f, y + (i * widthSpacing));
                    Instantiate(projectile, spawnPosition, spawnRotation, transform.parent);

                    spawnPosition = new Vector3(x + (i * distanceSpacing), 0f, y - (i * widthSpacing));
                    Instantiate(projectile, spawnPosition, spawnRotation, transform.parent);
                }
                break;
            case 3:
                if (!useManualSpawnLocation)
                {
                    x = Random.Range(-(widthOfLevel / 2), (widthOfLevel / 2));
                    y = -(lengthOfLevel / 2);

                    spawnPosition = new Vector3(x, 0f, y);
                }
                spawnRotation = Quaternion.Euler(0f, actualAngleVariation, 0f);

                Instantiate(projectile, spawnPosition, spawnRotation, transform.parent);

                for (int i = 1; i <= numberOfFlankingProjectilesOnEachSide; i++)
                {
                    spawnPosition = new Vector3(x + (i * widthSpacing), 0f, y - (i * distanceSpacing));
                    Instantiate(projectile, spawnPosition, spawnRotation, transform.parent);
                    spawnPosition = new Vector3(x - (i * widthSpacing), 0f, y - (i * distanceSpacing));
                    Instantiate(projectile, spawnPosition, spawnRotation, transform.parent);
                }
                break;
            case 4:
                if (!useManualSpawnLocation)
                {
                    x = -(lengthOfLevel / 2);
                    y = Random.Range(-(widthOfLevel / 2), (widthOfLevel / 2));

                    spawnPosition = new Vector3(x, 0f, y);
                }
                spawnRotation = Quaternion.Euler(0f, 90f + actualAngleVariation, 0f);

                Instantiate(projectile, spawnPosition, spawnRotation, transform.parent);

                for (int i = 1; i <= numberOfFlankingProjectilesOnEachSide; i++)
                {
                    spawnPosition = new Vector3(x - (i * distanceSpacing), 0f, y + (i * widthSpacing));
                    Instantiate(projectile, spawnPosition, spawnRotation, transform.parent);


                    spawnPosition = new Vector3(x - (i * distanceSpacing), 0f, y - (i * widthSpacing));
                    Instantiate(projectile, spawnPosition, spawnRotation, transform.parent);
                }
                break;
            default:
                print("spawnWave side needs to be 1-4. It recieved " + side);
                break;
        }




    }

    void WhatEnemiesShouldSpawn()//60 times a second, no matter the FPS
    {
        switch (phase)
        {
            case 1:
                if (Random.Range(0f, 60f) <= 2)
                {
                    //slow projectiles
                    Vector3 spawnPosition = new Vector3(Random.Range(-(widthOfLevel / 2), (widthOfLevel / 2)), 0f, -(lengthOfLevel / 2));
                    Quaternion spawnRotation = new Quaternion();
                    Instantiate(slowEnemyProjectile, spawnPosition, spawnRotation, transform.parent);

                }
                break;
            case 2:
                if (Random.Range(0f, 60f) <= 3)
                {
                    //slow projectiles

                    Vector3 spawnPosition = new Vector3(Random.Range(-(widthOfLevel / 2), (widthOfLevel / 2)), 0f, -(lengthOfLevel / 2));
                    Quaternion spawnRotation = new Quaternion();
                    Instantiate(slowEnemyProjectile, spawnPosition, spawnRotation, transform.parent);
                }
                if (Random.Range(0f, 60f) <= 2)
                {
                    //fast projectiles

                    Vector3 spawnPosition = new Vector3(Random.Range(-(widthOfLevel / 2), (widthOfLevel / 2)), 0f, -(lengthOfLevel / 2));
                    Quaternion spawnRotation = new Quaternion();
                    spawnRotation = Quaternion.Euler(0f, Random.Range(-30f, 30f), 0f);
                    Instantiate(fastEnemyProjectile, spawnPosition, spawnRotation, transform.parent);
                }
                break;
            case 3:
                cooldown1TimeCounter += enemySpawnFunctionCallInterval;
                cooldown1 = 0.8f;
                if (cooldown1TimeCounter > cooldown1)
                {
                    //planar explosions that spawn slow enemy projectiles
                    cooldown1TimeCounter -= cooldown1;
                    growingProjectileScript.sizeUp = 1.2f;
                    spawnPlanarExplosion(slowEnemyProjectile, 8, true, 1f);
                }
                break;
            case 4:
                if (Random.Range(0f, 60f) <= 3)
                {
                    //homing missiles

                    homingMissileScript.fuelTime = 1.5f;
                    homingMissileScript.turnSpeed = 1f;

                    Vector3 spawnPosition = new Vector3(Random.Range(-(widthOfLevel / 2), (widthOfLevel / 2)), 0f, -(lengthOfLevel / 2));
                    Quaternion spawnRotation = new Quaternion();
                    spawnRotation = Quaternion.Euler(0f, Random.Range(-40f, 40f), 0f);
                    Instantiate(homingMissile, spawnPosition, spawnRotation, transform.parent);

                }
                break;
            case 5:
                cooldown1 = 1.2f;

                cooldown1TimeCounter += enemySpawnFunctionCallInterval;
                if (cooldown1TimeCounter > cooldown1)
                {

                    homingMissileScript.fuelTime = 1.5f;
                    homingMissileScript.turnSpeed = 1f;
                    //planar explosion that spawns homing missiles
                    cooldown1TimeCounter -= cooldown1;
                    spawnPlanarExplosion(homingMissile, 5);
                }
                break;
            case 6:
                //waves top and bottom slow projectiles

                if (Random.Range(0f, 60f) <= 2.6)
                {
                    spawnWave(slowEnemyProjectile, 3);
                }

                if (Random.Range(0f, 60f) <= 2.6)
                {
                    spawnWave(slowEnemyProjectile, 1);

                }
                break;
            case 7:
                //double layer planar explosions: fast and slow projectiles
                cooldown1 = 1.2f;

                cooldown1TimeCounter += enemySpawnFunctionCallInterval;

                if (cooldown1TimeCounter > cooldown1)
                {
                    //planar explosion that spawns homing missiles
                    float tempX = Random.Range(-(widthOfLevel / 2), (widthOfLevel / 2));
                    float tempY = Random.Range(-(lengthOfLevel / 2), (lengthOfLevel / 2));
                    cooldown1TimeCounter -= cooldown1;
                    spawnPlanarExplosion(slowEnemyProjectile, 16, false, 1f, tempX, tempY);
                    spawnPlanarExplosion(fastEnemyProjectile, 12, false, 1f, tempX, tempY);
                }
                break;
            case 8:
                //double layer planar explosions: slow projectiles and homing missiles
                cooldown1 = 1.6f;

                cooldown1TimeCounter += enemySpawnFunctionCallInterval;

                if (cooldown1TimeCounter > cooldown1)
                {
                    float tempX = Random.Range(-(widthOfLevel / 2), (widthOfLevel / 2));
                    float tempY = Random.Range(-(lengthOfLevel / 2), (lengthOfLevel / 2));
                    cooldown1TimeCounter -= cooldown1;
                    homingMissileScript.fuelTime = 0.5f;
                    homingMissileScript.turnSpeed = 3.5f;
                    spawnPlanarExplosion(slowEnemyProjectile, 12, false, 1f, tempX, tempY);
                    spawnPlanarExplosion(homingMissile, 12, false, 1f, tempX, tempY);
                }
                break;
            case 9:
                //fast projectiles from all directions
                if (Random.Range(0f, 60f) <= 6)
                {
                    spawnWave(fastEnemyProjectile, Random.Range(1, 5), 0, 1f, 1f);
                }

                break;
            case 10:
                //slow projectiles from all directions, up to 45 degree angle variation
                if (Random.Range(0f, 60f) <= 12)
                {
                    spawnWave(slowEnemyProjectile, Random.Range(1, 5), 0, 1f, 1f, 45f);
                }

                break;
            case 11:
                //spawn planar explosions on edge of map only

                cooldown1TimeCounter += enemySpawnFunctionCallInterval;
                cooldown1 = 0.4f;
                if (cooldown1TimeCounter > cooldown1)
                {
                    cooldown1TimeCounter -= cooldown1;
                    planarExplosionScript.theEnemyToSpawn = slowEnemyProjectile;
                    planarExplosionScript.numberOfEnemiesSpawned = 10;
                    //planarExplosion.
                    spawnWave(planarExplosion, Random.Range(1, 5), 0);
                }

                break;

            case 12:
                //bullet sharks from bottom
                cooldown1TimeCounter += enemySpawnFunctionCallInterval;
                cooldown1 = 1.15f;
                if (cooldown1TimeCounter > cooldown1)
                {
                    cooldown1TimeCounter -= cooldown1;
                    //slow projectiles
                    Vector3 spawnPosition = new Vector3(Random.Range(-(widthOfLevel / 2), (widthOfLevel / 2)), 0f, -(lengthOfLevel / 2));
                    Quaternion spawnRotation = new Quaternion();
                    bulletSharkScript.whatToShoot = slowEnemyProjectile;
                    bulletSharkScript.shootInterval = 0.7f;
                    bulletSharkScript.turnSpeed = 0f;
                    bulletSharkScript.numberOfProjectilesToShoot = 4;
                    Instantiate(bulletShark, spawnPosition, spawnRotation, transform.parent);
                }
                break;
            case 13:
                //bullet sharks from sides
                cooldown1TimeCounter += enemySpawnFunctionCallInterval;
                cooldown1 = 1.5f;
                if (cooldown1TimeCounter > cooldown1)
                {
                    cooldown1TimeCounter -= cooldown1;

                    bulletSharkScript.whatToShoot = slowEnemyProjectile;
                    bulletSharkScript.shootInterval = 1.2f;
                    bulletSharkScript.turnSpeed = 0f;
                    bulletSharkScript.numberOfProjectilesToShoot = 4;
                    //Instantiate(bulletShark, spawnPosition, spawnRotation, transform.parent);
                    spawnWave(bulletShark, 2, 0);
                    spawnWave(bulletShark, 4, 0);
                }
                break;
            case -1:

                //Win level
                LevelBase.instance.AcknowledgeLevelCompletion();

                break;
            default:
                print("phase error. phase is " + phase);
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        secondsPassed += Time.deltaTime;
        secondsPassedInt = (int)secondsPassed;
        enemySpawnTimer += Time.deltaTime;

        while (enemySpawnTimer > enemySpawnFunctionCallInterval) // to make enemy spawn function run 60 times per second
                                                                 //even when FPS is above or below 60
        {
            enemySpawnTimer -= enemySpawnFunctionCallInterval;
            setPhase();
            WhatEnemiesShouldSpawn();
        }

    }
}
