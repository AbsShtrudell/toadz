using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PepleJump
{
    public enum PlatformType
    {
        Normal = 1, Spring = 2, Fragile = 4, Broken = 8,
        Target = 16, Disposable = 32, MovingHorizontally = 64, VoidHole = 128,
        Explosive = 256, SittingMonster = 512, FlyingMonster = 1024
    }

    public class PlatformController : MonoBehaviour
    {
        [Zenject.Inject] private PlatformsSpawner spawner;
        [Zenject.Inject] private PlatformTraits platformTraits;
        [Zenject.Inject] private PowerupSpawner powerupSpawner;
        [Zenject.Inject] private PowerupTraits powerupTraits;

        [SerializeField, Min(0f)] private float startVerticalSpreadMin = 0.5f;
        [SerializeField, Min(0f)] private float startVerticalSpreadMax = 1f;
        [SerializeField, Min(0f)] private float endVerticalSpreadMin = 4f;
        [SerializeField, Min(0f)] private float endVerticalSpreadMax = 4f;
        [SerializeField, Min(0f)] private float verticalSpreadMinDelta = 0.1f;
        [SerializeField, Min(0f)] private float verticalSpreadMaxDelta = 0.1f;
        [SerializeField] private float _horizontalSpreadMin = -1.8f;
        [SerializeField] private float _horizontalSpreadMax = 1.8f;
        [SerializeField, Min(1)] protected int maxPlatformCount = 7;
        [Space]
        [SerializeField] NormalPlatform startPlatform;

        public float horizontalSpreadMin => _horizontalSpreadMin;
        public float horizontalSpreadMax => _horizontalSpreadMax;

        private float currentVerticalSpreadMin;
        private float currentVerticalSpreadMax;
        private float nextY;
        private float previousNextY;
        private float previousNextX;
        private float nextX;
        private int leftSideFactor = 0;
        private int rightSideFactor = 1;
        private bool aboveMax = false;

        private List<IPlatform> platformsInGame = new List<IPlatform>();
        private IPlatform lastPlatform
        { get { if (platformsInGame.Count == 0) return null; else return platformsInGame[platformsInGame.Count - 1]; } }

        protected virtual void Start()
        {
            currentVerticalSpreadMin = startVerticalSpreadMin;
            currentVerticalSpreadMax = startVerticalSpreadMax;
            nextY = startPlatform.transform.position.y + endVerticalSpreadMax; //Random.Range(currentVerticalSpreadMin, currentVerticalSpreadMax);
            previousNextY = startPlatform.transform.position.y;
            nextX = GetXPosition();

            platformsInGame.Add(startPlatform);

            for (int i = 1; i <= maxPlatformCount; i++)
            {
                SpawnNext();
            }
        }

        private float GetXPosition()
        {
            int temp = leftSideFactor;
            leftSideFactor = rightSideFactor;
            rightSideFactor = temp;

            return Random.Range(horizontalSpreadMin * leftSideFactor, horizontalSpreadMax * rightSideFactor);
            //return Random.Range(horizontalSpreadMin, horizontalSpreadMax);
        }

        public virtual void SpawnNext()
        {
            SpawnFillerPlatforms();

            MovePlatform(GetNextMainPlatform(), true);
        }

        protected void SpawnFillerPlatforms()
        {
            float nextFillerY = previousNextY + currentVerticalSpreadMin;

            while (nextFillerY <= nextY - currentVerticalSpreadMin)
            {
                var platform = GetNextPlatform();

                void onDespawned(IPlatform platform1)
                {
                    platformsInGame.Remove(platform1);
                    platform.onDespawned -= onDespawned;
                };
                platform.onDespawned += onDespawned;

                Vector3 position = Vector3.zero;
                position.y = nextFillerY;

                //for (int i = 0; i <= 15; i++)
                //{
                //    position.x = GetXPosition();
                //    if (!IsInSpawnFreeArea(platform, position)) break;
                //}

                position.x = GetXPosition();
                float defaultX = position.x;
                while (IsInSpawnFreeArea(platform, position))
                {
                    position.x -= 0.01f;
    
                    if (position.x < horizontalSpreadMin)
                    {
                        position.x = defaultX;
                        break;
                    }
    
                }
    
                while (IsInSpawnFreeArea(platform, position))
                {
                    position.x += 0.01f;
    
                    if (position.x > horizontalSpreadMax)
                    {
                        position.x = horizontalSpreadMax;
                        break;
                    }
    
                }

                platform.transform.position = position;

                nextFillerY += Random.Range(currentVerticalSpreadMin, currentVerticalSpreadMax);

                //currentVerticalSpreadMin = Mathf.MoveTowards(currentVerticalSpreadMin, endVerticalSpreadMin, verticalSpreadMinDelta);
                //currentVerticalSpreadMax = Mathf.MoveTowards(currentVerticalSpreadMax, endVerticalSpreadMax, verticalSpreadMaxDelta);
            }
        }

        protected IPlatform GetNextPlatform(PlatformType ignoreTypes = PlatformType.Target)
        {
            IPlatform platform = null;

            foreach (var rule in platformTraits.spawnRules)
            {
                PlatformType type = rule.type;

                if (ignoreTypes.HasFlag(type)) continue;
                if (spawner.InGame(type) >= rule.maxInGame) continue;
                if (PlatformsInRow(type) >= rule.maxInRow) continue;
                if (Random.Range(0f, 100f) >= rule.spawnChance) continue;

                platform = spawner.Spawn(type);
                break;
            }

            if (platform == null)
            {
                platform = SpawnNormal();
            }

            platformsInGame.Add(platform);

            foreach (var rule in platformTraits.spawnRules)
            {
                rule.spawnChance = Mathf.MoveTowards(rule.spawnChance, rule.endSpawnChance, rule.spawnChanceDelta);
            }

            return platform;
        }

        private IPlatform SpawnNormal()
        {
            IPlatform platform = spawner.Spawn(PlatformType.Normal);

            foreach (var rule in powerupTraits.rules)
            {
                var type = rule.type;

                if (powerupSpawner.InGame() >= powerupTraits.maxInGame) continue;
                if (Random.Range(0, 100) >= rule.spawnChance) continue;

                var powerup = powerupSpawner.Spawn(type);
                powerup.transform.parent = platform.transform;
                powerup.transform.localPosition = Vector3.up;

                break;
            }

            return platform;
        }

        protected void MovePlatform(IPlatform platform, bool changeSpread)
        {
            Vector3 position = platform.transform.position;
            position.y = nextY;

            //for(int i = 0; i <= 15; i++)
            //{
            //    position.x = GetXPosition();
            //    if (!IsInSpawnFreeArea(platform, position)) break;
            //}

            position.x = GetXPosition();
            float defaultX = position.x;
            while (IsInSpawnFreeArea(platform, position))
            {
                position.x -= 0.01f;

                if (position.x < horizontalSpreadMin)
                {
                   // position.x = defaultX;
                    break;
                }

            }

            while (IsInSpawnFreeArea(platform, position))
            {
                position.x += 0.01f;

                if (position.x > horizontalSpreadMax)
                {
                    position.x = horizontalSpreadMax;
                    break;
                }

            }

            platform.transform.position = position;

            previousNextY = nextY;
            nextY += endVerticalSpreadMax; //Random.Range(currentVerticalSpreadMin, currentVerticalSpreadMax);
            nextX = GetXPosition();

            if (changeSpread)
            {
                currentVerticalSpreadMin = Mathf.MoveTowards(currentVerticalSpreadMin, endVerticalSpreadMin, verticalSpreadMinDelta);
                currentVerticalSpreadMax = Mathf.MoveTowards(currentVerticalSpreadMax, endVerticalSpreadMax, verticalSpreadMaxDelta);
            }
        }

        protected IPlatform GetNextMainPlatform()
        {
            var platform = GetNextPlatform(PlatformType.Fragile | PlatformType.VoidHole | PlatformType.Target | PlatformType.FlyingMonster | PlatformType.SittingMonster);

            void onDespawned(IPlatform platform1)
            {
                platformsInGame.Remove(platform1);
                SpawnNext();

                platform.onDespawned -= onDespawned;
            };
            platform.onDespawned += onDespawned;

            return platform;
        }

        protected bool IsInSpawnFreeArea(IPlatform platform, Vector2 position)
        {
            Vector2 pos = position + platform.Offset;
            float minX = pos.x - platform.SpawnFree.x;
            float maxX = pos.x + platform.SpawnFree.x;
            float minY = pos.y - platform.SpawnFree.y;
            float maxY = pos.y + platform.SpawnFree.y;

            int startPlatform = platformsInGame.Count - 2;
            int endPlatform = startPlatform - 3;

            for (int i = startPlatform; i >= 0 && i >= endPlatform; i--)
            {
                if((minX >= platformsInGame[i].spawnFreeCenter.x - platformsInGame[i].SpawnFree.x && maxX <= platformsInGame[i].spawnFreeCenter.x + platformsInGame[i].SpawnFree.x) &&
                   (minY >= platformsInGame[i].spawnFreeCenter.y - platformsInGame[i].SpawnFree.y && maxY <= platformsInGame[i].spawnFreeCenter.y + platformsInGame[i].SpawnFree.y))
                   return true;
            }
            return false;
        }

        protected int PlatformsInRow(PlatformType type)
        {
            int result = 0;
            int maxResult = 0;
            for(int i = 0; i < platformsInGame.Count; i ++)
            {
                if (type == platformsInGame[i].GetPlatformType())
                {
                    result++;
                }
                else if (result >= maxResult)
                {
                    maxResult = result;
                    result = 0;
                }
                else
                {
                    result = 0;
                }
            }

            if (result >= maxResult)
            {
                maxResult = result;
                result = 0;
            }
            return maxResult;
        }
    }
}

