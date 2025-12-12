using UnityEngine;

public class AnimationBehaviour : MonoBehaviour
{
    [SerializeField]private Animator animator;
    public void Walk(Vector3 movement) 
    {
        animator.SetFloat("Velocity", movement.magnitude);
    }
    public void Dance(bool dancing)
    {
        animator.SetBool("Dance", dancing);
    }
}
