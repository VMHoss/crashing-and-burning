using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200015C RID: 348
[Serializable]
public class KGFEventSequence : KGFEventBase, KGFIValidator
{
	// Token: 0x06000A08 RID: 2568 RVA: 0x0004D174 File Offset: 0x0004B374
	protected override void KGFAwake()
	{
		KGFEventSequence.itsListOfSequencesAll.Add(this);
	}

	// Token: 0x06000A09 RID: 2569 RVA: 0x0004D184 File Offset: 0x0004B384
	public void Step()
	{
		this.itsStayBeforeStepID++;
	}

	// Token: 0x06000A0A RID: 2570 RVA: 0x0004D194 File Offset: 0x0004B394
	public void Finish()
	{
		this.itsStayBeforeStepID = this.itsEntries.Count + 1;
	}

	// Token: 0x06000A0B RID: 2571 RVA: 0x0004D1AC File Offset: 0x0004B3AC
	public static bool GetSingleStepMode()
	{
		return KGFEventSequence.itsStepMode;
	}

	// Token: 0x06000A0C RID: 2572 RVA: 0x0004D1B4 File Offset: 0x0004B3B4
	public bool IsWaitingForDebugInput()
	{
		return this.itsStayBeforeStepID == this.itsEventDoneCounter;
	}

	// Token: 0x06000A0D RID: 2573 RVA: 0x0004D1E4 File Offset: 0x0004B3E4
	public int GetCurrentStepNumber()
	{
		return this.itsEventDoneCounter.GetValueOrDefault(0);
	}

	// Token: 0x06000A0E RID: 2574 RVA: 0x0004D1F4 File Offset: 0x0004B3F4
	public int GetStepCount()
	{
		return this.itsEntries.Count;
	}

	// Token: 0x06000A0F RID: 2575 RVA: 0x0004D204 File Offset: 0x0004B404
	public static void SetSingleStepMode(bool theActivateStepMode)
	{
		KGFEventSequence.itsStepMode = theActivateStepMode;
	}

	// Token: 0x06000A10 RID: 2576 RVA: 0x0004D20C File Offset: 0x0004B40C
	public static KGFEventSequence[] GetAllSequences()
	{
		return KGFEventSequence.itsListOfSequencesAll.ToArray();
	}

	// Token: 0x06000A11 RID: 2577 RVA: 0x0004D218 File Offset: 0x0004B418
	public static IEnumerable<KGFEventSequence> GetQueuedSequences()
	{
		for (int i = KGFEventSequence.itsListOfSequencesAll.Count - 1; i >= 0; i--)
		{
			KGFEventSequence aSequence = KGFEventSequence.itsListOfSequencesAll[i];
			if (aSequence == null)
			{
				KGFEventSequence.itsListOfSequencesAll.RemoveAt(i);
			}
			else if (aSequence.gameObject == null)
			{
				KGFEventSequence.itsListOfSequencesAll.RemoveAt(i);
			}
			else if (aSequence.IsQueued())
			{
				yield return aSequence;
			}
		}
		yield break;
	}

	// Token: 0x06000A12 RID: 2578 RVA: 0x0004D234 File Offset: 0x0004B434
	public static int GetNumberOfRunningSequences()
	{
		return KGFEventSequence.itsListOfRunningSequences.Count;
	}

	// Token: 0x06000A13 RID: 2579 RVA: 0x0004D240 File Offset: 0x0004B440
	public static KGFEventSequence[] GetRunningEventSequences()
	{
		return KGFEventSequence.itsListOfRunningSequences.ToArray();
	}

	// Token: 0x06000A14 RID: 2580 RVA: 0x0004D24C File Offset: 0x0004B44C
	public void InitList()
	{
		if (this.itsEntries.Count == 0)
		{
			this.itsEntries.Add(new KGFEventSequence.KGFEventSequenceEntry());
		}
	}

	// Token: 0x06000A15 RID: 2581 RVA: 0x0004D27C File Offset: 0x0004B47C
	public void Insert(KGFEventSequence.KGFEventSequenceEntry theElementAfterToInsert, KGFEventSequence.KGFEventSequenceEntry theElementToInsert)
	{
		int num = this.itsEntries.IndexOf(theElementAfterToInsert);
		this.itsEntries.Insert(num + 1, theElementToInsert);
	}