//public class PlatformController : MonoBehaviour
//{
//    [System.Serializable]
//    private class Chance
//    {
//        public delegate void MakeAction(Platform p);

//        public Platform.Type type;
//        public float value;
//        public MakeAction action;
//    }

//    [SerializeField] private GameObject platformRef;
//    [SerializeField, Min(0f)] private float startVerticalSpreadMin = 0.5f;
//    [SerializeField, Min(0f)] private float startVerticalSpreadMax = 1f;
//    [SerializeField, Min(0f)] private float endVerticalSpreadMin = 4f;
//    [SerializeField, Min(0f)] private float endVerticalSpreadMax = 4f;
//    [SerializeField, Min(0f)] private float verticalSpreadMinDelta = 0.1f;
//    [SerializeField, Min(0f)] private float verticalSpreadMaxDelta = 0.1f;
//    [SerializeField] private float _horizontalSpreadMin = -1.8f;
//    [SerializeField] private float _horizontalSpreadMax = 1.8f;
//    [SerializeField, Min(1)] protected int maxPlatformCount = 7;
//    [SerializeField] private Transform jetpackPool;
//    [SerializeField] private Chance[] chances;
//    [Zenject.Inject] private Zenject.DiContainer container;
//    [Zenject.Inject] private PlatformTraits traits;
//    private float currentVerticalSpreadMin;
//    private float currentVerticalSpreadMax;
//    private float nextY;
//    private float nextX;
//    private float sumChance = 0f;
//    private bool aboveMax = false;
//    private Platform lastNormal;

