using System;
using UnityEngine;

// Token: 0x02000196 RID: 406
public class charactercontroller : MonoBehaviour
{
	// Token: 0x06000BE0 RID: 3040 RVA: 0x00056D5C File Offset: 0x00054F5C
	private void Update()
	{
		if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
		{
			base.transform.Rotate(new Vector3(0f, -Time.deltaTime * this.itsVelocity * 4f, 0f));
		}
		else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
		{
			base.transform.Rotate(new Vector3(0f, Time.deltaTime * this.itsVelocity * 4f, 0f));
		}
		if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
		{
			base.transform.position -= base.transform.forward * Time.deltaTime * this.itsVelocity;
		}
		if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
		{
			base.transform.position += base.transform.forward * Time.deltaTime * this.itsVelocity;
		}
	}

	// Token: 0x04000BBA RID: 3002
	private float itsVelocity = 30f;
}
