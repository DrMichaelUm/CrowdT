using System.Collections.Generic;
using UnityEngine;

namespace KetchappTools.Core.Extensions
{
	public static class RandomX
	{
		#region // ==============================[Struct / Class]============================== //

			public class WeightedValue<T>
			{
				public T Value;
				public float weight;

				public static float[] GetWeights(List<WeightedValue<T>> list)
				{
					float[] probs = new float[list.Count];
					for (int i = 0; i < probs.Length; i++)
						probs[i] = list[i].weight;
					
					return probs;
				}
			}

		#endregion

		#region // ==============================[Methods]============================== //

			/// <summary>
			/// Returns an array of random int between min [inclusive] and max [inclusive].
			/// </summary>
			public static int[]		GetRandomNumbers(int min, int max, int count, bool noDuplicate = true)
			{
				int[] numbers = new int[count];

				if (!noDuplicate)
				{
					for (int i = 0; i < count; i++)
						numbers[i] = Random.Range(min, max);
				}
				else
				{
					List<int> orderedIndex = new List<int>();
					for (int i = 0; i < max; i++)
						orderedIndex.Add(i);

					for (int i = 0; i < count && orderedIndex.Count != 0; i++)
					{
						int randomIndex = Random.Range(0, orderedIndex.Count);
						numbers[i] = orderedIndex[randomIndex];
						orderedIndex.RemoveAt(randomIndex);
					}
				}

				return numbers;
			}

			/// <summary>
			/// return a ponderated random index of an array containing probability weights
			/// </summary>
			/// <param name="prob"></param>
			/// <returns></returns>
			public static int		PickRandomFromWeightedProbabilityArray(float[] prob)
			{
				float[] cumuProb = new float[prob.Length];
				for (int i = 0; i < cumuProb.Length; i++)
				{
					if (i == 0)
						cumuProb[i] = prob[i];
					else
					{
						cumuProb[i] = cumuProb[i - 1] + prob[i];
					}
				}

				//Debug
				string s = "cumu prob : ";
				for (int i = 0; i < cumuProb.Length; i++)
				{
					s += cumuProb[i] + ", ";
				}
				//Debug.Log(s);

				float rnd = Random.Range(0, cumuProb[cumuProb.Length - 1]);
				for (int i = 0; i < cumuProb.Length; i++)
				{
					if (rnd <= cumuProb[i])
					{
						//Debug.Log("random = " + rnd + " smaller than " + cumuProb[i] + " at index " + i);
						return i;
					}

				}

				//Debug.Log("random = " + rnd + " bigger than greatest number, select the last index");
				return cumuProb.Length - 1;
			}

			/// <summary>
			/// return a ponderated random index of an array containing probability weights
			/// </summary>
			/// <param name="prob"></param>
			/// <returns></returns>
			public static T			PickRandomFromWeightedProbabilityArray<T>(List<WeightedValue<T>> nums)
			{
				return nums[PickRandomFromWeightedProbabilityArray(WeightedValue<T>.GetWeights(nums))].Value;
			}

			/// <summary>
			/// Returns a random value between p_range.x [inclusive] and p_range.max [inclusive].
			/// </summary>
			public static float		RandomRange(Vector2 p_range)
			{
				return Random.Range(p_range.x, p_range.y);
			}

			/// <summary>
			/// Returns a Ray included in a range angle.
			/// </summary>
			public static Ray		RandomRay(Vector3 origin, Vector3 direction, Vector2 angleRange, float length = 1f)
			{
				float angle1 = Random.Range(angleRange.x, angleRange.y);
				float angle2 = Random.Range(angleRange.x, angleRange.y);
				Vector3 norm1 = Vector3.Cross(direction, new Vector3(1, 2, 3));
				Vector3 norm2 = Vector3.Cross(direction, norm1);
				Vector3 dir = direction + MathX.Sin(MathX.DegreeToRadian(angle1)) * norm1 + MathX.Sin(MathX.DegreeToRadian(angle2)) * norm2;

				return new Ray(origin, dir * length);
			}

			/// <summary>
			/// Returns a random Vector2 between min [inclusive] and max [inclusive].
			/// </summary>
			public static Vector2	RandomVector2 (Vector2 min, Vector2 max)
			{
				float x = Random.Range(-min.x, max.x);
				float y = Random.Range(-min.y, max.y);
	
				return new Vector2(x, y);
			}

			/// <summary>
			/// Returns a random Vector3 between min [inclusive] and max [inclusive].
			/// </summary>
			public static Vector3	RandomVector3(Vector3 min, Vector3 max)
			{
				float x = Random.Range(-min.x, max.x);
				float y = Random.Range(-min.y, max.y);
				float z = Random.Range(-min.z, max.z);

				return new Vector3(x, y, z);
			}

			/// <summary>
			/// Returns a random Vector3 between min [inclusive] and max [inclusive].
			/// </summary>
			public static Vector3	RandomVector3(float min, float max)
			{
				float x = Random.Range(-min, max);
				float y = Random.Range(-min, max);
				float z = Random.Range(-min, max);

				return new Vector3(x, y, z);
			}

			/// <summary>
			/// Returns a random Vector3 within a specific angular range.
			/// </summary>
			public static Vector3	RandomVector3 (Vector3 dir, float angleRange, bool allowInferiorValues)
			{
				float angle = angleRange;
				if (allowInferiorValues)
					angle = Random.Range(0, angleRange);
				Vector3 deltaDir = Quaternion.AngleAxis(Random.Range(0, 360), dir) * Vector3.Cross(dir, new Vector3(1.1f, 1.12f, 1.8f));
				return dir + deltaDir * MathX.Sin(MathX.DegreeToRadian(angle));
			}

		#endregion
	}
}

