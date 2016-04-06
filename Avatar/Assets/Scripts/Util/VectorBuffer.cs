using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VectorBuffer {

	private List<Vector2> m_buffer;
	private int m_size;
	private int m_head;
	private int m_count;

	public VectorBuffer(int size)
	{
		m_head = 0;
		m_count = 0;
		m_size = size;
		m_buffer = new List<Vector2>(m_size);

		// Initialize buffer
		for (int i = 0; i < m_size; i++)
			m_buffer.Add (Vector2.zero);
	}

	public Vector2 GetAverage()
	{
		Vector2 sum = GetSum();
		return ((1.0f / m_count) * sum);
	}

	public Vector2 GetSum()
	{
		Vector2 sum = Vector2.zero;
		for(int i = 0; i < m_count; i++)
			sum += m_buffer[i];

		return sum;
	}

	public void PushVector(Vector2 vector)
	{
		m_buffer[m_head] = vector;
		m_head = (m_head + 1) % m_size;
		m_count = Mathf.Min (m_size, m_count + 1);
	}

	public void Reset()
	{
		m_count = 0;
		m_head = 0;
	}

	public bool IsFull()
	{
		return (m_count >= m_size);
	}


}
