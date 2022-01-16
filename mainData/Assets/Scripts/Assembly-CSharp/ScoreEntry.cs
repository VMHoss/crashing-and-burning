using System;

// Token: 0x0200011D RID: 285
public class ScoreEntry : IComparable<ScoreEntry>
{
	// Token: 0x06000817 RID: 2071 RVA: 0x0003D298 File Offset: 0x0003B498
	public ScoreEntry(string anItem, int aScore)
	{
		this.item = anItem;
		this.score = aScore;
	}

	// Token: 0x06000818 RID: 2072 RVA: 0x0003D2B0 File Offset: 0x0003B4B0
	public int CompareTo(ScoreEntry other)
	{
		return this.item.CompareTo(other.item);
	}

	// Token: 0x0400089A RID: 2202
	public string item;

	// Token: 0x0400089B RID: 2203
	public int score;
}
