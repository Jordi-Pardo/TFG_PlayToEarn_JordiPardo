using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    Animator animator;

    private int health = 0;
    public int strength = 0;

    public bool IsDead { get;private set; }
    private SpriteRenderer spriteRenderer;




    public void Setup(CharacterConfigSO characterConfigSO)
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        IsDead = false;
        health = characterConfigSO.health;
        strength = characterConfigSO.strength;
        spriteRenderer.sprite = characterConfigSO.sprite;

    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            animator.SetTrigger("Attack");
        }
    }
    public void Attack()
    {
        animator.SetTrigger("Attack");
    }
    public void RecieveDamage(int damage)
    {
        health -= damage;
        
       StartCoroutine(FlashDamage());
        if(health <= 0)
        {
            IsDead = true;
            animator.SetBool("IsDead", IsDead);
        }
    }

    public IEnumerator FlashDamage()
    {
        float duration = 1f;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.red;

        float speed = 2f;
        while (duration >=0)
        {
            duration -= Time.deltaTime;
            spriteRenderer.color = Color.Lerp(spriteRenderer.color, Color.white, Time.deltaTime * speed);
            yield return null;
        }
        spriteRenderer.color = Color.white;
    }
}
