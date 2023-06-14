using System.Collections;
using UnityEngine;

public class ButoEnemy : MonoBehaviour
{
    [Header("Monster Status")]
    [SerializeField] float hpButo;
    [SerializeField] float maxHpButo = 5;
    public float attackDistance;
    public float moveSpeed;
    public float timer;
    private float intTimer;
    [HideInInspector] public Transform target;
    private float distance;

    [Header("Monster Triggered")]
    public GameObject hotZone;
    public GameObject triggerArea;

    [Header("Sprite")]
    [SerializeField] SpriteRenderer butoSprite;

    [Header("Patrol Monster")]
    public Transform rightPos;
    public Transform leftPos;

    [Header("Animator")]
    private Animator butoAnim;

    [Header("Particle")]
    [SerializeField] GameObject hitEfxGo;
    [SerializeField]Transform efxspawn;


    [Header("Boolean")]
    [HideInInspector] public bool inRange;
    private bool attackMode;
    private bool cooling;

    [Header("KnockBack")]
    [SerializeField] private Rigidbody2D butoRb;
    [SerializeField] private float knockBackForce;
    [SerializeField] private float knockBackCounter;
    [SerializeField] private float knockBackTotalTime;
    private bool knockFromHit;

    [Header("References")]
    public static ButoEnemy instance;
    public HealthButoCanvas healthButo;

    private void Awake()
    {
        instance = this;
        SelectTarget();
        intTimer = timer;
        butoAnim = GetComponent<Animator>();
        butoRb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (!attackMode)
        {
            Move();
        }

        if(!InsideofLimits()&& !butoAnim.GetCurrentAnimatorStateInfo(0).IsName("Buto_Attack"))
        {
            SelectTarget();
        }
        if(inRange)
        {

            EnemyLogic();
        }
    }
   public void KenaDamage(float damage)
    {
        hpButo -= damage;
        StartCoroutine(BlinkSprite(0.5f));
        healthButo.UpdateHealthBar(maxHpButo, hpButo);
        if(hpButo <= 0)
        {
          GameObject efs=  (GameObject)Instantiate(hitEfxGo,efxspawn);
          efs.transform.SetParent(null);
            Destroy(gameObject);
        }
    }
    
    void Move()
    {
        butoAnim.SetBool("Walk", true);
        if (!butoAnim.GetCurrentAnimatorStateInfo(0).IsName("Buto_Attack"))
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed *  Time.deltaTime);
        }
    }

    void Attack()
    {
        timer = intTimer;
        attackMode = true;

        butoAnim.SetBool("Walk", false);
        butoAnim.SetBool("Attack", true);
    }

    void StopAttack()
    {
        cooling = false;
        attackMode = false;
        butoAnim.SetBool("Attack", false);
    }

    void Cooldown()
    {
        timer -= Time.deltaTime;
        if(timer <= 0 && cooling && attackMode)
        {
            cooling = false;
            timer = intTimer;
        }
    }

    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position,target.position);

        if(distance > attackDistance)
        {

            StopAttack();
        }
        else if(attackDistance >= distance && cooling == false)
        {
            Attack();
        }
        if (cooling)
        {
            Cooldown();
            butoAnim.SetBool("Attack", false);
        }
    }

    public void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        if(transform.position.x > target.position.x)
        {
            rotation.y = 180f;
        }
        else
        {
            rotation.y = 0f;
        }

        transform.eulerAngles = rotation;
    }

    public void TriggerCooling()
    {
        cooling = true;
    }

    private bool InsideofLimits()
    {
        return transform.position.x > leftPos.position.x && transform.position.x < rightPos.position.x;
    }

    public void SelectTarget()
    {
        float distanceToLeft = Vector2.Distance(transform.position, leftPos.position);
        float distanceToRight = Vector2.Distance(transform.position, rightPos.position);

        if(distanceToLeft > distanceToRight)
        {
            target = leftPos;
        }
        else
        {
            target = rightPos;
        }

        Flip();
    }

    public void KnockBackButo()
    {
        if (knockFromHit == true)
        {
            butoRb.velocity = new Vector2(-knockBackForce, knockBackForce);
        }
        if (knockFromHit == false)
        {
            butoRb.velocity = new Vector2(knockBackForce, knockBackForce);
        }

        knockBackCounter -= Time.deltaTime;
    }

    private IEnumerator BlinkSprite(float duration)
    {
        float elapsedTime = 0f;
        Color originalColor = butoSprite.color;
        Color blinkColor = Color.red;

        while (elapsedTime < duration)
        {
            butoSprite.color = blinkColor;
            yield return new WaitForSeconds(0.1f);
            butoSprite.color = originalColor;
            yield return new WaitForSeconds(0.1f);
            elapsedTime += 0.2f;
        }
        butoSprite.color = Color.white;
    }
}