//    public float horizontalSpreadMin => _horizontalSpreadMin;
//    public float horizontalSpreadMax => _horizontalSpreadMax;

//    public event System.Action onPepleWon;

//    protected virtual void Start()
//    {
//        foreach (var c in chances)
//        {
//            sumChance += c.value;

//            switch (c.type)
//            {
//                case Platform.Type.Normal:
//                    c.action = MakeNormal;
//                    break;
//                case Platform.Type.Spring:
//                    c.action = MakeSpring;
//                    break;
//                case Platform.Type.Disposable:
//                    c.action = MakeDisposable;
//                    break;
//                case Platform.Type.Fragile:
//                    c.action = MakeFragile;
//                    break;
//                default:
//                    Debug.LogError("Missing action for " + c.type);
//                    break;
//            }
//        }

//        currentVerticalSpreadMin = startVerticalSpreadMin;
//        currentVerticalSpreadMax = startVerticalSpreadMax;
//        nextY = platformRef.transform.position.y + Random.Range(currentVerticalSpreadMin, currentVerticalSpreadMax);
//        nextX = Random.Range(horizontalSpreadMin, horizontalSpreadMax);
//        lastNormal = platformRef.GetComponent<Platform>();

//        for (int i = 1; i <= maxPlatformCount; i++)
//        {
//            var p = container.InstantiatePrefab(platformRef).GetComponent<Platform>();
//            p.onPepleWon += Won;
//            SpawnNext(p);
//        }
//    }