	// Token: 0x06000A16 RID: 2582 RVA: 0x0004D2A8 File Offset: 0x0004B4A8
	public void Delete(KGFEventSequence.KGFEventSequenceEntry theElementToDelete)
	{
		if (this.itsEntries.Count > 1)
		{
			this.itsEntries.Remove(theElementToDelete);
		}
	}

	// Token: 0x06000A17 RID: 2583 RVA: 0x0004D2C8 File Offset: 0x0004B4C8
	public void MoveUp(KGFEventSequence.KGFEventSequenceEntry theElementToMoveUp)
	{
		int num = this.itsEntries.IndexOf(theElementToMoveUp);
		if (num <= 0)
		{
			KGFEvent.LogWarning("cannot move up element at 0 index", "KGFEventSystem", this);
			return;
		}
		this.Delete(theElementToMoveUp);
		this.itsEntries.Insert(num - 1, theElementToMoveUp);
	}

	// Token: 0x06000A18 RID: 2584 RVA: 0x0004D310 File Offset: 0x0004B510
	public void MoveDown(KGFEventSequence.KGFEventSequenceEntry theElementToMoveDown)
	{
		int num = this.itsEntries.IndexOf(theElementToMoveDown);
		if (num >= this.itsEntries.Count - 1)
		{
			KGFEvent.LogWarning("cannot move down element at end index", "KGFEventSystem", this);
			return;
		}
		this.Delete(theElementToMoveDown);
		this.itsEntries.Insert(num + 1, theElementToMoveDown);
	}

	// Token: 0x06000A19 RID: 2585 RVA: 0x0004D364 File Offset: 0x0004B564
	public bool IsRunning()
	{
		return this.itsEventSequenceRunning;
	}

	// Token: 0x06000A1A RID: 2586 RVA: 0x0004D36C File Offset: 0x0004B56C
	public bool IsQueued()
	{
		int? num = this.itsEventDoneCounter;
		return num != null && !this.itsEventSequenceRunning;
	}

	// Token: 0x06000A1B RID: 2587 RVA: 0x0004D398 File Offset: 0x0004B598
	public string GetNextExecutedJobItem()
	{
		int? num = this.itsEventDoneCounter;
		if (num == null)
		{
			return "not running";
		}
		if (this.itsEventDoneCounter.GetValueOrDefault() < this.itsEntries.Count)
		{
			return this.itsEntries[this.itsEventDoneCounter.GetValueOrDefault()].itsEvent.name;
		}
		return "finished";
	}

	// Token: 0x06000A1C RID: 2588 RVA: 0x0004D400 File Offset: 0x0004B600
	[KGFEventExpose]
	public override void Trigger()
	{
		this.itsEventDoneCounter = new int?(0);
		if (base.gameObject.active)
		{
			this.itsEventSequenceRunning = true;
			KGFEvent.LogDebug("Start: " + base.gameObject.name, "KGFEventSystem", this);
			base.StartCoroutine("StartSequence");
		}
		else
		{
			KGFEvent.LogDebug("Queued: " + base.gameObject.name, "KGFEventSystem", this);
		}
	}

	// Token: 0x06000A1D RID: 2589 RVA: 0x0004D484 File Offset: 0x0004B684
	[KGFEventExpose]
	public void StopSequence()
	{
		base.StopCoroutine("StartSequence");
		this.itsEventSequenceRunning = false;
		this.itsEventDoneCounter = null;
		if (KGFEventSequence.itsListOfRunningSequences.Contains(this))
		{
			KGFEventSequence.itsListOfRunningSequences.Remove(this);
		}
	}

