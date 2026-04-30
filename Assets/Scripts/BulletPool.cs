using UnityEngine;
using UnityEngine.Pool;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance { get; private set; }
    [SerializeField] private BulletBehaviour bulletPrefab;
    private ObjectPool<BulletBehaviour> pool;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        } else
        {
            Instance = this;
            CreatePool();
        }
    }

    public void SpawnBullet(Vector3 position, Quaternion rotation)
    {
        BulletBehaviour newBullet = pool.Get();
        newBullet.transform.SetPositionAndRotation(position, rotation);
        newBullet.InitialiseBullet();
    }

    public void Release(BulletBehaviour bulletBehaviour)
    {
        pool.Release(bulletBehaviour);
    }

    private void CreatePool()
    {
        pool = new ObjectPool<BulletBehaviour>(createFunc: OnCreate, actionOnGet: OnGet, actionOnRelease: OnRelease);
    }

    private void OnRelease(BulletBehaviour bulletBehaviour)
    {
        bulletBehaviour.gameObject.SetActive(false);
    }

    private void OnGet(BulletBehaviour bulletBehaviour)
    {
        bulletBehaviour.gameObject.SetActive(true);
    }

    private BulletBehaviour OnCreate()
    {
        return Instantiate(bulletPrefab);
    }
}
