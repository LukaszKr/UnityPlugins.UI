using System.Collections.Generic;
using System.IO;
using UnityPlugins.Common.Editor;
using UnityPlugins.UI.Unity;
using UnityEditor;
using UnityEngine;

namespace UnityPlugins.UI.Editor
{
	[CustomEditor(typeof(AssetPanelProvider))]
	public class AssetPanelProviderEditor : AExtendedEditor<AssetPanelProvider>
	{
		protected override void Initialize()
		{
			DrawDefault = true;
		}

		protected override void Draw()
		{
			if(GUILayout.Button("Refresh List"))
			{
				List<APanel> panels = GetPanels();
				Target.SetPanels(panels);
				EditorUtility.SetDirty(Target);
			}
		}

		private List<APanel> GetPanels()
		{
			string targetPath = AssetDatabase.GetAssetPath(Target);
			string directoryPath = Path.GetDirectoryName(targetPath);
			string[] guids = AssetDatabase.FindAssets("t:prefab", new string[] { directoryPath });
			int length = guids.Length;

			List<APanel> panels = new List<APanel>(length);
			for(int x = 0; x < length; ++x)
			{
				string assetPath = AssetDatabase.GUIDToAssetPath(guids[x]);
				APanel panelPrefab = AssetDatabase.LoadAssetAtPath<APanel>(assetPath);
				if(panelPrefab != null)
				{
					panels.Add(panelPrefab);
				}
			}
			return panels;
		}
	}
}
