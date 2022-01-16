using System;
using UnityEngine;

// Token: 0x0200009F RID: 159
public class DontDestroyMe : MonoBehaviour
{
	// Token: 0x06000507 RID: 1287 RVA: 0x0002425C File Offset: 0x0002245C
	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	// Token: 0x06000508 RID: 1288 RVA: 0x0002426C File Offset: 0x0002246C
	private void Start()
	{
	}

	// Token: 0x06000509 RID: 1289 RVA: 0x00024270 File Offset: 0x00022470
	private void Update()
	{
	}
}
