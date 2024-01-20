using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;
using UnityEngine.SceneManagement;

public class EnemyBehavior : MonoBehaviour
{

    public float attackDistance = 2.5f; // The distance at which the enemy starts to attack
    public int maxHealth = 1; // The maximum health of the enemy
    public float moveSpeed = 1.5f; // The movement speed of the enemy
    public float rotSpeed = 20f;
    Animator playAnim;

    ZombieSpawner zombieSpawner;
    private int currentHealth; // The current health of the enemy
    private bool isAttacking; // Whether the enemy is currently attacking
    private bool isDead; // Whether the enemy is dead

void Start()
    {
        zombieSpawner = FindObjectOfType<ZombieSpawner>();
        currentHealth = maxHealth;
        isAttacking = false;
        isDead = false;
        playAnim = gameObject.GetComponent<Animator>();
    }
    
    public void Quit() {
        
        SceneManager.LoadScene(0);
    }
void Update()
{
    if (isDead) return;

    // Check if the enemy is close enough to attack the player
    float distanceToPlayer = Vector3.Distance(transform.position, Camera.main.transform.position);
    if (distanceToPlayer <= attackDistance)
    {
        isAttacking = true;
        transform.LookAt(Camera.main.transform.position);
        playAnim.SetBool("Punch", true);
        playAnim.SetBool("Walking", false);
        Invoke("Quit", 2.0f);
    }
    else
    {
        isAttacking = false;
        transform.LookAt(Camera.main.transform.position);
        playAnim.SetBool("Punch", false);
        playAnim.SetBool("Walking", true);
    }

    // If the enemy is not attacking, move towards the player
    if (!isAttacking)
    {
        Vector3 targetPosition = new Vector3(Camera.main.transform.position.x, transform.position.y, Camera.main.transform.position.z);
        Vector3 direction = (new Vector3(Camera.main.transform.position.x, transform.position.y, Camera.main.transform.position.z) - transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotSpeed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }
}
public void TakeDamage(int damage)
{
    currentHealth -= damage;

    if (currentHealth <= 0)
    {
        Die();
    }
}

private void Die()
{
    
    zombieSpawner.ZombieKilled(gameObject);

    isDead = true;


    // Destroy the enemy GameObject or perform any other actions necessary upon death
}
}