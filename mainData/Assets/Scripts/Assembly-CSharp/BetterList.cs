using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000052 RID: 82
public class BetterList<T>
{
	// Token: 0x06000271 RID: 625 RVA: 0x00010E7C File Offset: 0x0000F07C
	public IEnumerator<T> GetEnumerator()
	{
		if (this.buffer != null)
		{
			for (int i = 0; i < this.size; i++)
			{
				yield return this.buffer[i];
			}
		}
		yield break;
	}

	// Token: 0x17000046 RID: 70
	public T this[int i]
	{
		get
		{
			return this.buffer[i];
		}
		set
		{
			this.buffer[i] = value;
		}
	}

	// Token: 0x06000274 RID: 628 RVA: 0x00010EB8 File Offset: 0x0000F0B8
	private void AllocateMore()
	{
		T[] array = (this.buffer == null) ? new T[32] : new T[Mathf.Max(this.buffer.Length << 1, 32)];
		if (this.buffer != null && this.size > 0)
		{
			this.buffer.CopyTo(array, 0);
		}
		this.buffer = array;
	}

	// Token: 0x06000275 RID: 629 RVA: 0x00010F20 File Offset: 0x0000F120
	private void Trim()
	{
		if (this.size > 0)
		{
			if (this.size < this.buffer.Length)
			{
				T[] array = new T[this.size];
				for (int i = 0; i < this.size; i++)
				{
					array[i] = this.buffer[i];
				}
				this.buffer = array;
			}
		}
		else
		{
			this.buffer = null;
		}
	}

	// Token: 0x06000276 RID: 630 RVA: 0x00010F98 File Offset: 0x0000F198
	public void Clear()
	{
		this.size = 0;
	}

	// Token: 0x06000277 RID: 631 RVA: 0x00010FA4 File Offset: 0x0000F1A4
	public void Release()
	{
		this.size = 0;
		this.buffer = null;
	}

	// Token: 0x06000278 RID: 632 RVA: 0x00010FB4 File Offset: 0x0000F1B4
	public void Add(T item)
	{
		if (this.buffer == null || this.size == this.buffer.Length)
		{
			this.AllocateMore();
		}
		this.buffer[this.size++] = item;
	}

	// Token: 0x06000279 RID: 633 RVA: 0x00011004 File Offset: 0x0000F204
	public void Insert(int index, T item)
	{
		if (this.buffer == null || this.size == this.buffer.Length)
		{
			this.AllocateMore();
		}
		if (index < this.size)
		{
			for (int i = this.size; i > index; i--)
			{
				this.buffer[i] = this.buffer[i - 1];
			}
			this.buffer[index] = item;
			this.size++;
		}
		else
		{
			this.Add(item);
		}
	}

	// Token: 0x0600027A RID: 634 RVA: 0x0001109C File Offset: 0x0000F29C
	public bool Contains(T item)
	{
		if (this.buffer == null)
		{
			return false;
		}
		for (int i = 0; i < this.size; i++)
		{
			if (this.buffer[i].Equals(item))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600027B RID: 635 RVA: 0x000110F4 File Offset: 0x0000F2F4
	public bool Remove(T item)
	{
		if (this.buffer != null)
		{
			EqualityComparer<T> @default = EqualityComparer<T>.Default;
			for (int i = 0; i < this.size; i++)
			{
				if (@default.Equals(this.buffer[i], item))
				{
					this.size--;
					this.buffer[i] = default(T);
					for (int j = i; j < this.size; j++)
					{
						this.buffer[j] = this.buffer[j + 1];
					}
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x0600027C RID: 636 RVA: 0x00011198 File Offset: 0x0000F398
	public void RemoveAt(int index)
	{
		if (this.buffer != null && index < this.size)
		{
			this.size--;
			this.buffer[index] = default(T);
			for (int i = index; i < this.size; i++)
			{
				this.buffer[i] = this.buffer[i + 1];
			}
		}
	}

	// Token: 0x0600027D RID: 637 RVA: 0x00011210 File Offset: 0x0000F410
	public T[] ToArray()
	{
		this.Trim();
		return this.buffer;
	}

	// Token: 0x0600027E RID: 638 RVA: 0x00011220 File Offset: 0x0000F420
	public void Sort(Comparison<T> comparer)
	{
		bool flag = true;
		while (flag)
		{
			flag = false;
			for (int i = 1; i < this.size; i++)
			{
				if (comparer(this.buffer[i - 1], this.buffer[i]) > 0)
				{
					T t = this.buffer[i];
					this.buffer[i] = this.buffer[i - 1];
					this.buffer[i - 1] = t;
					flag = true;
				}
			}
		}
	}

	// Token: 0x040002F3 RID: 755
	public T[] buffer;

	// Token: 0x040002F4 RID: 756
	public int size;
}
