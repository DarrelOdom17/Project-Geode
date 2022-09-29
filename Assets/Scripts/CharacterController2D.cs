using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
   public GameObject groundCheckPosition;
    public GameObject debugTarget;
    public GameObject debugDirection;
    public GameObject rayDebug;
    public Vector3 groundCheckSize = new Vector3(0.75f, 0.2f, 1.0f);
    public List<string> jumpableTags = new List<string>();
    public ContactFilter2D contactFilter2D;
    public CapsuleCollider2D CapsuleCollider2D;
    public bool isTouchingGround = false;
    
    private Ray ray;

    void Awake()
    {
        CapsuleCollider2D = GetComponentInChildren<CapsuleCollider2D>();
    }

    void OnDrawGizmosSelected()
    {
        /*if (isTouchingGround)
            Gizmos.color = new Color(0,1,0,0.5f);
        else
            Gizmos.color = new Color(1.0f, 0.0f, 0.0f, 0.5f);
        */
        
        Gizmos.color = (isTouchingGround) ? new Color(0,1,0,0.5f) : new Color(1.0f, 0.0f, 0.0f, 0.5f);

        Gizmos.DrawCube(groundCheckPosition.transform.position, groundCheckSize);

        Gizmos.DrawRay(ray);
    }

    public bool IsTouchingGround()
    {
        List<RaycastHit2D> results = new List<RaycastHit2D>();

        GroundCheck(results);

            foreach (RaycastHit2D hit in results)
            {
                if (jumpableTags.Contains(hit.collider.tag))
                {
                    return true;
                }
            }
            return false;
    }

    public void GroundCheck(List<RaycastHit2D> results)
    {
        Physics2D.BoxCast(
            new Vector2(
                groundCheckPosition.transform.position.x, 
                groundCheckPosition.transform.position.y),
            new Vector2(
                groundCheckSize.x,
                groundCheckSize.y),
            0.0f,
            Vector2.right, 
            contactFilter2D,
            results,
            0.1f
        );
    }
}
