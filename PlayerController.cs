using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true; 
	private Vector3 velocity = Vector3.zero;

	private Animator anim;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator> ();
		AnimSide ();
	}

	void Update(){
	}

	public void Move(float move)
	{
		Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
		m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref velocity, m_MovementSmoothing);

		if (move > 0 && !m_FacingRight)
		{
			Flip();
		}
		else if (move < 0 && m_FacingRight)
		{
			Flip();
		}
	}


	private void Flip()
	{
		m_FacingRight = !m_FacingRight;
		AnimSide ();
	}
		
	public void AnimSide(){
		anim.SetBool ("isRight", m_FacingRight);
	}

}
