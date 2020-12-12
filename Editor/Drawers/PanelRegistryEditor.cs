using System.Collections.Generic;
using System.IO;
using ProceduralLevel.UnityPlugins.CustomUI;
using ProceduralLevel.UnityPluginsEditor.ExtendedEditor;
using UnityEditor;
using UnityEngine;

namespace ProceduralLevel.UnityPluginsEditor.CustomUI
{
	[CustomEditor(typeof(PanelRegistry))]
	public class PanelRegistryEditor: AExtendedEditor<PanelRegistry>
	{
		protected override void Initialize()
		{
			DrawDefault = true;
		}

		protected override void Draw()
		{
			if(GUILayout.Button("Refresh List"))
			{
				List<AUIPanel> panels = GetPanels();
				Target.SetPanels(panels);
				EditorUtility.SetDirty(Target);
			}
		}

		private List<AUIPanel> GetPanels()
		{
			string targetPath = AssetDatabase.GetAssetPath(Target);
			string directoryPath = Path.GetDirectoryName(targetPath);
			string[] guids = AssetDatabase.FindAssets("t:prefab", new string[] { directoryPath });
			int length = guids.Length;

			List<AUIPanel> panels = new List<AUIPanel>(length);
			for(int x = 0; x < length; ++x)
			{
				string assetPath = AssetDatabase.GUIDToAssetPath(guids[x]);
				AUIPanel panelPrefab = AssetDatabase.LoadAssetAtPath<AUIPanel>(assetPath);
				if(panelPrefab != null)
				{
					panels.Add(panelPrefab);
				}
			}
			return panels;
		}
	}
}
