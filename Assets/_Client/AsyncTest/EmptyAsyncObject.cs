using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Ketchapp.CrowdTerritory
{
	public class EmptyAsyncObject : MonoBehaviour
	{
		private void Start()
		{
			// MoveUniTask();
		}

		async UniTask<string> DemoAsync()
		{
			while (true)
			{
				await UniTask.Yield();
			}
		}

		private async void Update()
		{
			if (Input.GetKey(KeyCode.Space))
				MoveUniTask();
		}

		private async void Move()
		{
			while (true)
			{
				transform.position += transform.forward * Time.deltaTime;
				await Task.Yield();
			}
		}
		
		private async UniTaskVoid MoveUniTask()
		{
			// while (true)
			// {
				transform.position += transform.forward * Time.deltaTime;
				await UniTask.Yield();
			// }
		}
	}
}