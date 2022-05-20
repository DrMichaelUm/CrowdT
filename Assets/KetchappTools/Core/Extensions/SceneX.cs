using UnityEditor;
using UnityEngine.SceneManagement;

namespace KetchappTools.Core.Extensions
{
	public static class SceneX
	{
		#if UNITY_EDITOR

			/// <summary>
			/// Add this scene to the EditorBuildSettings.
			/// </summary>
			/// <returns></returns>
			public static void	AddToBuild(this Scene _scene)
			{
				// Create new list
				EditorBuildSettingsScene[] editorBuildSettingsScene = new EditorBuildSettingsScene[EditorBuildSettings.scenes.Length - 1];

				foreach (EditorBuildSettingsScene iEditorBuildSettingsScene in EditorBuildSettings.scenes)
				{
					if (iEditorBuildSettingsScene.path == _scene.path)
						return;
				}

				// Copy the old List on new
				System.Array.Copy(EditorBuildSettings.scenes, editorBuildSettingsScene, EditorBuildSettings.scenes.Length);

				// Add new scene to list on last position
				editorBuildSettingsScene[EditorBuildSettings.scenes.Length] = new EditorBuildSettingsScene(_scene.path, true);

				// Save the new list
				EditorBuildSettings.scenes = editorBuildSettingsScene;
			}

			/// <summary>
			/// Add this scene to the EditorBuildSettings.
			/// </summary>
			/// <returns></returns>
			public static void	RemoveToBuild(this Scene _scene)
			{
				// Create new list
				EditorBuildSettingsScene[] editorBuildSettingsScene = new EditorBuildSettingsScene[EditorBuildSettings.scenes.Length - 1];

				// Add scene to list
				int i = 0;
				bool find = false;

				foreach (EditorBuildSettingsScene iEditorBuildSettingsScene in EditorBuildSettings.scenes)
				{
					if (iEditorBuildSettingsScene.path == _scene.path)
						find = true;
					else if (i < editorBuildSettingsScene.Length)
					{
						editorBuildSettingsScene[i] = iEditorBuildSettingsScene;
						i++;
					}
				}

				// Save the new list only if find the element to remove
				if (find)
					EditorBuildSettings.scenes = editorBuildSettingsScene;
			}

		#endif
	}
}