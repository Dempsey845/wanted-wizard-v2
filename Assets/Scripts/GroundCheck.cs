using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] Transform groundCheckPoint;
    [SerializeField] float maxCheckDistance = 1f;
    [SerializeField] LayerMask groundLayerMask;

    bool grounded = false;

    public bool IsGrounded {get { return grounded; }}

    void Update()
    {
        grounded = Physics.Raycast(groundCheckPoint.position, Vector3.down, maxCheckDistance, groundLayerMask);
    }
}
