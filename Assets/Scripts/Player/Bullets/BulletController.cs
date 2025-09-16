using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public class BulletController : MonoBehaviour, IBullet
{
    private BulletConfig config;
    private IObjectPool<GameObject> originPool;


    private Vector3 direction = Vector3.up;
    private float lifeTimer;


    // Opcional: si preferís Rigidbody2D, reemplazar Update() por física en FixedUpdate.


    public void Initialize(BulletConfig config, IObjectPool<GameObject> originPool)
    {
        this.config = config;
        this.originPool = originPool;
    }


    public void SetDirection(Vector3 direction)
    {
        this.direction = direction.normalized;
    }


    private void OnEnable()
    {
        lifeTimer = config != null ? config.lifeTime : 3f;
    }


    private void Update()
    {
        // Mover
        float speed = (config != null) ? config.speed : 30f;
        transform.position += speed * Time.deltaTime * direction;


        // Lifetime
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0f)
        {
            ReturnToPool();
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Aplica daño usando tu patrón Command existente
        var target = collision.gameObject.GetComponent<IDamageable>();
        if (target != null)
        {
            int dmg = (config != null) ? config.damage : 5;
            ICommand damageCommand = new DamageCommand(target, dmg);
            damageCommand.Execute();
        }


        ReturnToPool();
    }


    private void ReturnToPool()
    {
        if (originPool != null)
            originPool.Return(gameObject);
        else
            gameObject.SetActive(false);
    }
}