	// Token: 0x06000A1E RID: 2590 RVA: 0x0004D4D0 File Offset: 0x0004B6D0
	private IEnumerator StartSequence()
	{
		this.itsStayBeforeStepID = 0;
		if (!KGFEventSequence.itsListOfRunningSequences.Contains(this))
		{
			KGFEventSequence.itsListOfRunningSequences.Add(this);
		}
		int? num = this.itsEventDoneCounter;
		if (num == null)
		{
			yield break;
		}
		for (int i = this.itsEventDoneCounter.GetValueOrDefault(0); i < this.itsEntries.Count; i++)
		{
			KGFEventSequence.KGFEventSequenceEntry anEntry = this.itsEntries[i];
			if (anEntry.itsWaitBefore > 0f)
			{
				yield return new WaitForSeconds(anEntry.itsWaitBefore);
			}
			try
			{
				if (anEntry.itsEvent != null)
				{
					anEntry.itsEvent.Trigger();
				}
				else
				{
					KGFEvent.LogError("events have null entries", "KGFEventSystem", this);
				}
			}
			catch (Exception ex)
			{
				Exception e = ex;
				KGFEvent.LogError("Exception in event_sequence:" + e, "KGFEventSystem", this);
			}
			this.itsEventDoneCounter = new int?(i + 1);
			if (anEntry.itsWaitAfter > 0f)
			{
				yield return new WaitForSeconds(anEntry.itsWaitAfter);
			}
		}
		this.itsEventDoneCounter = null;
		this.itsEventSequenceRunning = false;
		if (KGFEventSequence.itsListOfRunningSequences.Contains(this))
		{
			KGFEventSequence.itsListOfRunningSequences.Remove(this);
		}
		yield break;
	}

	// Token: 0x06000A1F RID: 2591 RVA: 0x0004D4EC File Offset: 0x0004B6EC
	private void OnDestruct()
	{
		this.StopSequence();
	}

	// Token: 0x06000A20 RID: 2592 RVA: 0x0004D4F4 File Offset: 0x0004B6F4
	public override KGFMessageList Validate()
	{
		KGFMessageList kgfmessageList = new KGFMessageList();
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		if (this.itsEntries != null)
		{
			for (int i = 0; i < this.itsEntries.Count; i++)
			{
				KGFEventSequence.KGFEventSequenceEntry kgfeventSequenceEntry = this.itsEntries[i];
				if (kgfeventSequenceEntry.itsEvent == null)
				{
					flag = true;
				}
				if (kgfeventSequenceEntry.itsWaitBefore < 0f)
				{
					flag2 = true;
				}
				if (kgfeventSequenceEntry.itsWaitAfter < 0f)
				{
					flag3 = true;
				}
			}
		}
		if (flag)
		{
			kgfmessageList.AddError("sequence entry has null event");
		}
		if (flag2)
		{
			kgfmessageList.AddError("sequence entry itsWaitBefore <= 0");
		}
		if (flag3)
		{
			kgfmessageList.AddError("sequence entry itsWaitAfter <= 0");
		}
		return kgfmessageList;
	}

	// Token: 0x04000A47 RID: 2631
	private const string itsEventCategory = "KGFEventSystem";

	// Token: 0x04000A48 RID: 2632
	public List<KGFEventSequence.KGFEventSequenceEntry> itsEntries = new List<KGFEventSequence.KGFEventSequenceEntry>();

	// Token: 0x04000A49 RID: 2633
	private static List<KGFEventSequence> itsListOfRunningSequences = new List<KGFEventSequence>();

	// Token: 0x04000A4A RID: 2634
	private bool itsEventSequenceRunning;

	// Token: 0x04000A4B RID: 2635
	private static List<KGFEventSequence> itsListOfSequencesAll = new List<KGFEventSequence>();

	// Token: 0x04000A4C RID: 2636
	private static bool itsStepMode = false;

	// Token: 0x04000A4D RID: 2637
	private int itsStayBeforeStepID;

	// Token: 0x04000A4E RID: 2638
	private int? itsEventDoneCounter;

	// Token: 0x0200015D RID: 349
	[Serializable]
	public class KGFEventSequenceEntry
	{
		// Token: 0x04000A4F RID: 2639
		public float itsWaitBefore;

		// Token: 0x04000A50 RID: 2640
		public KGFEventBase itsEvent;

		// Token: 0x04000A51 RID: 2641
		public float itsWaitAfter;
	}
}
