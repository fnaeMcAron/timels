using UnityEngine;

public class FnaeController : CharacterBase
{
    [Header("Настройки Фная")]
    public float burnDamage = 10f;
    public GameObject molotovPrefab;
    //public GameObject fireEffect;

    public override void PerformMeleeAttack()
    {
        Debug.Log($"Фнай: удар вблизи {damageMultiplier}");
        StartCoroutine(EnablingCollider(1f, 0, 10, "Удар перчатками"));
        //CreateBurnEffect();
    }

    public override void PerformRangedAttack()
    {
        Debug.Log($"Фнай: дистанционные перчатки {damageMultiplier}");
        ShootFireProjectile();
    }

    public override void UseAbility(bool isHold)
    {
        Debug.Log($"Фнай: Молотовы {damageMultiplier}");
        ShootFireProjectile();
    }

    public override void Dodge()
    {
        Debug.Log("Фнай: уворот");
    }

    public override void PerformMeleeChargeAttack()
    {

    }

    public override void PerformRangedAim()
    {

    }

    /*private void CreateBurnEffect()
    {
        // TODO: �������� ��������������� ����� � �������� ����������
        if (fireEffect != null)
        {
            Instantiate(fireEffect, transform.position, Quaternion.identity);
        }
    }*/

    private void ShootFireProjectile()
    {
        GameObject projectile = Instantiate(molotovPrefab, transform.position + transform.up * 2, Quaternion.identity);
        Rigidbody molotovPhys = projectile.GetComponent<Rigidbody>();

        projectile.GetComponent<Renderer>().material.color = Color.red;
        //подправь чтобы они летели ниже
        molotovPhys.linearVelocity = new Vector3(cameraFollow.transform.forward.x * moveSpeed, 5f, cameraFollow.transform.forward.z * moveSpeed);
    }

    private void IgniteArea()
    {
        // TODO: �������� ��������������� ����� � �������� ����������
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 5f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                enemy = hitCollider.GetComponent<Enemy>();
                enemy.TakeDamage(25f);
                StartCoroutine(enemy.Stun(10));
            }
        }
    }
}