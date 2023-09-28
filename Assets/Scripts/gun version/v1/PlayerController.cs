using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Weapon weapon;
    public Weapon weapon2;
    public Weapon weapon3;
    public Weapon weapon4;
    public Weapon weapon5;
    public Weapon weapon6;
    public Weapon weapon7;
    public Weapon weapon8;

    Vector2 moveDirection;
    Vector2 mousePosition;

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if(Input.GetMouseButtonDown(0))
        {
            weapon.Fire();
            StartCoroutine(TimeDelay2());
            StartCoroutine(TimeDelay3());
            StartCoroutine(TimeDelay4());
            StartCoroutine(TimeDelay5());
            StartCoroutine(TimeDelay6());
            StartCoroutine(TimeDelay7());
            StartCoroutine(TimeDelay8());
            
        }

        moveDirection = new Vector2(moveX, moveY).normalized;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);

        Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;
        
    }

    IEnumerator TimeDelay2()
    {
        yield return new WaitForSeconds(1);
        weapon2.Fire();
    }
    IEnumerator TimeDelay3()
    {
        yield return new WaitForSeconds(2);
        weapon3.Fire();
    }
    IEnumerator TimeDelay4()
    {
        yield return new WaitForSeconds(3);
        weapon4.Fire();
    }
    IEnumerator TimeDelay5()
    {
        yield return new WaitForSeconds(4);
        weapon5.Fire();
    }
    IEnumerator TimeDelay6()
    {
        yield return new WaitForSeconds(5);
        weapon6.Fire();
    }
    IEnumerator TimeDelay7()
    {
        yield return new WaitForSeconds(6);
        weapon7.Fire();
    }
    IEnumerator TimeDelay8()
    {
        yield return new WaitForSeconds(7);
        weapon8.Fire();
    }
}
