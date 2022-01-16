using System;
using UnityEngine;

// Token: 0x02000193 RID: 403
public class KGFMapSystemScriptControlDemo : MonoBehaviour
{
	// Token: 0x06000BD0 RID: 3024 RVA: 0x00056808 File Offset: 0x00054A08
	private void Start()
	{
		this.itsMapSystem = base.GetComponent<KGFMapSystem>();
		this.itsMapSystem.EventClickedOnMinimap += this.OnUserClickedOnMap;
		this.itsMapSystem.EventUserFlagCreated += this.OnUserFlagWasCreated;
	}

	// Token: 0x06000BD1 RID: 3025 RVA: 0x00056868 File Offset: 0x00054A68
	private void OnUserClickedOnMap(object theSender, EventArgs theEventArgs)
	{
		KGFMapSystem.KGFClickEventArgs kgfclickEventArgs = theEventArgs as KGFMapSystem.KGFClickEventArgs;
		if (kgfclickEventArgs != null)
		{
			Debug.Log("Clicked at position(world space): " + kgfclickEventArgs.itsPosition);
		}
	}

	// Token: 0x06000BD2 RID: 3026 RVA: 0x0005689C File Offset: 0x00054A9C
	private void OnUserFlagWasCreated(object theSender, EventArgs theEventArgs)
	{
		KGFMapSystem.KGFFlagEventArgs kgfflagEventArgs = theEventArgs as KGFMapSystem.KGFFlagEventArgs;
		if (kgfflagEventArgs != null)
		{
			Debug.Log("Created marker at position(world space): " + kgfflagEventArgs.itsPosition);
		}
	}

	// Token: 0x06000BD3 RID: 3027 RVA: 0x000568D0 File Offset: 0x00054AD0
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.M))
		{
			this.itsMapSystem.SetFullscreen(!this.itsMapSystem.GetFullscreen());
		}
		else if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			this.itsMapSystem.ZoomIn();
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			this.itsMapSystem.ZoomOut();
		}
		else if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			this.itsMapSystem.ZoomMax();
		}
		else if (Input.GetKeyDown(KeyCode.Alpha4))
		{
			this.itsMapSystem.ZoomMin();
		}
		else if (Input.GetKeyDown(KeyCode.H))
		{
			this.itsMapSystem.SetViewportEnabled(!this.itsMapSystem.GetViewportEnabled());
		}
		else if (Input.GetKeyDown(KeyCode.Z))
		{
			this.itsMapSystem.SetModeStatic(!this.itsMapSystem.GetModeStatic());
		}
	}

	// Token: 0x04000BA6 RID: 2982
	private KGFMapSystem itsMapSystem;
}
