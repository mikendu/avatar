using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VectorBuffer {

	private List<Vector2> m_buffer;
	private int m_size;
	private int m_head;

	public VectorBuffer(int size)
	{
		m_head = 0;
		m_size = size;
		m_buffer = new List<Vector2>(m_size);

		// Initialize buffer
		for (int i = 0; i < m_size; i++)
			m_buffer.Add (Vector2.zero);
	}

	public Vector2 GetAverage()
	{
		Vector2 average = Vector2.zero;
		foreach (Vector2 vector in m_buffer)
			average += ((1.0f / m_size) * vector);

		return average;
	}

	public void PushVector(Vector2 vector)
	{
		m_buffer[m_head] = vector;
		m_head = (m_head + 1) % m_size;
	}
}