//    private void Won()
//    {
//        onPepleWon?.Invoke();
//    }

//    public virtual void SpawnNext(Platform p)
//    {
//        p.StopHorizontalMovement();

//        aboveMax = nextY - lastNormal.transform.position.y > endVerticalSpreadMax;

//        if (aboveMax)
//        {
//            nextY = lastNormal.transform.position.y + endVerticalSpreadMax;
//        }

//        float randomValue = Random.Range(0f, sumChance);

//        float lowerBound = 0f, upperBound = 0f;
//        foreach (var c in chances)
//        {
//            upperBound += c.value;

//            if (randomValue >= lowerBound && randomValue <= upperBound)
//            {
//                c.action(p);
//                break;
//            }

//            lowerBound += c.value;
//        }

//        MovePlatform(p);
//    }

//    private void MakeDisposable(Platform p)
//    {
//        p.type = Platform.Type.Disposable;

//        lastNormal = p;
//    }

//    private void MakeFragile(Platform p)
//    {
//        if (aboveMax)
//        {
//            MakeNormal(p);
//        }
//        else
//            p.type = Platform.Type.Fragile;
//    }

//    private void MakeSpring(Platform p)
//    {
//        p.type = Platform.Type.Spring;

//        lastNormal = p;
//    }

//    private void MakeNormal(Platform p)
//    {
//        p.type = Platform.Type.Normal;

//        lastNormal = p;

//        if (Random.Range(0, 10) < 4)
//        {
//            p.StartHorizontalMovement();
//        }
//        else if (jetpackPool.childCount > 0 && Random.Range(0, 10) == 0)
//        {
//            var item = jetpackPool.GetChild(0);

//            item.SetParent(null);
//            item.position = new Vector2(nextX, nextY + 0.5f);
//            item.gameObject.SetActive(true);
//        }
//    }

//    protected void MovePlatform(Platform p)
//    {
//        Vector3 position = p.transform.position;
//        position.y = nextY;
//        position.x = nextX;
//        p.transform.position = position;

//        nextY += Random.Range(currentVerticalSpreadMin, currentVerticalSpreadMax);
//        nextX = Random.Range(horizontalSpreadMin, horizontalSpreadMax);

//        currentVerticalSpreadMin = Mathf.MoveTowards(currentVerticalSpreadMin, endVerticalSpreadMin, verticalSpreadMinDelta);
//        currentVerticalSpreadMax = Mathf.MoveTowards(currentVerticalSpreadMax, endVerticalSpreadMax, verticalSpreadMaxDelta);
//    }
//}