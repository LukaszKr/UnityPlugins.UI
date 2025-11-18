using System.Collections.Generic;
using System.IO;
using UnityPlugins.Common.Editor;
using UnityPlugins.UI.Unity;
using UnityEditor;
using UnityEngine;

namespace UnityPlugins.UI.Editor
{
	[CustomEditor(typeof(DefaultPanelsContainer))]
	public class AssetPanelProviderSOEditor : AExtendedEditor<DefaultPanelsContainer>
	{
		protected override void Initialize()
		{
		}

		protected override void Draw()
		{
			DrawDefaultGUI();

			if(GUILayout.Button("Refresh List"))
			{
				List<APanelComponent> panels = GetPanels();
				Target.SetPanels(panels);
				EditorUtility.SetDirty(Target);
			}
		}

		private List<APanelComponent> GetPanels()
		{
			string targetPath = AssetDatabase.GetAssetPath(Target);
			string directoryPath = Path.GetDirectoryName(targetPath);
			string[] guids = AssetDatabase.FindAssets("t:prefab", new string[] { directoryPath });
			int length = guids.Length;

			List<APanelComponent> panels = new List<APanelComponent>(length);
			for(int x = 0; x < length; ++x)
			{
				string assetPath = AssetDatabase.GUIDToAssetPath(guids[x]);
				APanelComponent panelPrefab = AssetDatabase.LoadAssetAtPath<APanelComponent>(assetPath);
				if(panelPrefab != null)
				{
					panels.Add(panelPrefab);
				}
			}
			return panels;
		}
	}
}
