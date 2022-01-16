using System;
using UnityEngine;

// Token: 0x02000197 RID: 407
public class charactercontroller_sidescroller : MonoBehaviour
{
	// Token: 0x06000BE2 RID: 3042 RVA: 0x00056EBC File Offset: 0x000550BC
	private void Awake()
	{
		this.itsRigidBody = base.rigidbody;
	}

	// Token: 0x06000BE3 RID: 3043 RVA: 0x00056ECC File Offset: 0x000550CC
	private void FixedUpdate()
	{
		if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
		{
			this.itsRigidBody.AddForce(-this.itsVelocity, 0f, 0f, ForceMode.Force);
		}
		else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
		{
			this.itsRigidBody.AddForce(this.itsVelocity, 0f, 0f, ForceMode.Force);
		}
		if (Input.GetKeyDown(KeyCode.UpArrow) || (Input.GetKey(KeyCode.Space) && this.itsGrounded))
		{
			base.rigidbody.AddForce(0f, 1500f, 0f);
			this.itsGrounded = false;
		}
		if (this.itsRigidBody.velocity.x > 5f)
		{
			this.itsRigidBody.velocity = new Vector3(5f, this.itsRigidBody.velocity.y, 0f);
		}
		else if (this.itsRigidBody.velocity.x < -5f)
		{
			this.itsRigidBody.velocity = new Vector3(-5f, this.itsRigidBody.velocity.y, 0f);
		}
		this.itsRigidBody.AddForce(0f, -50f, 0f);
		if (base.transform.position.y < -10f)
		{
			this.itsRigidBody.MovePosition(new Vector3(base.transform.position.x, 20f, 0f));
		}
	}

	// Token: 0x06000BE4 RID: 3044 RVA: 0x00057094 File Offset: 0x00055294
	public void OnCollisionEnter(Collision theCollision)
	{
		this.itsGrounded = true;
	}

	// Token: 0x04000BBB RID: 3003
	private float itsVelocity = 200f;

	// Token: 0x04000BBC RID: 3004
	private Rigidbody itsRigidBody;

	// Token: 0x04000BBD RID: 3005
	private bool itsGrounded = true;
}
